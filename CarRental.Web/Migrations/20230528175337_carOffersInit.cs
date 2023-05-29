using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Web.Migrations
{
    /// <inheritdoc />
    public partial class carOffersInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CarOfferId",
                table: "CarOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriversLicenseNumber",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CarOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumOfDrivers",
                table: "CarOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "CarOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CarOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "CarOrders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CarOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CarOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "CarOfferId",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "DriversLicenseNumber",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "NumOfDrivers",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "State",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CarOrders");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CarOrders");
        }
    }
}
