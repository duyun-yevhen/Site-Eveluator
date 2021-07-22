using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using WebCrawler.Model;
using Xunit;

namespace WebCrawler.Tests
{
	public class DBWorkerTests
	{
		[Fact]
		public void DBWorker_SaveresultTest()
		{
			// arrange
			var testsRepositoryMock = new Mock<IRepository<PerformanceTest>>();
			var urlResponseTimeRepositoryMock = new Mock<IRepository<PerformanceResult>>();
			var dBWorker = new DbWorker(testsRepositoryMock.Object, urlResponseTimeRepositoryMock.Object);

			// act
			dBWorker.SaveResultAsync(new Uri("http://test.com"), new List<PerformanceResult>() { new PerformanceResult(), new PerformanceResult() }).Wait();

			// assert
			testsRepositoryMock.Verify(a => a.Add(It.IsAny<PerformanceTest>()), Times.Once());
			testsRepositoryMock.Verify(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

			urlResponseTimeRepositoryMock.Verify(a => a.AddRange(It.IsAny<IEnumerable<PerformanceResult>>()), Times.Once());
			urlResponseTimeRepositoryMock.Verify(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
		}
	}
}
