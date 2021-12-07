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

		public  IEnumerable<PerformanceResult> DoWork(Uri url, int querydDelay = 500)
		{
			IEnumerable<PerformanceResult> results = GetAllLinks(url);
			RequestUrlsForSetResponseTimes(results, 100, 1000);

			return results;
		}

		private IEnumerable<PerformanceResult> GetAllLinks(Uri url)
		{
			var sitemapLinks = _sitemapCrawler.GetSitesFromSitemap(_sitemapCrawler.GetSitemaps(new Uri("http://" + url.Host)));
			var pageLinks = new Queue<Uri>();

			Queue<Uri> queuedLinks = new Queue<Uri>();
			queuedLinks.Enqueue(url);
			

			var result = new List<PerformanceResult>();

			while (queuedLinks.Count > 0)
			{
				var links = _sitepageCrawler.FindPageChildrenLinks(queuedLinks.Dequeue());
				var uniqueLinks = links.Where(_ => pageLinks.Contains(_) == false);

				foreach(var link in uniqueLinks)
                {
					queuedLinks.Enqueue(link);
					pageLinks.Enqueue(link);
				}
				
			}

			foreach (var link in sitemapLinks.Union(pageLinks))
			{
				result.Add(new PerformanceResult() 
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
		private void RequestUrlsForSetResponseTimes(IEnumerable<PerformanceResult> urls, int querydDelay = 100, int timeout = 1000)
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
