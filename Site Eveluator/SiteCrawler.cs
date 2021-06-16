using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml;

namespace SiteEvaluating
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
		public List<UrlResponseTime> allUrls;

		private List<Uri> sitemaps;

		public SiteCrawler(Uri url)
		{
			StartUrl = url;
			sitemaps = new List<Uri>();
			crawlingUrls = new List<Uri>();
			sitemapUrls = new List<Uri>();
			FindChildrenUrl(StartUrl);
			GetSitemaps(new Uri("http://" + StartUrl.Host));
			crawlingNotSitemapUrls = new List<Uri>(crawlingUrls.Where(p => !sitemapUrls.Contains(p)));
			sitemapUrlsNotCrawling = new List<Uri>(sitemapUrls.Where(p => !crawlingUrls.Contains(p)));
			allUrls = new List<UrlResponseTime>();
			foreach (var i in crawlingUrls)
				allUrls.Add(i);
			foreach (var i in sitemapUrlsNotCrawling)
				allUrls.Add(i);
			GetAllResponseTime(500, 10000);
		}

		/// <summary>
		/// Queries all found Url and gets a delay in ms
		/// </summary>
		/// <param name="querydDelay">Delay between requests</param>
		/// <param name="timeout">Maximum response timeout</param>
		public virtual void GetAllResponseTime(int querydDelay, int timeout)
		{
			Stopwatch stopwatch;
			for (int i = 0; i < allUrls.Count; i++)
			{
				stopwatch = Stopwatch.StartNew();
				try
				{
					HttpWebRequest myHttwebrequest = (HttpWebRequest)HttpWebRequest.Create(allUrls[i].url.OriginalString);
					myHttwebrequest.Timeout = timeout;
					HttpWebResponse myHttpWebresponse = (HttpWebResponse)myHttwebrequest.GetResponse();

				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
				stopwatch.Stop();
				Console.WriteLine($"{i}/{allUrls.Count}");
				allUrls[i].responseTime = stopwatch.ElapsedMilliseconds;
				Thread.Sleep(querydDelay);
			}
			allUrls.Sort();
		}

		/// <summary>
		/// Crawling by site and found all childrens url 
		/// </summary>
		/// <param name="url"></param>
		public virtual void FindChildrenUrl(Uri url)
		{
			HttpWebRequest myHttwebrequest = (HttpWebRequest)HttpWebRequest.Create(url);
			myHttwebrequest.Timeout = 10000;
			HttpWebResponse myHttpWebresponse = (HttpWebResponse)myHttwebrequest.GetResponse();

			using (StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(), true))
			{
				string site = strm.ReadToEnd();
				int pos = 0;
				char c;
				while (true)
				{
					pos = site.IndexOf("href", pos + 1);
					if (pos <= 0)
						break;
					int s = site.IndexOf('\'', pos);
					int l = site.IndexOf('\"', pos);
					if (s != -1 && l != -1)
						c = s < l ? '\'' : '\"';
					else
						c = s > l ? '\'' : '\"';

					s = site.IndexOf(c, pos) + 1;
					l = site.IndexOf(c, s);
					string href = site[s..l];

					Uri temp = new Uri(href, UriKind.RelativeOrAbsolute);
					if (!temp.IsAbsoluteUri)
						temp = new Uri(url, temp);
					if ((temp.Scheme == Uri.UriSchemeHttps || temp.Scheme == Uri.UriSchemeHttp) && !crawlingUrls.Contains(temp))
						crawlingUrls.Add(temp);
				}
			}
		}

		/// <summary>
		/// Get all Url from sitemaps if in exist
		/// </summary>
		/// <param name="hostUrl"></param>
		public virtual void GetSitemaps(Uri hostUrl)
		{
			HttpWebRequest myHttwebrequest = (HttpWebRequest)HttpWebRequest.Create(hostUrl + "robots.txt");
			myHttwebrequest.Timeout = 10000;
			HttpWebResponse myHttpWebresponse = (HttpWebResponse)myHttwebrequest.GetResponse();
			using (StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(), true))
			{
				string site = strm.ReadToEnd();
				int s = 0;
				int l = 0;
				while (true)
				{
					s = site.IndexOf("Sitemap", l) + 9;
					if (s > 8)
					{
						l = site.IndexOf('\n', s);
						if (l > 0)
						{
							sitemaps.Add(new Uri(site[s..l]));
						}
						else
							break;
					}
					else
						break;
				}
			}

			foreach (var map in sitemaps)
			{
				myHttwebrequest = (HttpWebRequest)HttpWebRequest.Create(map);
				myHttpWebresponse = (HttpWebResponse)myHttwebrequest.GetResponse();
				using (StreamReader strm = new StreamReader(myHttpWebresponse.GetResponseStream(), true))
				{
					if (map.ToString().EndsWith(".gz"))
					{
						using GZipStream gZipStream = new GZipStream(strm.BaseStream, CompressionMode.Decompress);
						if (map.ToString().Contains(".xml"))
						{
							XmlDocument doc = new XmlDocument();
							doc.Load(gZipStream);
							XmlNodeList xmlSitemapList = doc.GetElementsByTagName("url");
							foreach (XmlNode node in xmlSitemapList)
								if (node["loc"] != null)
									sitemapUrls.Add(new Uri(node["loc"].InnerText));
						}
						else if (map.ToString().Contains(".txt"))
						{
							using (StreamReader unzip = new StreamReader(gZipStream))
								while (!unzip.EndOfStream)
								{
									if (Uri.TryCreate(unzip.ReadLine(), UriKind.RelativeOrAbsolute, out Uri url))
										sitemapUrls.Add(url);
								}
						}
					}
					else if (map.ToString().EndsWith(".xml"))
					{
						XmlDocument doc = new XmlDocument();
						doc.Load(strm);
						XmlNodeList xmlSitemapList = doc.GetElementsByTagName("url");
						foreach (XmlNode node in xmlSitemapList)
							if (node["loc"] != null)
								sitemapUrls.Add(new Uri(node["loc"].InnerText));
					}
					else if (map.ToString().EndsWith(".txt"))
					{
						while (!strm.EndOfStream)
							sitemapUrls.Add(new Uri(strm.ReadLine()));
					}
				}
			}
		}
	}
}
