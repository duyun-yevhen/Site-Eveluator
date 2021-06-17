using System;

/// <summary>
/// helper class for storing url and response time
/// </summary> 
namespace WebCrawler
{
	public class UrlResponseTime : IComparable<UrlResponseTime>
	{
		public Uri url;
		public long responseTime;
		public int CompareTo(UrlResponseTime other) => this.responseTime.CompareTo(other.responseTime);
		public static implicit operator UrlResponseTime(Uri uri)
		{
			return new UrlResponseTime
			{
				responseTime = 0,
				url = uri
			};
		}
	}
}
