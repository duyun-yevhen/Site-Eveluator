using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Model;

namespace WebCrawler.WebApplication
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

		public async Task<List<UrlPerformanseTestResult>> DoWorkAsync(Uri url, int querydDelay = 500)
		{
			List<UrlPerformanseTestResult> results = null;
			await Task.Run(() =>
			{
				results = GetAllLinks(url);
				RequestUrlsForSetResponseTimes(results, querydDelay, 1000);
			});
			return results;
		}

		private List<UrlPerformanseTestResult> GetAllLinks(Uri url)
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
		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		private void RequestUrlsForSetResponseTimes(List<UrlPerformanseTestResult> urls, int querydDelay = 500, int timeout = 1000)
		{
			foreach (var link in urls)
			{
				link.ResponseTime = _siteRequest.GetUrlResponseTime(link.Url, timeout);
				Thread.Sleep(querydDelay);
			}
			urls.Sort((l, r) => l.ResponseTime.CompareTo(r.ResponseTime));
		}
	}
}
