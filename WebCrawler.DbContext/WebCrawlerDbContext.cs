using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Model;

namespace WebCrawler.Model
{
	public class WebCrawlerDbContext : DbContext
	{
		public DbSet<PerformanceTest> PerformanceTests { get; set; }
		public DbSet<UrlResponseTime> UrlResponseTimes { get; set; }



		public WebCrawlerDbContext()
		{
			
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test;Trusted_Connection=True;");
		}
	}
}
