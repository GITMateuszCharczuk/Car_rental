using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations
{
    /// <inheritdoc />
    public partial class superadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03bf295d-7983-4702-aa8d-6aa18cbc8f27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67441128-611e-47fb-aaf9-29b7362a4517", "AQAAAAIAAYagAAAAEMzvQOtxM5mLY7TzZAo/vQ3vJWKWigQkVYfOTeVJ+2eFPZwBzflvmUIss1PCPDbFUw==", "73f801d6-4bf2-474e-a133-c2ed580f88f7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03bf295d-7983-4702-aa8d-6aa18cbc8f27",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da6638d6-7e2b-4f9d-85a6-17c856655a6c", "AQAAAAIAAYagAAAAELL+C7JFNmY6WzHQYEp55cadENlq54tJl6siS6NNHYg4EsTgKlttwcmBxa/kGJjbCg==", "886b2e57-4392-463b-9907-716de49aa437" });
        }
    }
}
