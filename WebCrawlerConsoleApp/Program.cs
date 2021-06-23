using Microsoft.Extensions.Hosting;
using WebCrawler.Model;

namespace WebCrawler.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var Crawler = new SiteCrawlerConsoleAppUI();
			Crawler.DoWork();
		}

		//public static IHostBuilder CreateHostBuilder(string[] args) =>
		//		Host.CreateDefaultBuilder(args)
		//			.ConfigureServices((hostContext, services) =>
		//			{
		//				services.AddEfRepository<WebCrawlerDbContext>(options => options.UseSqlServer(@"Server=localhost\MSSQLSERVER01;Database=Test;Trusted_Connection=True"));
		//				services.AddScoped<DbWorker>();
		//				services.AddScoped<WebCrawlerApp>();
		//			}).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
	
	public void ConfigurateDB()
		{
		//	string connectionString = Resources.ConnectionString;
		//	var optionsBuilder = new DbContextOptionsBuilder<WebCrawlerDbContext>();
		//	var options = optionsBuilder.UseSqlServer(connectionString).Options;
		//	var db = new WebCrawlerDbContext(options);
		}
	}
}
