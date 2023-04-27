using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations
{
    /// <inheritdoc />
    public partial class Addingsuperadminchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03bf295d-7983-4702-aa8d-6aa18cbc8f27",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da6638d6-7e2b-4f9d-85a6-17c856655a6c", "SUPERADMIN@BLOGGIE.COM", "SUPERADMIN@BLOGGIE.COM", "AQAAAAIAAYagAAAAELL+C7JFNmY6WzHQYEp55cadENlq54tJl6siS6NNHYg4EsTgKlttwcmBxa/kGJjbCg==", "886b2e57-4392-463b-9907-716de49aa437" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03bf295d-7983-4702-aa8d-6aa18cbc8f27",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9bfdc80-95da-4979-afd1-4d806259e633", null, null, "AQAAAAIAAYagAAAAEIm2IdXBTVwdu8kuFduGdXiWICTstQZnUxGa3cI6lEzBk8qldKy3J+2PYfo6OKd7gQ==", "3f503435-1f16-4bda-a08c-617b4eb89144" });
        }
    }
}
