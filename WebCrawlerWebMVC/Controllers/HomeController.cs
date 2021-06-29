using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.WebApplication.Models;

namespace WebCrawler.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly DbWorker _dbWorker;
		private readonly SiteCrawlerWorker _siteCrawlerWorker;

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, DbWorker dbWorker, SiteCrawlerWorker siteCrawlerWorker)
		{
			_logger = logger;
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
