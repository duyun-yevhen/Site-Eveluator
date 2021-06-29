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

		public async Task<int> SaveResultAsync(Uri siteUrl, List<UrlPerformanseTestResult> urlResponseTimes)
		{
			var test = new PerformanceTest() { SiteUrl = siteUrl };
			_testsRepository.Add(test);
			await _testsRepository.SaveChangesAsync();

			_urlResponseTimeRepository.AddRange(urlResponseTimes.Select(p => { p.TestID = test.Id; return p; })); ;
			await _urlResponseTimeRepository.SaveChangesAsync();
			return test.Id;
		}

		public async Task<PerformanceTest> GetResultsByTestIDAsync(int testID)
		{
			PerformanceTest result = null;
			await Task.Run(() =>
			{
				result = _testsRepository.Include(p => p.UrlResponseTimes).FirstOrDefault(s => s.Id == testID);
			});
			return result;
		}

		public async Task<IQueryable<PerformanceTest>> GetTestsAsync()
		{
			IQueryable<PerformanceTest> result = null;
			await Task.Run(() =>
			{
				result = _testsRepository.GetAll();
			});
			return result;
		}

		public PerformanceTest GetLastTest()
		{
			return _testsRepository.Include(p => p.UrlResponseTimes).OrderBy(s => s.Id).Last();
		}
	}
}
