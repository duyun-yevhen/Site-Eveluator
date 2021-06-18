using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCrawler.Programm
{
	public class SiteCrawlerConsoleAppUI
	{
		public void DoWork()
		{
			while (true)
			{
				Console.WriteLine("Enter site URL:");
				Uri url;
				if (Uri.TryCreate(Console.ReadLine(), UriKind.Absolute, out url))
				{
					SiteCrawler site = new SiteCrawler();
					List<UrlResponseTime> fullSiteCrawlResults = site.FindAndRequireAllchildrenUrls(url);
					Console.Clear();
					PrintResult(fullSiteCrawlResults);
				}
				else
				{
					Console.WriteLine("Wrong URL!");
				}

				Console.WriteLine();
			}
		}

		private void PrintResult(List<UrlResponseTime> fullSiteCrawlResults)
		{
			Console.WriteLine("Urls FOUNDED IN SITEMAP but not founded after crawling a web site:");

			int c = 1;
			foreach (var link in fullSiteCrawlResults.Where(s => s.InSitemap && !s.InSitePage))
			{
				Console.WriteLine($"{c++}) {link.Url}");
			}

			c = 1;
			Console.WriteLine("\r\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml");
			foreach (var link in fullSiteCrawlResults.Where(s => !s.InSitemap && s.InSitePage))
			{
				Console.WriteLine($"{c++}) {link.Url}");
			}

			if (fullSiteCrawlResults.Count > 0)
			{
				Console.WriteLine("\r\nTiming:");
				for (int i = 0; i < fullSiteCrawlResults.Count; i++)
				{
					Console.WriteLine($"{i + 1}) {fullSiteCrawlResults[i].ResponseTime} {fullSiteCrawlResults[i].Url}");
				}
			}

			Console.WriteLine($"\r\nUrls(html documents) found after crawling a website: {fullSiteCrawlResults.Where(s=>s.InSitePage).Count()}\r\n" +
								$"Urls found in sitemap: {fullSiteCrawlResults.Where(s=>s.InSitemap).Count()}");
		}
	}
}
