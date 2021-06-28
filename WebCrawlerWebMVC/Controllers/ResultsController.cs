using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.WebApplication.Models;

namespace WebCrawler.WebApplication.Controllers
{
	public class ResultsController : Controller
	{
		private readonly DbWorker _dbWorker;

		private readonly ILogger<HomeController> _logger;

		public ResultsController(ILogger<HomeController> logger, DbWorker dbWorker)
		{
			_logger = logger;
			_dbWorker = dbWorker;
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
