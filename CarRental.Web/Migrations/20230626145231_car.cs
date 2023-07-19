using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Web.Migrations
{
    /// <inheritdoc />
    public partial class car : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlHandle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Horsepower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfProduction = table.Column<int>(type: "int", nullable: false),
                    EngineDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriveDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GearboxDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarDeliverylocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarReturnLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumOfDrivers = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriversLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarTags_CarOffers_CarOfferId",
                        column: x => x.CarOfferId,
                        principalTable: "CarOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageUrls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageUrls_CarOffers_CarOfferId",
                        column: x => x.CarOfferId,
                        principalTable: "CarOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarrifs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OneNormalDayPrice = table.Column<double>(type: "float", nullable: false),
                    OneWeekendDayPrice = table.Column<double>(type: "float", nullable: false),
                    FullWeekendPrice = table.Column<double>(type: "float", nullable: false),
                    OneWeekPrice = table.Column<double>(type: "float", nullable: false),
                    OneMonthPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarrifs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarrifs_CarOffers_CarOfferId",
                        column: x => x.CarOfferId,
                        principalTable: "CarOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarTags_CarOfferId",
                table: "CarTags",
                column: "CarOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_CarOfferId",
                table: "ImageUrls",
                column: "CarOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarrifs_CarOfferId",
                table: "Tarrifs",
                column: "CarOfferId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarOrders");

            migrationBuilder.DropTable(
                name: "CarTags");

            migrationBuilder.DropTable(
                name: "ImageUrls");

            migrationBuilder.DropTable(
                name: "Tarrifs");

            migrationBuilder.DropTable(
                name: "CarOffers");
        }
    }
}
