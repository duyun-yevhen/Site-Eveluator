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
		private readonly SitemapParser _parser = new SitemapParser();
		private readonly SiteRequest _siteRequest = new SiteRequest();

		public SitemapCrawler(SitemapParser sitemapParser, SiteRequest siteRequest)
		{
			_parser = sitemapParser;
			_siteRequest = siteRequest;
		}

		public virtual List<Uri> GetSitemaps(Uri url)
		{
			return _parser.GetSitemapsFromRobotsTxt(_siteRequest.DownloadSite(new Uri(url + "robots.txt")));
		}

		public virtual List<Uri> GetSitesFromSitemap(List<Uri> sitemaps)
		{
			List<Uri> sitemapUrls = new List<Uri>();
			foreach (var map in sitemaps)
			{
				string site = _siteRequest.DownloadSite(map);
				if (map.ToString().EndsWith(".xml")|| map.ToString().EndsWith(".xml.gz"))
				{
					sitemapUrls.AddRange(_parser.GetUrlsFromSitemapXML(site));
				}
				else
				{
					sitemapUrls.AddRange(_parser.GetUrlsFromSitemapTXT(site));
				}
			}

			return sitemapUrls;
		}
	}
}
