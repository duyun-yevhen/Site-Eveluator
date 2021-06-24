using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using WebCrawler.ConsoleApp;
using Xunit;
using System.Data;
using WebCrawler.Model;
using System.Collections;

namespace WebCrawler.Tests
{
	public class DBWorkerTests
	{
		[Fact]
		public void DBWorker_SaveresultTest()
		{
			// arrange
			var testsRepositoryMock = new Mock<IRepository<PerformanceTest>>();
			var urlResponseTimeRepositoryMock = new Mock<IRepository<UrlPerformanseTestResult>>();
			var dBWorker = new DbWorker(testsRepositoryMock.Object, urlResponseTimeRepositoryMock.Object);

			// act
			dBWorker.SaveResult(new Uri("http://test.com"), new List<UrlPerformanseTestResult>() { new UrlPerformanseTestResult(), new UrlPerformanseTestResult() });

			// assert
			testsRepositoryMock.Verify(a => a.Add(It.IsAny<PerformanceTest>()), Times.Once());
			testsRepositoryMock.Verify(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
			
			urlResponseTimeRepositoryMock.Verify(a => a.AddRange(It.IsAny<IEnumerable<UrlPerformanseTestResult>>()), Times.Once());
			urlResponseTimeRepositoryMock.Verify(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
		}
	}
}
