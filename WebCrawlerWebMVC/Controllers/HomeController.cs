using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.Service;

namespace WebCrawler.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly DbWorker _dbWorker;
		private readonly SiteCrawlerService _sitePefrormanseService;

		public HomeController(DbWorker dbWorker, SiteCrawlerService sitePefrormanseService)
		{
			_dbWorker = dbWorker;
			_sitePefrormanseService = sitePefrormanseService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var result = await _dbWorker.GetTestsAsync();

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
