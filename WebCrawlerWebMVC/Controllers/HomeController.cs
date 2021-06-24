using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawlerWebMVC.Models;

namespace WebCrawlerWebMVC.Controllers
{
	public class HomeController : Controller
	{

		private readonly SiteCrawlerWorker siteCrawlerWorker = new SiteCrawlerWorker(); //изменить?
		private readonly DbWorker _dbWorker;

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, DbWorker dbWorker)
		{
			_logger = logger;
			_dbWorker = dbWorker;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetPerformance(Uri url)
		{
			ViewBag.StartUrl = url;
			var results = siteCrawlerWorker.GetAllLinks(url);
			siteCrawlerWorker.RequestUrlsForSetResponseTimes(results);
			_dbWorker.SaveResult(url, results);
			ViewBag.PerfomanseResult = results;
			return View("Index");
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
