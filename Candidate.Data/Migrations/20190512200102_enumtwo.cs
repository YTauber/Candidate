using Microsoft.EntityFrameworkCore.Migrations;

namespace Candidate.Data.Migrations
{
    public partial class enumtwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Candidates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Candidates");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Candidates",
                nullable: true);
        }
    }
}
