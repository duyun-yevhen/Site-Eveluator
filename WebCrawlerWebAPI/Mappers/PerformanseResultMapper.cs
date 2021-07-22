using AutoMapper;
using System.Collections.Generic;
using WebCrawlerWebAPI.Models;

namespace WebCrawlerWebAPI.Mappers
{
	public class PerformanseResultMapper
	{
		private readonly Mapper _mapper;

		public PerformanseResultMapper()
		{
			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<WebCrawler.Model.PerformanceTest, PerformanceTest>();
				cfg.CreateMap<WebCrawler.Model.PerformanceResult, PerformanceResult>();
			});

			_mapper = new Mapper(configuration);
		}

		public PerformanceTest Map(WebCrawler.Model.PerformanceTest source)
		{
			return _mapper.Map<PerformanceTest>(source);
		}

		public IEnumerable<PerformanceTest> Map(IEnumerable<WebCrawler.Model.PerformanceTest> source)
		{
			return _mapper.Map<IEnumerable<PerformanceTest>>(source);
		}
	}
}
