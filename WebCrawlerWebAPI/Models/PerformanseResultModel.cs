using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Model;
using WebCrawler.Service;

namespace WebCrawlerWebAPI.Models
{
	public class PerformanseResultModel
	{
		public int Id { get; set; }
		public Uri Url { get; set; }
		public bool InSitemap { get; set; }
		public bool InSitePage { get; set; }
		public int ResponseTime { get; set; }
	}
}
