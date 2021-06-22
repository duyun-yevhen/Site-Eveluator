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
		public SitePageParser siteParser = new SitePageParser();
		public SiteRequest siteRequest = new SiteRequest();

		public virtual List<Uri> FindPageChildrenLinks(Uri pageUrl)
		{
			string page = siteRequest.DownloadSite(pageUrl);
			List<Uri> pageLinks = siteParser.ParseAllChildrenLinks(page, pageUrl);
			return pageLinks;
		}
	}
}
