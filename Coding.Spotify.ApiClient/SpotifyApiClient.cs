using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coding.Spotify.ApiClient.Models;
using Flurl;
using Newtonsoft.Json;

namespace Coding.Spotify.ApiClient
{
	public class SpotifyApiClient
	{
		private const string ClientId = "996d0037680544c987287a9b0470fdbb";
		private const string ClientSecret = "5a3c92099a324b8f9e45d77e919fec13";

		protected const string BaseUrl = "https://api.spotify.com/";
		private HttpClient GetDefaultClient()
		{
			var authHandler = new SpotifyAuthClientCredentialsHttpMessageHandler(
				ClientId,
				ClientSecret,
				new HttpClientHandler());

			var client = new HttpClient(authHandler)
			{
				BaseAddress = new Uri(BaseUrl)
			};

			return client;
		}

		public async Task<SearchArtistResponse> SearchArtistsAsync(string artistName, int? limit = null, int? offset = null)
		{
			var client = GetDefaultClient();

			var url = new Url("/v1/search");
			url = url.SetQueryParam("q", artistName);
			url = url.SetQueryParam("type", "artist");

			if (limit != null)
				url = url.SetQueryParam("limit", limit);

			if (offset != null)
				url = url.SetQueryParam("offset", offset);

			var response = await client.GetStringAsync(url);

			var artistResponse = JsonConvert.DeserializeObject<SearchArtistResponse>(response);
			return artistResponse;
		}

		/// <summary>
		/// //https://developer.spotify.com/web-api/get-recommendations/
		/// Using: Get recommendations based on seeds, endpoint GET https://api.spotify.com/v1/recommendations
		/// </summary>
		/// <param name="artistId"></param>
		/// <param name="danceability"></param>
		/// <param name="energy"></param>
		/// <param name="tempo"></param>
		/// <returns></returns>
		public async Task<TrackListResponse> GetRecommendationsAsync(string artistId, float danceability, float? energy, float? tempo)
		{
			if (!string.IsNullOrEmpty(artistId))
			{
				var client = GetDefaultClient();
				var url = new Url("/v1/recommendations");

				url = url.SetQueryParam("seed_artists", artistId);
				url = url.SetQueryParam("target_danceability", danceability);

				if (energy != null)
					url = url.SetQueryParam("target_energy", energy);

				if (tempo != null)
					url = url.SetQueryParam("target_tempo", tempo);

				var response = await client.GetStringAsync(url);
				var recommendationResponse = JsonConvert.DeserializeObject<TrackListResponse>(response);

				return recommendationResponse;
			}
			return new TrackListResponse();
		}
	}
}
