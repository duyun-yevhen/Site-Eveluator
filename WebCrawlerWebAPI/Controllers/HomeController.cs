using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.Service;

namespace WebCrawlerWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : Controller
	{
		private readonly SiteCrawlerService _siteCrawlerService;

		public HomeController( SiteCrawlerService siteCrawlerService)
		{
			_siteCrawlerService = siteCrawlerService;
		}

		[HttpGet]
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<PerformanceTest> TestResults(int testID)
		{
			var result = await _siteCrawlerService.GetResultsByTestIdAsync(testID);
			result.UrlTestResults.AsParallel().ForAll(s => s.Test = null);
			return result;
		}


		[HttpGet]
		public async Task<IEnumerable<PerformanceTest>> Index()
		{
			return await _siteCrawlerService.GetTestsAsync();
		}

		[HttpPost]
		public async Task<int> GetPerformance(Uri url)
		{
			var id = await _siteCrawlerService.GetSitePefrormanseAsync(url);

			return id;
		}
		
		
	}
}
