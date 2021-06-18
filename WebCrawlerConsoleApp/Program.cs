using System;
using System.Collections.Generic;

namespace WebCrawler.Programm
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Enter site URL:");
				Uri url;
				if (Uri.TryCreate(Console.ReadLine(), UriKind.Absolute, out url))
				{
					SiteCrawler site = new SiteCrawler(url);
					FullSiteCrawlResults fullSiteCrawlResults = site.FindAndRequireAllchildrenUrls();
					Console.Clear();
					List<Uri> sitemapUrlsNotCrawling = fullSiteCrawlResults.SitemapUrlsNotCrawling;
					List<Uri> crawlingNotSitemapUrls = fullSiteCrawlResults.CrawlingNotSitemapUrls;
					List<UrlResponseTime> urlsResponseTimes = fullSiteCrawlResults.AllUrlsWithResponsetime;
					if (sitemapUrlsNotCrawling.Count > 0)
					{
						Console.WriteLine("Urls FOUNDED IN SITEMAP but not founded after crawling a web site:");
						for (int i = 0; i < sitemapUrlsNotCrawling.Count; i++)
							Console.WriteLine($"{i + 1}) {sitemapUrlsNotCrawling[i]}");
					}
					else
					{
						Console.WriteLine("Couldn't found SITEMAP");
					}

					if (crawlingNotSitemapUrls.Count > 0)
					{
						Console.WriteLine("\r\nUrls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml");
						for (int i = 0; i < crawlingNotSitemapUrls.Count; i++)
							Console.WriteLine($"{i + 1}) {crawlingNotSitemapUrls[i]}");
					}
					else
					{
						Console.WriteLine("Couldn't found any URL on SITE");
					}

					if (urlsResponseTimes.Count > 0)
					{
						Console.WriteLine("\r\nTiming");
						for (int i = 0; i < urlsResponseTimes.Count; i++)
							Console.WriteLine($"{i + 1}) {urlsResponseTimes[i].ResponseTime} {urlsResponseTimes[i].Url}");
					}

					Console.WriteLine($"\r\nUrls(html documents) found after crawling a website: {fullSiteCrawlResults.CrawlingUrls.Count} \r\nUrls found in sitemap: {fullSiteCrawlResults.SitemapUrls.Count}");
				}
				else
					Console.WriteLine("Wrong URL!");

				Console.WriteLine();
			}
		}

	}
}
