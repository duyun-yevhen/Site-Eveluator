using Microsoft.EntityFrameworkCore;
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
		private readonly IRepository<PerformanseResult> _urlResponseTimeRepository;

		public DbWorker(IRepository<PerformanceTest> testsRepository, IRepository<PerformanseResult> urlResponseTimeRepository)
		{
			_testsRepository = testsRepository;
			_urlResponseTimeRepository = urlResponseTimeRepository;
		}

		public virtual async Task<int> SaveResultAsync(Uri siteUrl, IEnumerable<PerformanseResult> urlResponseTimes)
		{
			var test = new PerformanceTest() { SiteUrl = siteUrl };

			_testsRepository.Add(test);

			await _testsRepository.SaveChangesAsync();

			_urlResponseTimeRepository.AddRange(urlResponseTimes.Select(p => { p.TestId = test.Id; return p; })
									  .OrderBy(s => s.ResponseTime));

			await _urlResponseTimeRepository.SaveChangesAsync();

			return test.Id;
		}

		public virtual async Task<PerformanceTest> GetResultsByTestIdAsync(int testId)
		{
			return await _testsRepository.Include(s => s.UrlTestResults)
										 .FirstOrDefaultAsync(s => s.Id == testId);
		}

		public virtual async Task<IEnumerable<PerformanceTest>> GetTestsAsync()
		{
			return await _testsRepository.GetAll().ToListAsync();
		}

		public virtual async Task<PerformanceTest> GetLastTest()
		{
			return await _testsRepository.Include(p => p.UrlTestResults).OrderBy(s => s.Id).LastAsync();
		}
	}
}
