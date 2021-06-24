using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WebCrawler.Model
{
	public class DbWorker
	{
		private readonly IRepository<PerformanceTest> _testsRepository;
		private readonly IRepository<UrlPerformanseTestResult> _urlResponseTimeRepository;

		public DbWorker(IRepository<PerformanceTest> testsRepository, IRepository<UrlPerformanseTestResult> urlResponseTimeRepository)
		{
			_testsRepository = testsRepository;
			_urlResponseTimeRepository = urlResponseTimeRepository;
		}

		public void SaveResult(Uri siteUrl, List<UrlPerformanseTestResult> urlResponseTimes)
		{
			var test = new PerformanceTest() { SiteUrl = siteUrl };
			_testsRepository.Add(test);
			 _testsRepository.SaveChanges();

			_urlResponseTimeRepository.AddRange(urlResponseTimes.Select(p => { p.TestID = test.Id; return p;})); ;
			_urlResponseTimeRepository.SaveChanges();
		}
	}
}
