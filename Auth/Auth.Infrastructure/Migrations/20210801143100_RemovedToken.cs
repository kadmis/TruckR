﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Infrastructure.Migrations
{
    public partial class RemovedToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "UserAuthentications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidUntil",
                table: "UserAuthentications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidUntil",
                table: "UserAuthentications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "UserAuthentications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
