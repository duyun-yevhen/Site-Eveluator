using System;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	public class SiteCrawlerConsoleAppUI
	{
		private readonly SiteCrawlerUIWorker _crawlerWorker;
		private readonly DbWorker _dbWorker;

		public SiteCrawlerConsoleAppUI(DbWorker dbWorker, SiteCrawlerUIWorker crawlerWorker)
		{
			_dbWorker = dbWorker;
			_crawlerWorker = crawlerWorker;
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
					var result = _crawlerWorker.DoWork(url);

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
