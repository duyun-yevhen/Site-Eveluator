using System;
using System.Net;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WebCrawler.Tests
{
	public class SiteCrawlerTests
	{
		[Fact]
		public void SiteCrawler_FindAndRequireAllchildrenUrls_SouldBeNotNull()
		{
			// arrange
			var pasrserMock = new Mock<SiteParser>();
			var reqesterMock = new Mock<SiteRequest>();

			pasrserMock.Setup(a => a.GetSitemapFromRobotsTxt(It.IsAny<string>())).Returns(new List<Uri>());
			pasrserMock.Setup(a => a.ParseAllLink(It.IsAny<string>(), It.IsAny<Uri>())).Returns(new List<Uri>());
			reqesterMock.Setup(a => a.GetResponseTimes(It.IsAny<List<UrlResponseTime>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<UrlResponseTime>());

			SiteCrawler siteCrawler = new SiteCrawler();
			siteCrawler.siteParser = pasrserMock.Object;
			siteCrawler.siteRequest = reqesterMock.Object;
			// act
			var actual = siteCrawler.FindAndRequireAllchildrenUrls(new Uri("http://test.com"));
			// assert
			Assert.NotNull(actual);
		}
	}
}
