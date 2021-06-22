using System;

namespace WebCrawler.ConsoleApp
{
	public class SiteCrawlerConsoleAppUI
	{
		SiteCrawlerWorker crawlerWorker = new SiteCrawlerWorker();
		public void DoWork()
		{
			while (true)
			{
				Console.WriteLine("Enter site URL:");
				if (Uri.TryCreate(Console.ReadLine(), UriKind.Absolute, out Uri url))
				{
					Console.Clear();
					var result = crawlerWorker.GetAllLinks(url);
					crawlerWorker.RequestUrlsForSetResponseTimes(result, 300);

					crawlerWorker.PrintSitemapResult(result);
					crawlerWorker.PrintsitePageResult(result);
					crawlerWorker.PrintTotalResult(result);
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
