using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Model;
using WebCrawler.WebApplication.Models;

namespace WebCrawler.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly DbWorker _dbWorker;
		private readonly SiteCrawlerWorker _siteCrawlerWorker;


		public HomeController(DbWorker dbWorker, SiteCrawlerWorker siteCrawlerWorker)
		{
			_dbWorker = dbWorker;
			_siteCrawlerWorker = siteCrawlerWorker;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _dbWorker.GetTestsAsync());
		}

		[HttpGet]
		public async Task<int> GetPerformance(Uri url)
		{
			var result = await _siteCrawlerWorker.DoWorkAsync(url, 250);
			int id = await _dbWorker.SaveResultAsync(url, result);

			return id;
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
