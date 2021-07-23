using AutoMapper;
using System.Collections.Generic;
using WebCrawlerWebAPI.Models;

namespace WebCrawlerWebAPI.Mappers
{
	public class PerformanseResultMapperProfile : Profile
	{
		public PerformanseResultMapperProfile()
		{
			CreateMap<WebCrawler.Model.PerformanceTest, PerformanceTest>();
			CreateMap<WebCrawler.Model.PerformanceResult, PerformanceResult>();
			CreateMap<WebCrawler.Model.PerformanceTest, PerformanceTestInfo>();
		}
	}
}
