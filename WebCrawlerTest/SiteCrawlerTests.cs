using System;
using System.Net;
using Xunit;
using Moq;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace WebCrawler.Tests
{
	public class SiteCrawlerTests
	{
		[Fact]
		public void SiteCrawler_GetSiteResponse()
		{
			// arrange
			SiteCrawler crawler = new SiteCrawler();
			// act
			HttpWebResponse webResponse = crawler.GetResponse(new Uri("http://google.com"),1000);
			// assert
			Assert.NotNull(webResponse);
		}

		[Fact]
		public void SiteCrawler_GetAllResponseTime()
		{
			// arrange
			SiteCrawler crawler = new SiteCrawler();
			crawler.allUrlsTimes = new List<UrlResponseTime>
			{
				new Uri("http://google.com"),
			};

			// act
			crawler.GetAllResponseTime(0,10);
			// assert
			Assert.True(0 <= crawler.allUrlsTimes[0].responseTime);
		}

		[Fact]
		public void SiteCrawler_FindChildrenUrlDocuments()
		{
			// arrange
			SiteCrawler crawler = new SiteCrawler();
			var parserMock = new Mock<SiteParser>();
			crawler.siteParser = parserMock.Object;
			// act
			List<Uri> urlList = crawler.FindChildrenUrl(new Uri("http://google.com"));
			//assert
			parserMock.Verify(a => a.ParseAllSite(It.IsNotNull<string>(), It.IsNotNull<Uri>()));
		}
	}
}
