using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			using IHost host = CreateHostBuilder(args).Build();
			var Crawler = host.Services.GetService<SiteCrawlerConsoleAppUI>();
			Crawler.DoWork();
			await host.RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddEfRepository<WebCrawlerDbContext>(options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True"));
				services.AddScoped<DbWorker>();
				services.AddScoped<SiteCrawlerConsoleAppUI>();
				services.AddScoped<SiteRequest>();

				services.AddScoped<SitemapParser>();
				services.AddScoped<SitemapCrawler>();
				services.AddScoped<SitepageCrawler>();
				services.AddScoped<SitePageParser>();

				services.AddScoped<SiteCrawlerWorker>();
				services.AddScoped<SiteCrawlerUIWorker>();
			}).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
		}
	}
}
