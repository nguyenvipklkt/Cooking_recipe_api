using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingRecipeApi.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientList",
                table: "foods");

            migrationBuilder.DropColumn(
                name: "FollowerId",
                table: "follows");

            migrationBuilder.RenameColumn(
                name: "StepList",
                table: "food_steps",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "FosllowingUserIde",
                table: "follows",
                newName: "FollowingUserId");

            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NoStep",
                table: "food_steps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cover", "CreatedDate" },
                values: new object[] { "", new DateTime(2023, 10, 27, 13, 27, 22, 823, DateTimeKind.Utc).AddTicks(5048) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NoStep",
                table: "food_steps");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "food_steps",
                newName: "StepList");

            migrationBuilder.RenameColumn(
                name: "FollowingUserId",
                table: "follows",
                newName: "FosllowingUserIde");

            migrationBuilder.AddColumn<string>(
                name: "IngredientList",
                table: "foods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowerId",
                table: "follows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 10, 26, 7, 13, 12, 699, DateTimeKind.Utc).AddTicks(5422));
        }
    }
}
