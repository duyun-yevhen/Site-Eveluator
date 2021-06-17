using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;

namespace WebCrawler
{
	/// <summary>
	/// Class for evaluating a website performance by crawling page.
	/// Also get response time for all found Url.
	/// </summary>
	public class SiteCrawler
	{
		public Uri StartUrl { get; private set; }
		public List<Uri> crawlingUrls;
		public List<Uri> sitemapUrls;
		public List<Uri> crawlingNotSitemapUrls;
		public List<Uri> sitemapUrlsNotCrawling;
		public List<UrlResponseTime> allUrlsTimes;

		private List<Uri> sitemaps;
		public SiteParser siteParser = new SiteParser();
		
		public SiteCrawler()
		{

		}

		public SiteCrawler(Uri url)
		{
			StartUrl = url;
			crawlingUrls = FindChildrenUrl(StartUrl);
			sitemaps = GetSitemaps(new Uri("http://"+ StartUrl.Host));
			sitemapUrls = GetSitesFromSitemaps(sitemaps);
			crawlingNotSitemapUrls = new List<Uri>(crawlingUrls.Where(p => !sitemapUrls.Contains(p)));
			sitemapUrlsNotCrawling = new List<Uri>(sitemapUrls.Where(p => !crawlingUrls.Contains(p)));
			allUrlsTimes = new List<UrlResponseTime>();
			foreach (var i in crawlingUrls)
				allUrlsTimes.Add(i);
			foreach (var i in sitemapUrlsNotCrawling)
				allUrlsTimes.Add(i);
			GetAllResponseTime(300, 1000);
		}

		public virtual HttpWebResponse GetResponse(Uri url, int timeout = 10000)
		{
			try
			{
				HttpWebRequest myHttwebrequest = (HttpWebRequest)WebRequest.Create(url);
				myHttwebrequest.Timeout = timeout;
				return (HttpWebResponse)myHttwebrequest.GetResponse();
			}
			catch(WebException e)
			{
				return (HttpWebResponse)e.Response;
			}
		}

		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		public virtual void GetAllResponseTime(int querydDelay, int timeout)
		{
			Stopwatch stopwatch;
			for (int i = 0; i < allUrlsTimes.Count; i++)
			{
				stopwatch = Stopwatch.StartNew();
				var response = GetResponse(allUrlsTimes[i].url);
				stopwatch.Stop();
				Console.WriteLine($"{i}/{allUrlsTimes.Count} {response.StatusCode} \n{allUrlsTimes[i].url}");
				allUrlsTimes[i].responseTime = stopwatch.ElapsedMilliseconds;
				Thread.Sleep(querydDelay);
			}
			allUrlsTimes.Sort();
		}

		/// <summary>
		/// Crawling by site and found all childrens url 
		/// </summary>
		/// <param name="url"></param>
		public virtual List<Uri> FindChildrenUrl(Uri url)
		{
			HttpWebResponse myHttpWebresponse = GetResponse(url);
			using StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(), true);
			return siteParser.ParseAllSite(strm.ReadToEnd(), url);
		}

		/// <summary>
		/// Get all Url from sitemaps if in exist
		/// </summary>
		/// <param name="url"></param>
		public virtual List<Uri> GetSitemaps(Uri url)
		{
			HttpWebResponse myHttpWebresponse = GetResponse(new Uri(url + "robots.txt"));
			using StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(),true);
			return siteParser.GetSitemapFromRobotsTxt(strm.ReadToEnd());
		}

		public virtual List<Uri> GetSitesFromSitemaps(List<Uri> sitemaps)
		{
			List<Uri> sitemapUrls = new List<Uri>();
			foreach (var map in sitemaps)
			{
				HttpWebResponse myHttpWebresponse = GetResponse(map);
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
