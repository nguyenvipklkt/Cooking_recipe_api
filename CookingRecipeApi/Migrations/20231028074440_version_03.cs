using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingRecipeApi.Migrations
{
    public partial class version_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodTypeId",
                table: "foods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 28, 7, 44, 39, 943, DateTimeKind.Utc).AddTicks(4123));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodTypeId",
                table: "foods");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 27, 13, 27, 22, 823, DateTimeKind.Utc).AddTicks(5048));
        }
    }
}
