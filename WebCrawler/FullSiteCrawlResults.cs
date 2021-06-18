using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler
{
	public class FullSiteCrawlResults
	{
		public List<Uri> UrlsFromSitemap { get; set; }
		public List<Uri> UrlsFromCrawling { get; set; }
		public Uri StartUrl { get; set; }
		public List<Uri> CrawlingNotSitemapUrls { get; set; }
		public List<Uri> SitemapUrlsNotCrawling { get; set; }
		public List<UrlResponseTime> AllUrlsWithResponsetime { get; set; }
	}
}
