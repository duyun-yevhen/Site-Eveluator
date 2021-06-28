using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebCrawler.Model.Migrations
{
	public partial class datetime_autogeneration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTime>(
				name: "Date",
				table: "PerformanceTests",
				type: "datetime2",
				nullable: false,
				defaultValueSql: "GETDATE()",
				oldClrType: typeof(DateTime),
				oldType: "datetime2");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTime>(
				name: "Date",
				table: "PerformanceTests",
				type: "datetime2",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldType: "datetime2",
				oldDefaultValueSql: "GETDATE()");
		}
	}
}
