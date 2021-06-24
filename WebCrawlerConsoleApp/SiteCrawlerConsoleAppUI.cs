using System;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	public class SiteCrawlerConsoleAppUI
	{
		private readonly SiteCrawlerWorker _crawlerWorker = new SiteCrawlerWorker();
		private readonly DbWorker _dbWorker;
		private readonly int TIMEOUT = 300;

		public SiteCrawlerConsoleAppUI(DbWorker dbWorker)
		{
			_dbWorker = dbWorker;
		}

		public void DoWork()
		{
			while (true)
			{
				Console.WriteLine("Enter site URL:");
				string input = Console.ReadLine();
				if (string.IsNullOrEmpty(input))
					Environment.Exit(0);

				if (Uri.TryCreate(input, UriKind.Absolute, out Uri url))
				{
					Console.Clear();
					var result = _crawlerWorker.GetAllLinks(url);
					_crawlerWorker.RequestUrlsForSetResponseTimes(result, TIMEOUT);

					_crawlerWorker.PrintSitemapResult(result);
					_crawlerWorker.PrintSitePageResult(result);
					_crawlerWorker.PrintTotalResult(result);

					_dbWorker.SaveResultAsync(url, result).Wait();
				}
				else
				{
					Console.WriteLine("Wrong URL!");
				}

				Console.WriteLine();
			}
		}
	}
}
