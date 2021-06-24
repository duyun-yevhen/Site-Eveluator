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

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, DbWorker dbWorker)
		{
			_logger = logger;
			_dbWorker = dbWorker;
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
			SiteCrawlerWorker siteCrawlerWorker = new SiteCrawlerWorker(); //изменить?
			await Task.Run(() =>
				{
					ViewBag.StartUrl = url;
					results = siteCrawlerWorker.GetAllLinks(url);
					siteCrawlerWorker.RequestUrlsForSetResponseTimes(results);
					ViewBag.PerfomanseResult = results;
				});

			await _dbWorker.SaveResultAsync(url, results);
			return View("Index");
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
