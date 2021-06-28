using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebCrawler.Model.Migrations
{
	public partial class init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "PerformanceTests",
				columns: table => new
				{
					TestId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					SiteUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
					Date = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PerformanceTests", x => x.TestId);
				});

			migrationBuilder.CreateTable(
				name: "UrlResponseTimes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
					InSitemap = table.Column<bool>(type: "bit", nullable: false),
					InSitePage = table.Column<bool>(type: "bit", nullable: false),
					ResponseTime = table.Column<int>(type: "int", nullable: false),
					TestId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UrlResponseTimes", x => x.Id);
					table.ForeignKey(
						name: "FK_UrlResponseTimes_PerformanceTests_TestId",
						column: x => x.TestId,
						principalTable: "PerformanceTests",
						principalColumn: "TestId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_UrlResponseTimes_TestId",
				table: "UrlResponseTimes",
				column: "TestId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "UrlResponseTimes");

			migrationBuilder.DropTable(
				name: "PerformanceTests");
		}
	}
}
