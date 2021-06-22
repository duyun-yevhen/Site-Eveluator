using System;
using System.Net;
using WebCrawler.Logic;
using Xunit;

namespace WebCrawler.Tests
{
	public class SiteRequestTests
	{
		[Fact]
		public void SiteRequest_GetResponse()
		{
			// arrange
			var siterquest = new SiteRequest();
			var page = new Uri("http://google.com");

			//act
			HttpWebResponse response = siterquest.GetPageResponse(page);

			//assert
			Assert.NotNull(response);
		}

		[Fact]
		public void SiteRequest_GetResponseTime()
		{
			// arrange
			var siterquest = new SiteRequest();
			var page = new Uri("http://google.com");

			//act
			int response = siterquest.GetUrlResponseTime(page);

			//assert
			Assert.True(response > 0);
		}

		[Fact]
		public void SiteRequest_DownloadSite()
		{
			// arrange
			var siterquest = new SiteRequest();
			var page = new Uri("http://google.com");

			//assert
			Assert.NotNull(siterquest.DownloadSite(page));
		}
	}
}
