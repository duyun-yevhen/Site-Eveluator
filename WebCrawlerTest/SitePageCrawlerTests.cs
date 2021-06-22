using Moq;
using System;
using WebCrawler.Logic;
using Xunit;

namespace WebCrawler.Tests
{
	public class SitePageCrawlerTests
	{


		[Fact]
		public void SiteCrawler_FindchildrensLinksOnPage()
		{
			// arrange
			var pasrserMock = new Mock<SitePageParser>();
			var reqesterMock = new Mock<SiteRequest>();
			var siteCrawler = new SitepageCrawler(pasrserMock.Object, reqesterMock.Object);

			// act
			var actual = siteCrawler.FindPageChildrenLinks(new Uri("http://test.com"));

			// assert
			pasrserMock.Verify(a => a.ParseAllChildrenLinks(It.IsAny<string>(), It.IsAny<Uri>()), Times.Once());
			reqesterMock.Verify(a => a.DownloadSite(It.IsAny<Uri>(), It.IsAny<int>()), Times.Once());
		}
	}
}
