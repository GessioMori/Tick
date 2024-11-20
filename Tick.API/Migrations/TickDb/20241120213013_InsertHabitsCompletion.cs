using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tick.API.Migrations.TickDb
{
    /// <inheritdoc />
    public partial class InsertHabitsCompletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Habits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HabitCompletions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "HabitCompletions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HabitCompletions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HabitCompletions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "HabitCompletions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HabitCompletions");
        }
    }
}
