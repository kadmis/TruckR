using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Migrations
{
    public partial class Users_AddedActivation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActivationId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");
        }
    }
}
