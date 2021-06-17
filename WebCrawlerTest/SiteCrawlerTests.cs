using System;
using System.Net;
using Xunit;

namespace WebCrawler.Tests
{
	public class SiteCrawlerTests
	{
		[Fact]
		public void SiteCrawler_FindChildrenUrl()
		{

			// arrange

			// act

			// assert
			Assert.Equal(HttpStatusCode.OK, new SiteCrawler().GetResponse(new Uri("https://github.com/duyun-yevhen/Site-Eveluator")).StatusCode);
		}

		[Fact]
		public void SiteCrawler_GetSitemap()
		{
			// arrange

			// act

			// assert
		}

		[Fact]
		public void SiteCrawler_GetSiteResponseTime()
		{
			// arrange

			// act

			// assert
		}

		[Fact]
		public void SiteCrawler_GetAllSitesResponseTime()
		{
			// arrange
			
			// act

			// assert
		}
	}
}
