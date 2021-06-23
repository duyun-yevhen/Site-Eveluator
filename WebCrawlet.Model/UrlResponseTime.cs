using System;
using System.ComponentModel.DataAnnotations;

namespace WebCrawler.Model
{
	public class UrlResponseTime
	{
		public int Id { get; set; }

		[MaxLength(2048)]
		public Uri Url { get; set; }
		public bool InSitemap { get; set; }
		public bool InSitePage { get; set; }
		public int ResponseTime { get; set; } 

		public int TestID { get; set; }
		public PerformanceTest Test { get; set; }
	}
}
