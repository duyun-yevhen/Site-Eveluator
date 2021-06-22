using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace WebCrawler.Logic
{
	public class SiteRequest
	{
		public virtual int GetUrlResponseTime(Uri url, int timeout = 10000)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			
			GetPageResponse(url, timeout);
			stopwatch.Stop();

			return (int)stopwatch.ElapsedMilliseconds;
		}

		public virtual HttpWebResponse GetPageResponse(Uri url, int timeout = 10000)
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
			HttpWebResponse response = GetPageResponse(url, timeout);
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
