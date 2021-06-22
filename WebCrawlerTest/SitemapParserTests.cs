using System;
using System.Collections.Generic;
using WebCrawler.Logic;
using Xunit;

namespace WebCrawler.Tests
{
	public class SitemapParserTests
	{
		[Fact]
		public void SitemapParser_ParseSitemapsFromRobotsTxt()
		{
			// arrange
			var parser = new SitemapParser();
			string robotsTxt = "# Group 1\n" +
								"User-agent: Googlebot\n" +
								"Disallow: /nogooglebot/\n" +
								"# Group 2\n" +
								"User-agent: *\n" +
								"Allow: /\n" +
								"Sitemap: http://www.example.com/sitemap.xml \n" +
								"Sitemap: http://www.example.com/sitemap.txt";

			List<Uri> expected = new List<Uri>
			{
				new Uri("http://www.example.com/sitemap.xml"),
				new Uri("http://www.example.com/sitemap.txt"),
			};

			// act
			List<Uri> actual = parser.GetSitemapsFromRobotsTxt(robotsTxt);
			// assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SitemapParser_ParseUrlFromSitemapTXT()
		{
			// arrange
			var parser = new SitemapParser();
			string siteMap = "http://test.com/dog.html \n" +
							"http://test.com/cat.html";
			List<Uri> expected = new List<Uri>
			{
				new Uri("http://test.com/dog.html"),
				new Uri("http://test.com/cat.html"),
			};

			// act
			List<Uri> actual = parser.GetUrlsFromSitemapTXT(siteMap);
			// assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SitemapParser_ParseUrlFromSitemapXml()
		{
			// arrange
			var parser = new SitemapParser();
			string siteMap = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>\n" +
								"<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n" +
								"<url>\n" +
								"<loc>http://www.example.com/sitemap.xml</loc>\n" +
								"<lastmod>2015-10-03</lastmod>\n" +
								"<changefreq>monthly</changefreq>\n" +
								"<priority>1.0</priority>\n" +
								"</url>\n" +
								"<sitemap>\n" +
								"<loc>http://www.example.com/sitemap.txt</loc>\n" +
								"<lastmod>2018-10-03</lastmod>\n" +
								"<changefreq>monthly</changefreq>\n" +
								"<priority>1.0</priority>\n" +
								"</sitemap>\n" +
								"</urlset>";

			List<Uri> expected = new List<Uri>
			{
				new Uri("http://www.example.com/sitemap.xml"),
				new Uri("http://www.example.com/sitemap.txt"),
			};

			// act
			List<Uri> actual = parser.GetUrlsFromSitemapXML(siteMap);
			// assert
			Assert.Equal(expected, actual);
		}
	}
}
