using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawler.Model.Migrations
{
    public partial class id_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlResponseTimes_PerformanceTests_TestID",
                table: "UrlResponseTimes");

            migrationBuilder.RenameColumn(
                name: "TestID",
                table: "UrlResponseTimes",
                newName: "TestId");

            migrationBuilder.RenameIndex(
                name: "IX_UrlResponseTimes_TestID",
                table: "UrlResponseTimes",
                newName: "IX_UrlResponseTimes_TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlResponseTimes_PerformanceTests_TestId",
                table: "UrlResponseTimes",
                column: "TestId",
                principalTable: "PerformanceTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlResponseTimes_PerformanceTests_TestId",
                table: "UrlResponseTimes");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "UrlResponseTimes",
                newName: "TestID");

            migrationBuilder.RenameIndex(
                name: "IX_UrlResponseTimes_TestId",
                table: "UrlResponseTimes",
                newName: "IX_UrlResponseTimes_TestID");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlResponseTimes_PerformanceTests_TestID",
                table: "UrlResponseTimes",
                column: "TestID",
                principalTable: "PerformanceTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
