using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Project.Data.Migrations
{
    public partial class intial_omar2_new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Items");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Items",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
