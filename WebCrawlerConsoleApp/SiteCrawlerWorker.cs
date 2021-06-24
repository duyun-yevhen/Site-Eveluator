using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebCrawler.Logic;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	public class SiteCrawlerWorker
	{
		private readonly SiteRequest _siteRequest;
		private readonly SitepageCrawler _sitepageCrawler;
		private readonly SitemapCrawler _sitemapCrawler;

		public SiteCrawlerWorker()
		{
			_siteRequest = new SiteRequest();
			_sitepageCrawler = new SitepageCrawler(new SitePageParser(), _siteRequest);
			_sitemapCrawler = new SitemapCrawler(new SitemapParser(), _siteRequest);
		}

		public List<UrlPerformanseTestResult> GetAllLinks(Uri url)
		{
			var sitemapLinks = _sitemapCrawler.GetSitesFromSitemap(_sitemapCrawler.GetSitemaps(new Uri("http://" + url.Host)));
			var pageLinks = _sitepageCrawler.FindPageChildrenLinks(url);
			var result = new List<UrlPerformanseTestResult>();

			foreach (var link in sitemapLinks.Union(pageLinks))
			{
				result.Add(new UrlPerformanseTestResult() { Url = link, InSitemap = sitemapLinks.Contains(link), InSitePage = pageLinks.Contains(link) });
			}

			return result;
		}

		public void PrintSitemapResult(List<UrlPerformanseTestResult> result)
		{
			Console.WriteLine("Urls FOUNDED IN SITEMAP but not founded after crawling a web site:");

			var links = result.Where(s => s.InSitemap && !s.InSitePage).ToList();
			for (int i = 0; i < links.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {links[i].Url}");
			}
		}

		public void PrintSitePageResult(List<UrlPerformanseTestResult> result)
		{
			Console.WriteLine("\r\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:");
			var links = result.Where(s => !s.InSitemap && s.InSitePage).ToList();
			for (int i = 0; i < links.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {links[i].Url}");
			}
		}

		public void PrintTotalResult(List<UrlPerformanseTestResult> result)
		{
			Console.WriteLine("\r\nTiming:");
			for (int i = 0; i < result.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {result[i].ResponseTime} {result[i].Url}");
			}

			Console.WriteLine($"\r\nUrls(html documents) found after crawling a website: {result.Where(s => s.InSitePage).Count()}\r\n" +
								$"Urls found in sitemap: {result.Where(s => s.InSitemap).Count()}");
		}

		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		public void RequestUrlsForSetResponseTimes(List<UrlPerformanseTestResult> urls, int querydDelay = 500, int timeout = 10000)
		{
			int i = 1;
			foreach (var link in urls)
			{
				Console.WriteLine($"{i++}/{urls.Count} {link.Url}");
				link.ResponseTime = _siteRequest.GetUrlResponseTime(link.Url, timeout);
				Thread.Sleep(querydDelay);
			}

			Console.Clear();
			urls.Sort((l, r) => l.ResponseTime.CompareTo(r.ResponseTime));
		}
	}
}
