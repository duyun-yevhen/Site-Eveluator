using System;
using System.Collections.Generic;

namespace WebCrawlerWebAPI.Models
{
	public class PerformanceTest
	{
		public int Id { get; set; }
		public Uri SiteUrl { get; set; }
		public DateTime Date { get; set; }
		public ICollection<PerformanceResult> UrlTestResults { get; set; }
	}
}
