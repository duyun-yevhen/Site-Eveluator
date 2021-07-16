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
	[Route("api/[controller]/[action]")]
	public class CrawlerController : Controller
	{
		private readonly SiteCrawlerService _siteCrawlerService;

		public CrawlerController( SiteCrawlerService siteCrawlerService)
		{
			_siteCrawlerService = siteCrawlerService;
		}

		[HttpGet]
		public async Task<PerformanceTest> GetTestResultById(int testID)
		{
			var result = await _siteCrawlerService.GetResultsByTestIdAsync(testID);
			result.UrlTestResults.AsParallel().ForAll(s => s.Test = null);
			return result;
		}


		[HttpGet]
		public async Task<IEnumerable<PerformanceTest>> GetAllTestsResult()
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
