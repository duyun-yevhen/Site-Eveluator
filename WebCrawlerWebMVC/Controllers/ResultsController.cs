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

		public ResultsController(DbWorker dbWorker)
		{
			_dbWorker = dbWorker;
		}

		[HttpGet]
		public async Task<IActionResult> TestResults(int testID)
		{
			var result = await _dbWorker.GetResultsByTestIDAsync(testID);
			return View(result);
		} 

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
