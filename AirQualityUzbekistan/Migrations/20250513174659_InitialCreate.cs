using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirQualityUzbekistan.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirQualityRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AQIUS = table.Column<int>(type: "INTEGER", nullable: false),
                    MainPollutantUS = table.Column<string>(type: "TEXT", nullable: false),
                    AQICN = table.Column<int>(type: "INTEGER", nullable: false),
                    MainPollutantCN = table.Column<string>(type: "TEXT", nullable: false),
                    Humidity = table.Column<int>(type: "INTEGER", nullable: false),
                    Pressure = table.Column<int>(type: "INTEGER", nullable: false),
                    Temperature = table.Column<int>(type: "INTEGER", nullable: false),
                    WindDirection = table.Column<int>(type: "INTEGER", nullable: false),
                    WindSpeed = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirQualityRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LocationStateId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationCities_LocationStates_LocationStateId",
                        column: x => x.LocationStateId,
                        principalTable: "LocationStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationCities_LocationStateId",
                table: "LocationCities",
                column: "LocationStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirQualityRecords");

            migrationBuilder.DropTable(
                name: "LocationCities");

            migrationBuilder.DropTable(
                name: "LocationStates");
        }
    }
}
