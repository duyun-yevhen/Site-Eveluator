using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawlerWebMVC.Models;

namespace WebCrawlerWebMVC.Controllers
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
		public IActionResult Index()
		{
			ViewBag.Tests = _dbWorker.GetTests();
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetPerformance(Uri url)
		{
			List<UrlPerformanseTestResult> results = null;
			await Task.Run(() =>
				{
					results = _siteCrawlerWorker.GetAllLinks(url);
					_siteCrawlerWorker.RequestUrlsForSetResponseTimes(results, timeout:1000);
				});

			await _dbWorker.SaveResultAsync(url, results);
			ViewBag.TestResult = _dbWorker.GetLastTest();
			return View("TestResults");
		}

		[HttpGet]
		public async Task<IActionResult> TestResults(int testID)
		{
			await Task.Run(() =>
			{ 
				var test = _dbWorker.GetResultsByTestID(testID);
				var results = _dbWorker.GetResultsByTestID(testID);
				ViewBag.TestResult = results;
			});
			return View();
		}



		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
