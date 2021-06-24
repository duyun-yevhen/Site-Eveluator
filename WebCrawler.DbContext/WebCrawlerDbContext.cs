using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace WebCrawler.Model
{
	public class WebCrawlerDbContext : DbContext, IEfRepositoryDbContext
	{
		public DbSet<PerformanceTest> PerformanceTests { get; set; }
		public DbSet<UrlPerformanseTestResult> UrlResponseTimes { get; set; }

		public WebCrawlerDbContext(DbContextOptions<WebCrawlerDbContext> options)
		: base(options)
		{
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebCrawlerDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
