using System;
using System.Collections.Generic;
using WebCrawler.Logic;
using Xunit;

namespace WebCrawler.Tests
{
	public class SitePageParserTests
	{
		[Fact]
		public void SiteParser_ParseAllHtmlDocumentsFromSite()
		{
			// arrange
			var parser = new SitePageParser();
			string site = "<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.01//EN\" \"http://test.com/TR/html4/strict.dtd\">\n" +
							"<html>\n" +
							"<link href=\" / manifest ? pwa = webhp\" crossorigin=\"use - credentials\" rel=\"manifest\">" +
							"<a http-equiv=\"content - type\" content=\"text / html; charset = utf - 8\">\n" +
							"<title>Ссылки на странице</title>\n" +
							"<a href =\"http://test.com/\"><a class=\"EzVRq\" href=\"1.html\">" +
							"</li><li role=\"none\"><a class=\"EzVRq\" href= \" http://test.com/2.html\" role =\"menuitem\" tabindex=\"-1\">Поиск</a></li>\n" +
							"<p><a href=\"http://test.com/3.html\">Собаки</a></p>\n" +
							"<p><a href=\'4\'>Кошки</a></p>\n" +
							"<p><link href=\"cat.html\">Кошки</a></p>\n" +
							"<p><link href=\"http://test.com/dog.html\"/a></p>\n" +
							"<p><link href=\"/\"/a></p>\n" +
							"</html>";
			List<Uri> expected = new List<Uri>
			{
				new Uri("http://test.com/1.html"),
				new Uri("http://test.com/2.html"),
				new Uri("http://test.com/3.html"),
				new Uri("http://test.com/4"),
			};

			// act
			IEnumerable<Uri> actual = parser.ParseAllChildrenLinks(site, new Uri("http://test.com/"));
			// assert
			Assert.Equal(expected, actual);
		}
	}
}
