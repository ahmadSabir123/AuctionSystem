using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionSystem.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsAddIn_productTbL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NewOwnerId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SoldPrice",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_NewOwnerId",
                table: "Products",
                column: "NewOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AbpUsers_NewOwnerId",
                table: "Products",
                column: "NewOwnerId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AbpUsers_NewOwnerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_NewOwnerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NewOwnerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SoldPrice",
                table: "Products");
        }
    }
}
