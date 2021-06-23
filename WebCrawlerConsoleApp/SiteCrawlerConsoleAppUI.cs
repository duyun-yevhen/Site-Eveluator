using Microsoft.EntityFrameworkCore;
using System;
using WebCrawler.ConsoleApp.Properties;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	public class SiteCrawlerConsoleAppUI
	{
		private readonly SiteCrawlerWorker _crawlerWorker = new SiteCrawlerWorker();
		private readonly int TIMEOUT = 300;

		public void DoWork()
		{
			while (true)
			{
				Console.WriteLine("Enter site URL:");
				if (Uri.TryCreate(Console.ReadLine(), UriKind.Absolute, out Uri url))
				{
					Console.Clear();
					var result = _crawlerWorker.GetAllLinks(url);
					_crawlerWorker.RequestUrlsForSetResponseTimes(result, TIMEOUT);

					_crawlerWorker.PrintSitemapResult(result);
					_crawlerWorker.PrintsitePageResult(result);
					_crawlerWorker.PrintTotalResult(result);
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
