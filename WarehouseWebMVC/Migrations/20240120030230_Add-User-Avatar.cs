using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "Avatar",
                value: "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky5.jpg?alt=media&token=89ff6391-2c89-4e62-a40e-1f96c5414071");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L,
                column: "Avatar",
                value: "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky1.jpg?alt=media&token=20f7f936-db7d-4498-9245-50875cc9f546");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3L,
                column: "Avatar",
                value: "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky2.jpg?alt=media&token=67c90174-f0e6-4251-acdb-e17d9d88e8ec");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4L,
                column: "Avatar",
                value: "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky3.jpg?alt=media&token=2b2622f9-9b99-4ab4-bbc9-aa3c66dd7b24");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5L,
                column: "Avatar",
                value: "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky4.jpg?alt=media&token=cbd4f161-7102-4ce1-b9f2-1ccf0c9edf57");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6L,
                column: "Avatar",
                value: "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fhusky6.webp?alt=media&token=85db917a-6c50-4860-948d-266409314974");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");
        }
    }
}
