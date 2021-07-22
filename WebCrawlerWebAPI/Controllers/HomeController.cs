using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawlerWebAPI.Models;
using WebCrawler.Service;
using AutoMapper;
using WebCrawlerWebAPI.Mappers;

namespace WebCrawlerWebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CrawlerController : Controller
	{
		private readonly SiteCrawlerService _siteCrawlerService;
		private readonly PerformanseResultMapper _performanseResultMapper;

		public CrawlerController(SiteCrawlerService siteCrawlerService, PerformanseResultMapper performanseResultMapper)
		{
			_siteCrawlerService = siteCrawlerService;
			_performanseResultMapper = performanseResultMapper;
		}

		[HttpGet("CrawlerTests/{id}")]
		public async Task<PerformanceTest> GetTestResultById(int id)
		{
			var result = await _siteCrawlerService.GetResultsByTestIdAsync(id);
			return _performanseResultMapper.Map(result);
		}


		[HttpGet("CrawlerTests")]
		public async Task<IEnumerable<PerformanceTest>> GetAllTestsResult()
		{
			var result = await _siteCrawlerService.GetTestsAsync();
			return _performanseResultMapper.Map(result);
		}

		[HttpPost("CrawlerTests/NewTest")]
		public async Task<int> GetPerformance([FromBody] Uri url)
		{

			if (url.IsAbsoluteUri)
			{
				return await _siteCrawlerService.GetSitePefrormanseAsync(url);
			}

			else
			{
				return -1;
			}
		}
	}
}
