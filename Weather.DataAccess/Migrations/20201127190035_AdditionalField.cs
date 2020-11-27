using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Weather.DataAccess.Migrations
{
    public partial class AdditionalField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temparature",
                table: "ZipCodeWeather");

            migrationBuilder.AddColumn<DateTime>(
                name: "AsOfDate",
                table: "ZipCodeWeather",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ZipCodeWeather",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cloud",
                table: "ZipCodeWeather",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ZipCodeWeather",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ZipCodeWeather",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ZipCodeWeather",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Pressure",
                table: "ZipCodeWeather",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temp",
                table: "ZipCodeWeather",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TempMax",
                table: "ZipCodeWeather",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TempMin",
                table: "ZipCodeWeather",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "WeatherDesc",
                table: "ZipCodeWeather",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wind",
                table: "ZipCodeWeather",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsOfDate",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "Cloud",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "Temp",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "TempMax",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "TempMin",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "WeatherDesc",
                table: "ZipCodeWeather");

            migrationBuilder.DropColumn(
                name: "Wind",
                table: "ZipCodeWeather");

            migrationBuilder.AddColumn<int>(
                name: "Temparature",
                table: "ZipCodeWeather",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
