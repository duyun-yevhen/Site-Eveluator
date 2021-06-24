using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawler.Model.Migrations
{
    public partial class namingfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "PerformanceTests",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PerformanceTests",
                newName: "TestId");
        }
    }
}
