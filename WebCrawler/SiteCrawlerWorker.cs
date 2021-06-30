using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebCrawler.Model;

namespace WebCrawler.Logic
{
	public class SiteCrawlerWorker
	{
		private readonly SiteRequest _siteRequest;
		private readonly SitepageCrawler _sitepageCrawler;
		private readonly SitemapCrawler _sitemapCrawler;

		public SiteCrawlerWorker(SiteRequest siteRequest, SitepageCrawler sitepageCrawler, SitemapCrawler sitemapCrawler)
		{
			_siteRequest = siteRequest;
			_sitepageCrawler = sitepageCrawler;
			_sitemapCrawler = sitemapCrawler;
		}

		public  IEnumerable<PerformanseResult> DoWork(Uri url, int querydDelay = 500)
		{
			IEnumerable<PerformanseResult> results = GetAllLinks(url);
			RequestUrlsForSetResponseTimes(results, querydDelay, 1000);

			return results;
		}

		private IEnumerable<PerformanseResult> GetAllLinks(Uri url)
		{
			var sitemapLinks = _sitemapCrawler.GetSitesFromSitemap(_sitemapCrawler.GetSitemaps(new Uri("http://" + url.Host)));
			var pageLinks = _sitepageCrawler.FindPageChildrenLinks(url);
			var result = new List<PerformanseResult>();

			foreach (var link in sitemapLinks.Union(pageLinks))
			{
				result.Add(new PerformanseResult() 
				{ 
					Url = link,
					InSitemap = sitemapLinks.Contains(link), 
					InSitePage = pageLinks.Contains(link) 
				});
			}

			return result;
		}
		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		private void RequestUrlsForSetResponseTimes(IEnumerable<PerformanseResult> urls, int querydDelay = 100, int timeout = 1000)
		{
			foreach (var link in urls)
			{
				link.ResponseTime = _siteRequest.GetUrlResponseTime(link.Url, timeout);
				Thread.Sleep(querydDelay);
			}
			urls.ToList().Sort((l, r) => l.ResponseTime.CompareTo(r.ResponseTime));
		}
	}
}
