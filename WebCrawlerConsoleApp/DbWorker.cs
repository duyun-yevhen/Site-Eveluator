using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	public class DbWorker
	{
		private readonly IRepository<PerformanceTest> _testsRepository;
		private readonly IRepository<UrlResponseTime> _urlResponseTimeRepository;

		public DbWorker(IRepository<PerformanceTest> testsRepository, IRepository<UrlResponseTime> urlResponseTimeRepository)
		{
			_testsRepository = testsRepository;
			_urlResponseTimeRepository = urlResponseTimeRepository;
		}

		public void SaveResult(Uri siteUrl, List<UrlResponseTime> urlResponseTimes)
		{
			var test = new PerformanceTest() { SiteUrl = siteUrl, Date = DateTime.UtcNow };
			_testsRepository.Add(test);
			_testsRepository.SaveChanges();

			_urlResponseTimeRepository.AddRange(urlResponseTimes.Select(p=> { p.TestID = test.TestId;  return p; }));;
			_urlResponseTimeRepository.SaveChanges();
		}
	}
}
