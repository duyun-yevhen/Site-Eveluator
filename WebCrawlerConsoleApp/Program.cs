using System;
using System.Collections.Generic;

namespace WebCrawler.Programm
{
	class Program
	{
		static void Main(string[] args)
		{
			var Crawler = new SiteCrawlerConsoleAppUI();
			Crawler.DoWork();
		}

	}
}
