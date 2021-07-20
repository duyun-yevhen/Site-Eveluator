using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.Service;

namespace WebCrawlerWebAPI.Models
{
	public class TestsResult
	{
		public int Id { get; set; }
		public Uri SiteUrl { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<PerformanseResultModel> UrlTestResults { get; set; }
	}
}
