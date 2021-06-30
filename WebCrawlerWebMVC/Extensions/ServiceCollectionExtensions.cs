using Microsoft.Extensions.DependencyInjection;
using WebCrawler.Logic;
using WebCrawler.Model;
using WebCrawler.Service;

namespace WebCrawler.WebApplication
{
    public static class ServiceCollectionExtension
    {
        public static void AddWebCrawlerLogicServices(this IServiceCollection services)
        {
			services.AddScoped<DbWorker>();
			services.AddScoped<SitepageCrawler>();
			services.AddScoped<SitemapParser>();
			services.AddScoped<SitemapCrawler>();
			services.AddScoped<SitePageParser>();
			services.AddScoped<SiteRequest>();
			services.AddScoped<SitepageCrawler>();
			services.AddScoped<SitemapCrawler>();
			services.AddScoped<SiteCrawlerService>();
		}
    }
}