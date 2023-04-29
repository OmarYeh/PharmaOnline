using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Project.Data.Migrations
{
    public partial class Fixingrelationsbetweentables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Items_ItemId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_ItemId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Baskets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Baskets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_ItemId",
                table: "Baskets",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Items_ItemId",
                table: "Baskets",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
