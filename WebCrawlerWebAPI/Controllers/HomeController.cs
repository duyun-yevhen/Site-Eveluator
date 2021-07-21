using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.Service;
using WebCrawlerWebAPI.Models;

namespace WebCrawlerWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CrawlerController : Controller
	{
		private readonly SiteCrawlerService _siteCrawlerService;

		public CrawlerController( SiteCrawlerService siteCrawlerService)
		{
			_siteCrawlerService = siteCrawlerService;
		}

		//[HttpGet("{id}")]
		//public async Task<TestsResult> GetTestResultById(int id)
		//{
		//	var maper = new Mappers.TestsMapper();
		//	var result = await _siteCrawlerService.GetResultsByTestIdAsync(id);
		//	return await Task.Run(() => maper.Map(result));
		//}


		//[HttpGet]
		//public async Task<IEnumerable<TestsResult>> GetAllTestsResult()
		//{
		//	var maper = new Mappers.TestsMapper();
		//	var result = await _siteCrawlerService.GetTestsAsync();
		//	return await Task.Run(() => maper.Map(result));
		//}

		[HttpGet("CrawlerTests/{id}")]
		public async Task<PerformanceTest> GetTestResultById(int id)
		{
			var result = await _siteCrawlerService.GetResultsByTestIdAsync(id);
			result.UrlTestResults.AsParallel().ForAll(s => s.Test = null);
			return result;
		}


		[HttpGet("CrawlerTests")]
		public async Task<IEnumerable<PerformanceTest>> GetAllTestsResult()
		{
			var result = await _siteCrawlerService.GetTestsAsync();
			result.AsParallel().ForAll(s => s.UrlTestResults = null);
			return result;
		}

		[HttpPost("CrawlerTests/NewTest")]
		public async Task<int> GetPerformance([FromBody] Uri url)
		{
			return await _siteCrawlerService.GetSitePefrormanseAsync(url);
		}
	}
}
