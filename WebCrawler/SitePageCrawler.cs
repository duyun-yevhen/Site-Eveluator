using System;
using System.Collections.Generic;

namespace WebCrawler.Logic
{
	/// <summary>
	/// Class for evaluating a website performance by crawling page.
	/// Also get response time for all found Url.
	/// </summary>
	public class SitepageCrawler
	{
		private readonly SitePageParser _siteParser;
		private readonly SiteRequest _siteRequest;

		public SitepageCrawler(SitePageParser siteParser, SiteRequest siteRequest)
		{
			_siteParser = siteParser;
			_siteRequest = siteRequest;
		}

		public virtual List<Uri> FindPageChildrenLinks(Uri pageUrl)
		{
			string page = _siteRequest.DownloadSite(pageUrl);
			List<Uri> pageLinks = _siteParser.ParseAllChildrenLinks(page, pageUrl);

			return pageLinks;
		}
	}
}
