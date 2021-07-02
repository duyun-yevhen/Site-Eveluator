using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Model;

namespace WebCrawler.Service
{
	public class SiteCrawlerService
	{

		private readonly SiteCrawlerWorker _siteCrawlerWorker;
		private readonly DbWorker _dbWorker;

		public SiteCrawlerService(DbWorker dbWorker, SiteCrawlerWorker siteCrawlerWorker)
		{
			_dbWorker = dbWorker;
			_siteCrawlerWorker = siteCrawlerWorker;
		}

		public virtual async Task<int> GetSitePefrormanseAsync(Uri url)
		{
			var siteResult =  _siteCrawlerWorker.DoWork(url, 250); 
			 
			return await _dbWorker.SaveResultAsync(url, siteResult);
		}

		public virtual async Task<PerformanceTest> GetResultsByTestIdAsync(int testID)
		{
			var result = await _dbWorker.GetResultsByTestIdAsync(testID);

			return result;
		}

		public async Task<IEnumerable<PerformanceTest>> GetTestsAsync()
		{
			var result = await _dbWorker.GetTestsAsync();

			return result;
		}
	}
}
