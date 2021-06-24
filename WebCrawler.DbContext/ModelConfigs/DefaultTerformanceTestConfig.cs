using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebCrawler.Model.ModelConfigs
{
	class DefaultTerformanceTestConfig : IEntityTypeConfiguration<PerformanceTest>
	{
		public void Configure(EntityTypeBuilder<PerformanceTest> builder)
		{
			builder.Property(u => u.Date)
					.HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
