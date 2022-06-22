using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherWebSolution.DAL.SqlServer.Migrations
{
    public partial class DataTimeIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Values_Time",
                table: "Values",
                column: "Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Values_Time",
                table: "Values");
        }
    }
}
