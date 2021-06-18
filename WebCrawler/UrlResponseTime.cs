using System;

/// <summary>
/// helper class for storing url and response time
/// </summary> 
namespace WebCrawler
{
	public class UrlResponseTime : IComparable<UrlResponseTime>
	{
		public Uri Url { get; set; }
		public long ResponseTime { get; set; } = -1;

		public int CompareTo(UrlResponseTime other) => this.ResponseTime.CompareTo(other.ResponseTime);
		
		public static implicit operator UrlResponseTime(Uri uri)
		{
			return new UrlResponseTime
			{
				Url = uri
			};
		}
	}
}
