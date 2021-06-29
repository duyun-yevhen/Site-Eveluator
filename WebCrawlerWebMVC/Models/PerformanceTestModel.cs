using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCrawler.WebApplication.Models
{
	public class PerformanceTestModel 
	{
		public int Id { get; set; }

		[MaxLength(2048)]
		public Uri SiteUrl { get; set; }
		public DateTime Date { get; set; }
		public List<UrlPerformanseTestResultModel> UrlResponseTimes { get; set; }
	}
}
