using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCrawler
{
	/// <summary>
	/// Class for evaluating a website performance by crawling page.
	/// Also get response time for all found Url.
	/// </summary>
	public class SiteCrawler
	{

		public SiteParser siteParser = new SiteParser();
		public SiteRequest siteRequest = new SiteRequest();

		public List<UrlResponseTime> FindAndRequireAllchildrenUrls(Uri startUrl)
		{
			List<Uri> childrenLinks = FindChildrenUrl(startUrl);
			List<Uri> sitemaps = GetSitemaps(new Uri("http://" + startUrl.Host));
			List<Uri> sitemapLinks = GetSitesFromSitemaps(sitemaps);

			List<UrlResponseTime> result = new List<UrlResponseTime>();
			foreach (var link in childrenLinks.Union(sitemapLinks).ToList())
			{
				result.Add(new UrlResponseTime() { Url = link, InSitemap = sitemapLinks.Contains(link), InSitePage = childrenLinks.Contains(link) });
			}

			GetAllResponseTime(result, 500, 10000);

			return result;
		}

		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		private void GetAllResponseTime(List<UrlResponseTime> urls, int querydDelay = 500, int timeout = 10000)
		{
			var allUrlsTimes = siteRequest.GetResponseTimes(urls, querydDelay, timeout);
			allUrlsTimes.Sort((l, r) => l.ResponseTime.CompareTo(r.ResponseTime));
		}

		/// <summary>
		/// Crawling by site and found all childrens url 
		/// </summary>
		/// <param name="url"></param>
		private List<Uri> FindChildrenUrl(Uri url)
		{
			return siteParser.ParseAllLink(siteRequest.DownloadSite(url), url);
		}

		/// <summary>
		/// Get all Url from sitemaps if in exist
		/// </summary>
		/// <param name="url"></param>
		private List<Uri> GetSitemaps(Uri url)
		{
			return siteParser.GetSitemapFromRobotsTxt(siteRequest.DownloadSite(new Uri(url + "robots.txt")));
		}

		private List<Uri> GetSitesFromSitemaps(List<Uri> sitemaps)
		{
			List<Uri> sitemapUrls = new List<Uri>();
			foreach (var map in sitemaps)
			{
				string site = siteRequest.DownloadSite(map);
				if (map.ToString().EndsWith(".xml"))
				{
					sitemapUrls.AddRange(siteParser.GetUrlFromSitemapXML(site));
				}
				else
				{
					sitemapUrls.AddRange(siteParser.GetUrlFromSitemapTXT(site));
				}
			}

			return sitemapUrls;
		}
	}
}
