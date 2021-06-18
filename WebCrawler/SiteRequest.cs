using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace WebCrawler
{
	public static class SiteRequest
	{
		public static List<UrlResponseTime> GetResponseTimes(List<Uri> urls, int querydDelay, int timeout = 10000)
		{
			List<UrlResponseTime> urlResponseTimes = new List<UrlResponseTime>();
			Stopwatch stopwatch;
			int i = 0;
			foreach (var site in urls)
			{
				stopwatch = Stopwatch.StartNew();
				var response = GetResponse(site, timeout);
				stopwatch.Stop();
#if DEBUG
				Console.WriteLine($"{i}/{urls.Count} {response.StatusCode}\n{site}");
#endif
				System.Threading.Thread.Sleep(querydDelay);
				urlResponseTimes.Add(new UrlResponseTime() { ResponseTime = stopwatch.ElapsedMilliseconds, Url = site });
			}
			return urlResponseTimes;
		}

		public static HttpWebResponse GetResponse(Uri url, int timeout = 10000)
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
	}
}
