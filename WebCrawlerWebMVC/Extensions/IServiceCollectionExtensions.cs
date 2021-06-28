using Microsoft.Extensions.DependencyInjection;
using WebCrawler.Logic;
using WebCrawler.Model;

namespace WebCrawler.WebApplication
{
    public static class IServiceCollectionExtension
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
		}
    }
}