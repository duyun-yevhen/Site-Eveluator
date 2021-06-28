using Moq;
using System;
using System.Collections.Generic;
using WebCrawler.Logic;
using Xunit;

namespace WebCrawler.Tests
{
	public class SitemapCrawlerTests
	{
		[Fact]
		public void SitemapCrawler_FindSitemaps()
		{
			// arrange
			var paserMock = new Mock<SitemapParser>();
			var reqesterMock = new Mock<SiteRequest>();
			var sitemapCrawler = new SitemapCrawler(paserMock.Object, reqesterMock.Object);

			// act
			var actual = sitemapCrawler.GetSitemaps(new Uri("http://test.com"));

			// assert
			paserMock.Verify(a => a.GetSitemapsFromRobotsTxt(It.IsAny<string>()), Times.Once());
			reqesterMock.Verify(a => a.DownloadSite(It.IsAny<Uri>(), It.IsAny<int>()), Times.Once());
		}

		[Fact]
		public void SitemapCrawler_FindSitemapTXTLinks()
		{
			// arrange
			var parserMock = new Mock<SitemapParser>();
			var reqesterMock = new Mock<SiteRequest>();
			var sitemapCrawler = new SitemapCrawler(parserMock.Object, reqesterMock.Object);

			var expected = new List<Uri>() { new Uri("http://test.com/sitemapTXT") };
			parserMock.Setup(a => a.GetUrlsFromSitemapTXT(It.IsAny<string>())).Returns(expected);

			// act
			var actual = sitemapCrawler.GetSitesFromSitemap(new List<Uri>() { new Uri("http://test.com/sitemap.txt") });

			// assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SitemapCrawler_FindSitemapXMLLinks()
		{
			// arrange
			var parserMock = new Mock<SitemapParser>();
			var reqesterMock = new Mock<SiteRequest>();
			var sitemapCrawler = new SitemapCrawler(parserMock.Object, reqesterMock.Object);

			var expected = new List<Uri>() { new Uri("http://test.com/sitemapXML") };
			parserMock.Setup(a => a.GetUrlsFromSitemapXML(It.IsAny<string>())).Returns(expected);

			// act
			var actual = sitemapCrawler.GetSitesFromSitemap(new List<Uri>() { new Uri("http://test.com/sitemap.xml") });

			// assert
			Assert.Equal(expected, actual);
		}
	}
}
