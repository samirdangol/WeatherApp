using Microsoft.EntityFrameworkCore.Migrations;

namespace Weather.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZipCodeWeather",
                columns: table => new
                {
                    ZipCode = table.Column<string>(nullable: false),
                    Temparature = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodeWeather", x => x.ZipCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZipCodeWeather");
        }
    }
}
