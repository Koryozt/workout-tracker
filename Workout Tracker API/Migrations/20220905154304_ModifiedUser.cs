using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workout_Tracker_API.Migrations
{
    public partial class ModifiedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Users_UserID",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Routines_UserID",
                table: "Routines");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Routines");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SavedRoutines",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedRoutines",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Routines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routines_UserID",
                table: "Routines",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Users_UserID",
                table: "Routines",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }
    }
}
