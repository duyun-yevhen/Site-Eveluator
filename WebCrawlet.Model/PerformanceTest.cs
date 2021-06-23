using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCrawler.Model
{
	public class PerformanceTest
	{
		[Key]
		public int TestId { get; set; }
		[MaxLength(2048)]
		public Uri SiteUrl { get; set; }
		public DateTime Date { get; set; }
		public List<UrlResponseTime> UrlResponseTimes { get; set; }
	}
}
