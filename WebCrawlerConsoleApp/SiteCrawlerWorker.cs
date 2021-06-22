using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApp
{
	public class SiteCrawlerWorker
	{
		private SiteRequest siteRequest = new SiteRequest();
		private SitepageCrawler sitepageCrawler = new SitepageCrawler();
		private SitemapCrawler sitemapCrawler = new SitemapCrawler();

		public List<UrlResponseTimeModel> GetAllLinks(Uri url)
		{
			var sitemapLinks = sitemapCrawler.GetSitesFromSitemap(sitemapCrawler.GetSitemaps(new Uri("http://" + url.Host)));
			var pageLinks = sitepageCrawler.FindPageChildrenLinks(url);
			var result = new List<UrlResponseTimeModel>();
			foreach (var link in sitemapLinks.Union(pageLinks))
			{
				result.Add(new UrlResponseTimeModel() { Url = link, InSitemap = sitemapLinks.Contains(link), InSitePage = pageLinks.Contains(link) });
			}

			return result;
		}

		public void PrintSitemapResult(List<UrlResponseTimeModel> result)
		{
			Console.WriteLine("Urls FOUNDED IN SITEMAP but not founded after crawling a web site:");
			int c = 1;
			foreach (var link in result.Where(s => s.InSitemap && !s.InSitePage))
			{
				Console.WriteLine($"{c++}) {link.Url}");
			}
		}

		public void PrintsitePageResult(List<UrlResponseTimeModel> result)
		{
			int c = 1;
			Console.WriteLine("\r\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:");
			foreach (var link in result.Where(s => !s.InSitemap && s.InSitePage))
			{
				Console.WriteLine($"{c++}) {link.Url}");
			}
		}

		public void PrintTotalResult(List<UrlResponseTimeModel> result)
		{
			int c = 1;
			Console.WriteLine("\r\nTiming:");
			foreach (var link in result)
			{
				Console.WriteLine($"{c++}) {link.ResponseTime} {link.Url}");
			}

			Console.WriteLine($"\r\nUrls(html documents) found after crawling a website: {result.Where(s => s.InSitePage).Count()}\r\n" +
								$"Urls found in sitemap: {result.Where(s => s.InSitemap).Count()}");
		}

		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		public void RequestUrlsForSetResponseTimes(List<UrlResponseTimeModel> urls, int querydDelay = 500, int timeout = 10000)
		{
			Stopwatch stopwatch;
			int i = 1;
			foreach (var link in urls)
			{
				stopwatch = Stopwatch.StartNew();
				Console.WriteLine($"{i++}/{urls.Count} {link.Url}");
				siteRequest.GetUrlResponseTime(link.Url, timeout);
				stopwatch.Stop();
				link.ResponseTime = (int)stopwatch.ElapsedMilliseconds;
				Thread.Sleep(querydDelay);
			}

			Console.Clear();
			urls.Sort((l, r) => l.ResponseTime.CompareTo(r.ResponseTime));
		}
	}
}
