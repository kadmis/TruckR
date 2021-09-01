using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Transport.Infrastructure.Migrations
{
    public partial class Assignment_AddedFailedState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FailedOn",
                table: "Assignments",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedOn",
                table: "Assignments");
        }
    }
}
