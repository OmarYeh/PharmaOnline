using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Project.Data.Migrations
{
    public partial class intial_omar3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MadeInCountry",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MadeInCountry",
                table: "Items");
        }
    }
}
