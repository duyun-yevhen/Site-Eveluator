using System;
using System.Collections.Generic;

namespace WebCrawlerWebAPI.Models
{
	public class PerformanceTestInfo
	{
		public int Id { get; set; }
		public Uri SiteUrl { get; set; }
		public DateTime Date { get; set; }
	}
}
