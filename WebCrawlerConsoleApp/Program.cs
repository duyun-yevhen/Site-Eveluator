﻿using System;
using System.Collections.Generic;

namespace WebCrawler.ConsoleApp
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
