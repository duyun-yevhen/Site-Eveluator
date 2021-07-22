using System;

namespace WebCrawlerWebAPI.Models
{
	public class PerformanceResult
	{
		public Uri Url { get; set; }
		public bool InSitemap { get; set; }
		public bool InSitePage { get; set; }
		public int ResponseTime { get; set; }
	}
}
