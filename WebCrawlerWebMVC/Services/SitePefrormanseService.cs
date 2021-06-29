using System;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Model;

namespace WebCrawler.WebApplication.Services
{
	public class SitePefrormanseService
	{

		private readonly SiteCrawlerWorker _siteCrawlerWorker;
		private readonly DbWorker _dbWorker;

		public SitePefrormanseService(DbWorker dbWorker, SiteCrawlerWorker siteCrawlerWorker)
		{
			_dbWorker = dbWorker;
			_siteCrawlerWorker = siteCrawlerWorker;
		}

		public virtual async Task<int> GetSitePefrormanseAsync(Uri url)
		{
			var siteResult =  _siteCrawlerWorker.DoWorkAsync(url, 250);

			return await _dbWorker.SaveResultAsync(url, siteResult);
		}
	}
}
