using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCrawler.Service;

namespace WebCrawler.WebApplication.Controllers
{
	public class ResultsController : Controller
	{
		private readonly SiteCrawlerService _siteCrawlerService;

		public ResultsController(SiteCrawlerService siteCrawlerService)
		{
			_siteCrawlerService = siteCrawlerService;
		}

		[HttpGet]
		public async Task<IActionResult> TestResults(int testID)
		{
			var result = await _siteCrawlerService.GetResultsByTestIdAsync(testID);

			return View(result);
		} 
	}
}
