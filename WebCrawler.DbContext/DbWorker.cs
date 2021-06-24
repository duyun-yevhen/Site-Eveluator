using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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

		public async Task SaveResultAsync(Uri siteUrl, List<UrlPerformanseTestResult> urlResponseTimes)
		{
			var test = new PerformanceTest() { SiteUrl = siteUrl };
			_testsRepository.Add(test);
			await _testsRepository.SaveChangesAsync();

			_urlResponseTimeRepository.AddRange(urlResponseTimes.Select(p => { p.TestID = test.Id; return p;})); ;
			await _urlResponseTimeRepository.SaveChangesAsync();
		}

		public PerformanceTest GetResultsByTestID(int testID)
		{
			return _testsRepository.Include(p => p.UrlResponseTimes).FirstOrDefault(s => s.Id == testID); 
		}

	}
}
