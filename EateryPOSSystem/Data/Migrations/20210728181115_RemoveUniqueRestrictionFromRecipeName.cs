using Microsoft.EntityFrameworkCore.Migrations;

namespace EateryPOSSystem.Data.Migrations
{
    public partial class RemoveUniqueRestrictionFromRecipeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_Name",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Name",
                table: "Recipes",
                column: "Name",
                unique: true);
        }
    }
}
