using System;
using System.Collections.Generic;

namespace WebCrawler.Logic
{
	/// <summary>
	/// Class for evaluating a website performance by crawling page.
	/// Also get response time for all found Url.
	/// </summary>
	public class SitemapCrawler
	{
		public SitemapParser parser = new SitemapParser();
		public SiteRequest siteRequest = new SiteRequest();

		public virtual List<Uri> GetSitemaps(Uri url)
		{
			return parser.GetSitemapsFromRobotsTxt(siteRequest.DownloadSite(new Uri(url + "robots.txt")));
		}

		public virtual List<Uri> GetSitesFromSitemap(List<Uri> sitemaps)
		{
			List<Uri> sitemapUrls = new List<Uri>();
			foreach (var map in sitemaps)
			{
				string site = siteRequest.DownloadSite(map);
				if (map.ToString().EndsWith(".xml")|| map.ToString().EndsWith(".xml.gz"))
				{
					sitemapUrls.AddRange(parser.GetUrlsFromSitemapXML(site));
				}
				else
				{
					sitemapUrls.AddRange(parser.GetUrlsFromSitemapTXT(site));
				}
			}

			return sitemapUrls;
		}
	}
}
