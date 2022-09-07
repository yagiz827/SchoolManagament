using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class teachpass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teachers");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Teachers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Teachers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "Password",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
