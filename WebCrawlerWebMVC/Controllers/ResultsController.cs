using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WebCrawler.Model;

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
			var result = await _dbWorker.GetResultsByTestIdAsync(testID);

			return View(result);
		} 
	}
}
