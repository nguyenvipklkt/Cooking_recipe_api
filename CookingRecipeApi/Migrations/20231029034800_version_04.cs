using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingRecipeApi.Migrations
{
    public partial class version_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "follows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 29, 3, 47, 59, 917, DateTimeKind.Utc).AddTicks(2341));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "follows");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 28, 7, 44, 39, 943, DateTimeKind.Utc).AddTicks(4123));
        }
    }
}
