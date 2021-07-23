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
	public class CrawlerTestController : Controller
	{
		private readonly SiteCrawlerService _siteCrawlerService;
		private readonly IMapper _mapper;


		public CrawlerTestController(SiteCrawlerService siteCrawlerService, IMapper mapper)
		{
			_siteCrawlerService = siteCrawlerService;
			_mapper = mapper;
		}

		[HttpGet("{id}")]
		public async Task<PerformanceTest> GetTestResultById(int id)
		{
			var result = await _siteCrawlerService.GetResultsByTestIdAsync(id);

			return _mapper.Map<PerformanceTest>(result);
		}


		[HttpGet]
		public async Task<IEnumerable<PerformanceTestInfo>> GetAllTestsInfo()
		{
			var result = await _siteCrawlerService.GetTestsAsync();

			return _mapper.Map<IEnumerable<PerformanceTestInfo>>(result);
		}

		[HttpPost]
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
