using System;
using System.Collections.Generic;

namespace WebCrawler.Logic
{
	public class SitePageParser
	{
		public virtual List<Uri> ParseAllChildrenLinks(string site, Uri siteUrl)
		{
			List<Uri> urlList = new List<Uri>();
			int pos = 0;

			while (true)
			{
				pos = site.IndexOf("<a ", ++pos);
				if (pos <= 0)
				{
					break;
				}

				string link = site[pos..(site.IndexOf(">", pos) + 1)];
				int linkStart = link.IndexOf("href=");

				if (linkStart < 0)
				{
					continue;
				}

				link = link.Replace("'", "\"");
				linkStart = link.IndexOf("\"", linkStart) + 1;
				int linkEnd = link.IndexOf("\"", linkStart);
				string href = link[linkStart..linkEnd].Trim();

				Uri pageUrl = new Uri(href, UriKind.RelativeOrAbsolute);

				if (!pageUrl.IsAbsoluteUri)
				{
					pageUrl = new Uri(siteUrl, href);
				}

				if (pageUrl.Scheme == Uri.UriSchemeHttps || pageUrl.Scheme == Uri.UriSchemeHttp)
				{
					if (!urlList.Contains(pageUrl))
					{
						urlList.Add(pageUrl);
					}
				}
			}
			return urlList;
		}
	}
}
