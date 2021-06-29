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

		public virtual async Task<int> SaveResultAsync(Uri siteUrl, IEnumerable<UrlPerformanseTestResult> urlResponseTimes)
		{
			var test = new PerformanceTest() { SiteUrl = siteUrl };

			_testsRepository.Add(test);

			await _testsRepository.SaveChangesAsync();

			_urlResponseTimeRepository.AddRange(urlResponseTimes.Select(p => { p.TestID = test.Id; return p; }).OrderBy(s=>s.ResponseTime));

			await _urlResponseTimeRepository.SaveChangesAsync();

			return test.Id;
		}

		public virtual async Task<PerformanceTest> GetResultsByTestIDAsync(int testID)
		{
			PerformanceTest result = null;

			await Task.Run(() =>
			{
				result = _testsRepository.Include(p => p.UrlTestResults).FirstOrDefault(s => s.Id == testID);
			});

			return result;
		}

		public virtual async Task<IEnumerable<PerformanceTest>> GetTestsAsync()
		{
			IEnumerable<PerformanceTest> result = null;

			await Task.Run(() =>
			{
				result = _testsRepository.GetAll().ToList();
			});
			return result;
		}

		public virtual PerformanceTest GetLastTest()
		{
			return _testsRepository.Include(p => p.UrlTestResults).OrderBy(s => s.Id).Last();
		}
	}
}
