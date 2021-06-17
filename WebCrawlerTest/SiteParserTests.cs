using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using System.Xml;

namespace WebCrawler.Tests
{
	public class SiteParserTests
	{
		[Fact]
		public void SiteParser_ParseAllHtmlDocumentsFromSite()
		{
			// arrange
			var parser = new SiteParser();
			string site = "<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">\n" +
							"<html>\n" +
							"<head>\n" +
							"<meta http-equiv=\"content - type\" content=\"text / html; charset = utf - 8\">\n" +
							"<title>Ссылки на странице</title>\n" +
							"</head>\n" +
							"<body>\n" +
							"<p><a href=\"http://test.com/dog.html\">Собаки</a></p>\n" +
							"<p><a href=\"cat.html\">Кошки</a></p>\n" +
							"<p><link href=\"cat.html\">Кошки</a></p>\n" +
							"<p><link href=\"http://test.com/dog.html\"/a></p>\n" +
							"</body>\n" +
							"</html>";
			List<Uri> expected = new List<Uri>
			{
				new Uri("http://test.com/dog.html"),
				new Uri("http://test.com/cat.html"),
			};

			// act
			List<Uri> actual = parser.ParseAllSite(site, new Uri("http://test.com/"));
			// assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SiteParser_ParseSitemapsFromRobotsTxt()
		{
			// arrange
			var parser = new SiteParser();
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
			List<Uri> actual = parser.GetSitemapFromRobotsTxt(robotsTxt);
			// assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SiteParser_ParseUrlFromSitemapTXT()
		{
			// arrange
			var parser = new SiteParser();
			string siteMap = "http://test.com/dog.html \n" +
							"http://test.com/cat.html";
			List<Uri> expected = new List<Uri>
			{
				new Uri("http://test.com/dog.html"),
				new Uri("http://test.com/cat.html"),
			};

			// act
			List<Uri> actual = parser.GetUrlFromSitemapTXT(siteMap);
			// assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SiteParser_ParseUrlFromSitemapXml()
		{
			// arrange
			var parser = new SiteParser();
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
			List<Uri> actual = parser.GetUrlFromSitemapXML(siteMap);
			// assert
			Assert.Equal(expected, actual);
		}
	}
}
