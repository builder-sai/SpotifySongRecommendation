using System.Threading.Tasks;
using System.Web.Mvc;
using SpotifyWeb.Models;
using SpotifyWeb.ViewModels.Builders;

namespace SpotifyWeb.Controllers
{
	public class RecommendationController : Controller
	{
		// GET: Recommendation
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(RecommendationModel model)
		{
			if (ModelState.IsValid)
			{
			    var recommendationModelBuilder = new TrackListModelBuilder(model);
			    var recommendedTracks = await recommendationModelBuilder.BuildAsync();
			    return View("Details", recommendedTracks);
			}
			return View();

		}
	}
}