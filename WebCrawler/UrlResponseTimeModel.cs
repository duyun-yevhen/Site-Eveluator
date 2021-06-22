using System;

/// <summary>
/// helper class for storing url and response time
/// </summary> 
namespace WebCrawler
{
	public class UrlResponseTimeModel
	{
		public Uri Url { get; set; }
		public bool InSitemap { get; set; }
		public bool InSitePage { get; set; }

		public int ResponseTime { get; set; } = -1;
	}
}
