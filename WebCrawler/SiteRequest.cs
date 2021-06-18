using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace WebCrawler
{
	public class SiteRequest
	{
		public virtual List<UrlResponseTime> GetResponseTimes(List<UrlResponseTime> urls, int querydDelay, int timeout = 10000)
		{
			Stopwatch stopwatch;
			int i = 0;
			foreach (var link in urls)
			{
				stopwatch = Stopwatch.StartNew();
				var response = GetResponse(link.Url, timeout);
				stopwatch.Stop();
				Console.WriteLine($"{i++}/{urls.Count} {response.StatusCode}\n{link.Url}");
				System.Threading.Thread.Sleep(querydDelay);
				link.ResponseTime = (int)stopwatch.ElapsedMilliseconds;
			}
			return urls;
		}

		public virtual HttpWebResponse GetResponse(Uri url, int timeout = 10000)
		{
			try
			{
				HttpWebRequest myHttwebrequest = (HttpWebRequest)WebRequest.Create(url);
				myHttwebrequest.Timeout = timeout;
				return (HttpWebResponse)myHttwebrequest.GetResponse();
			}
			catch (WebException e)
			{
				return (HttpWebResponse)e.Response;
			}
		}

		public virtual string DownloadSite(Uri url, int timeout = 10000)
		{
			HttpWebResponse response = GetResponse(url, timeout);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				return null;
			}

			using StreamReader strm = new StreamReader(response.GetResponseStream(), true);
			if (url.ToString().EndsWith(".gz")) 
			{
				using GZipStream gZipStream = new GZipStream(strm.BaseStream, CompressionMode.Decompress);
				using StreamReader siteStream = new StreamReader(gZipStream);
				return siteStream.ReadToEnd();
			}

			return strm.ReadToEnd();
		}
	}
}
