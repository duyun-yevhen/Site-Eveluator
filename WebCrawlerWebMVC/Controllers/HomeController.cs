using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCrawler.Service;

namespace WebCrawler.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly SiteCrawlerService _sitePefrormanseService;

		public HomeController( SiteCrawlerService sitePefrormanseService)
		{
			_sitePefrormanseService = sitePefrormanseService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var result = await _sitePefrormanseService.GetTestsAsync();

			return View(result);
		}

		[HttpGet]
		public async Task<int> GetPerformance(Uri url)
		{
			var id = await _sitePefrormanseService.GetSitePefrormanseAsync(url);

			return id;
		}
	}
}
