using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Model;
using WebCrawler.WebApplication.Models;
using WebCrawler.WebApplication.Services;

namespace WebCrawler.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly DbWorker _dbWorker;
		private readonly SitePefrormanseService _sitePefrormanseService;


		public HomeController(DbWorker dbWorker, SitePefrormanseService sitePefrormanseService)
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


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
