using System;
using System.Collections.Generic;
using System.Xml;

namespace WebCrawler
{
	public class SiteParser
	{
		public virtual List<Uri> ParseAllSite(string site, Uri siteUrl)
		{
			List<Uri> urlList = new List<Uri>();
			int pos = 0;
			char c;
			while (true)
			{
				pos = site.IndexOf("href", pos + 1);
				if (pos <= 0)
					break;
				int s = site.IndexOf('\'', pos);
				int l = site.IndexOf('\"', pos);
				if (s != -1 && l != -1)
					c = s < l ? '\'' : '\"';
				else
					c = s > l ? '\'' : '\"';

				s = site.IndexOf(c, pos) + 1;
				l = site.IndexOf(c, s);
				string href = site[s..l];

				Uri temp = new Uri(siteUrl, href);

				if ((temp.Scheme == Uri.UriSchemeHttps || temp.Scheme == Uri.UriSchemeHttp) && !urlList.Contains(temp))
					urlList.Add(temp);
			}
			return urlList;
		}


		public virtual List<Uri> GetUrlFromSitemapXML(string siteTXT)
		{
			List<Uri> urlList = new List<Uri>();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(siteTXT);
			XmlNodeList xmlSitemapList = doc.GetElementsByTagName("url");
			foreach (XmlNode node in xmlSitemapList)
			{
				if (node["loc"] != null)
				{
					urlList.Add(new Uri(node["loc"].InnerText));
				}
			}
			return urlList;
		}

		public virtual List<Uri> GetUrlFromSitemapTXT(string siteXML)
		{
			string[] sites = siteXML.Split('\n');
			List<Uri> urlList = new List<Uri>(sites.Length);
			foreach (var t in sites)
			{
				urlList.Add(new Uri(t, UriKind.RelativeOrAbsolute));
			}

			return urlList;
		}

		public virtual List<Uri> GetSitemapFromRobotsTxt(string site)
		{
			List<Uri> sitemaps = new List<Uri>();
			List<string> lines = new List<string>(site.Split());
			
			for(int i = lines.FindIndex(p => p.StartsWith("Sitemap:")); i +1 < lines.Count && i>0; i = lines.FindIndex(i + 1,p => p.StartsWith("Sitemap:")))
			{
				sitemaps.Add(new Uri(lines[i + 1], UriKind.RelativeOrAbsolute));
			}

			return sitemaps;
		}
	}
}
