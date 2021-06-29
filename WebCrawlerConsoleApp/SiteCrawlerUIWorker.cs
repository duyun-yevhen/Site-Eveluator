using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebCrawler.Logic;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	public class SiteCrawlerUIWorker
	{
		private readonly SiteCrawlerWorker _siteCrawlerWorker;

		public SiteCrawlerUIWorker(Logic.SiteCrawlerWorker siteCrawlerWorker)
		{
			_siteCrawlerWorker = siteCrawlerWorker;
		}

		public IEnumerable<UrlPerformanseTestResult> DoWork(Uri url)
		{
			return _siteCrawlerWorker.DoWorkAsync(url).Result;
		}

		public void PrintSitemapResult(IEnumerable<UrlPerformanseTestResult> result)
		{
			Console.WriteLine("Urls FOUNDED IN SITEMAP but not founded after crawling a web site:");

			var links = result.Where(s => s.InSitemap && !s.InSitePage).ToList();
			for (int i = 0; i < links.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {links[i].Url}");
			}
		}

		public void PrintSitePageResult(IEnumerable<UrlPerformanseTestResult> result)
		{
			Console.WriteLine("\r\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:");
			var links = result.Where(s => !s.InSitemap && s.InSitePage).ToList();
			for (int i = 0; i < links.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {links[i].Url}");
			}
		}

		public void PrintTotalResult(IEnumerable<UrlPerformanseTestResult> result)
		{
			var links = result.ToList();
			Console.WriteLine("\r\nTiming:");
			for (int i = 0; i < links.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {links[i].ResponseTime} {links[i].Url}");
			}

			Console.WriteLine($"\r\nUrls(html documents) found after crawling a website: {result.Where(s => s.InSitePage).Count()}\r\n" +
								$"Urls found in sitemap: {result.Where(s => s.InSitemap).Count()}");
		}
	}
}
