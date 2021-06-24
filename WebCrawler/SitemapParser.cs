using System;
using System.Collections.Generic;
using System.Xml;

namespace WebCrawler.Logic
{
	public class SitemapParser
	{
		public virtual List<Uri> GetUrlsFromSitemapXML(string siteTXT)
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

			xmlSitemapList = doc.GetElementsByTagName("sitemap");

			foreach (XmlNode node in xmlSitemapList)
			{
				if (node["loc"] == null)
				{
					continue;
				}

				urlList.Add(new Uri(node["loc"].InnerText));
			}

			return urlList;
		}

		public virtual List<Uri> GetUrlsFromSitemapTXT(string siteXML)
		{
			string[] sites = siteXML.Split('\n');
			List<Uri> urlList = new List<Uri>(sites.Length);

			foreach (var t in sites)
			{
				urlList.Add(new Uri(t, UriKind.RelativeOrAbsolute));
			}

			return urlList;
		}

		public virtual List<Uri> GetSitemapsFromRobotsTxt(string site)
		{
			List<Uri> sitemaps = new List<Uri>();
			List<string> lines = new List<string>(site.Split());

			int i = lines.FindIndex(p => p.StartsWith("Sitemap:"));

			while (i + 1 < lines.Count && i > 0)
			{
				sitemaps.Add(new Uri(lines[i + 1], UriKind.RelativeOrAbsolute));
				i = lines.FindIndex(i + 1, p => p.StartsWith("Sitemap:"));
			} 

			return sitemaps;
		}
	}
}
