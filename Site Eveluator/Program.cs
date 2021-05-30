using System;

namespace SiteEvaluating
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Enter site URL:");
				Uri url;
				if(Uri.TryCreate(Console.ReadLine(), UriKind.Absolute,out url))
				{
					SiteEvaluating site = new SiteEvaluating(url);
					Console.Clear();
					if(site.sitemapUrls.Count > 0)
					{
						Console.WriteLine("Urls FOUNDED IN SITEMAP but not founded after crawling a web site:");
						for (int i = 0; i < site.sitemapUrlsNotCrawling.Count; i++)
							Console.WriteLine($"{i + 1}) {site.sitemapUrlsNotCrawling[i]}");
					}
					else
					Console.WriteLine("Couldn't found SITEMAP");
					Console.WriteLine("\r\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml");
					for (int i = 0; i < site.crawlingNotSitemapUrls.Count; i++)
						Console.WriteLine($"{i + 1}) {site.crawlingNotSitemapUrls[i]}");

					Console.WriteLine("\r\nTiming");
					for (int i = 0; i < site.allUrls.Count; i++)
						Console.WriteLine($"{i + 1}) {site.allUrls[i].responseTime} {site.allUrls[i].url}");

					Console.WriteLine($"\r\nUrls(html documents) found after crawling a website: {site.crawlingUrls.Count} \r\nUrls found in sitemap: {site.sitemapUrls.Count}");
				}
				else
					Console.WriteLine("Wrong URL!");

				Console.WriteLine();
			}
		}

	}
}
