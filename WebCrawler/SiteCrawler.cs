using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace WebCrawler
{
	/// <summary>
	/// Class for evaluating a website performance by crawling page.
	/// Also get response time for all found Url.
	/// </summary>
	public class SiteCrawler
	{
		public Uri StartUrl { get; private set; }
		private SiteParser siteParser = new SiteParser();
		
		public SiteCrawler(Uri url)
		{
			StartUrl = url;
		}

		public FullSiteCrawlResults FindAndRequireAllchildrenUrls()
		{
			FullSiteCrawlResults result = new FullSiteCrawlResults();
			result.UrlsFromCrawling = FindChildrenUrl(StartUrl);
			List<Uri> sitemaps = GetSitemaps(new Uri("http://" + StartUrl.Host));
			result.UrlsFromSitemap = GetSitesFromSitemaps(sitemaps);
			result.CrawlingNotSitemapUrls = new List<Uri>(result.UrlsFromCrawling.Where(p => !result.UrlsFromSitemap.Contains(p)));
			result.SitemapUrlsNotCrawling = new List<Uri>(result.UrlsFromSitemap.Where(p => !result.UrlsFromCrawling.Contains(p)));
			List<Uri> allUrls = new List<Uri>();
			foreach (var i in result.UrlsFromCrawling)
				allUrls.Add(i);
			foreach (var i in result.SitemapUrlsNotCrawling)
				allUrls.Add(i);
			result.AllUrlsWithResponsetime = GetAllResponseTime(allUrls, 300, 10000);
			return result;
		}

		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		private List<UrlResponseTime> GetAllResponseTime(List<Uri> urls, int querydDelay, int timeout)
		{
			var allUrlsTimes = SiteRequest.GetResponseTimes(urls, querydDelay, timeout);
			allUrlsTimes.Sort();
			return allUrlsTimes;
		}

		/// <summary>
		/// Crawling by site and found all childrens url 
		/// </summary>
		/// <param name="url"></param>
		private List<Uri> FindChildrenUrl(Uri url)
		{
			HttpWebResponse myHttpWebresponse = SiteRequest.GetResponse(url);
			using StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(), true);
			return siteParser.ParseAllSite(strm.ReadToEnd(), url);
		}

		/// <summary>
		/// Get all Url from sitemaps if in exist
		/// </summary>
		/// <param name="url"></param>
		private List<Uri> GetSitemaps(Uri url)
		{
			HttpWebResponse myHttpWebresponse = SiteRequest.GetResponse(new Uri(url + "robots.txt"));
			using StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(), true);
			return siteParser.GetSitemapFromRobotsTxt(strm.ReadToEnd());
		}

		private List<Uri> GetSitesFromSitemaps(List<Uri> sitemaps)
		{
			List<Uri> sitemapUrls = new List<Uri>();
			foreach (var map in sitemaps)
			{
				HttpWebResponse myHttpWebresponse = SiteRequest.GetResponse(map);
				if (myHttpWebresponse.StatusCode != HttpStatusCode.OK)
				{
					continue;
				}
				using (StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(), true))
				{
					string site;
					if (map.ToString().EndsWith(".gz"))
					{
						using GZipStream gZipStream = new GZipStream(strm.BaseStream, CompressionMode.Decompress);
						using StreamReader siteStream = new StreamReader(gZipStream);
						site = siteStream.ReadToEnd();
					}
					else
					{
						site = strm.ReadToEnd();
					}

					if (map.ToString().EndsWith(".xml"))
					{
						sitemapUrls.AddRange(siteParser.GetUrlFromSitemapXML(site));
					}
					else
					{
						sitemapUrls.AddRange(siteParser.GetUrlFromSitemapTXT(site));
					}
				}
			}
			return sitemapUrls;
		}
	}
}
