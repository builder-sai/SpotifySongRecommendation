using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpotifyWeb.Models
{
	public class RecommendationModel
	{
		public int Id { get; set; }

		//Added DisplayName to be shown in @Html.LabelFor()
		[DisplayName("What artist do you like listen to now?")]
		public string Artist { get; set; }

		[DisplayName("In a dancing mode? Check this box for suitable tempo, rhythm stability and beat strenght.")]
		public bool Danceability { get; set; }

		[DisplayName("Slow or energitic tracks? Enter a number between 0 - 100. 0 for slow and more for faster and louder tracks.")]
		[Range(0, 100, ErrorMessage = "Energy level range 0-100")]
		public float? Energy { get; set; }

		[DisplayName("How fast would you like the tracks tempo? Enter a number between 0 - 100. 0 for slow and more for faster pace.")]
		[Range(0, 100, ErrorMessage = "Tempo must be in the range 0-100")]
		public float? Tempo { get; set; }
		
	}
}