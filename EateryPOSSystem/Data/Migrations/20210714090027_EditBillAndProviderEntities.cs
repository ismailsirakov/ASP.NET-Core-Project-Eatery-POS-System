namespace EateryPOSSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EditBillAndProviderEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderNumber",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "IsClosed",
                table: "Bills",
                newName: "Closed");

            migrationBuilder.RenameColumn(
                name: "BillOpenDateTime",
                table: "Bills",
                newName: "OpenDateTime");

            migrationBuilder.RenameColumn(
                name: "BillCloseDateTime",
                table: "Bills",
                newName: "CloseDateTime");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Products",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "POSSystemUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Providers");

            migrationBuilder.RenameColumn(
                name: "OpenDateTime",
                table: "Bills",
                newName: "BillOpenDateTime");

            migrationBuilder.RenameColumn(
                name: "Closed",
                table: "Bills",
                newName: "IsClosed");

            migrationBuilder.RenameColumn(
                name: "CloseDateTime",
                table: "Bills",
                newName: "BillCloseDateTime");

            migrationBuilder.AddColumn<string>(
                name: "ProviderNumber",
                table: "Providers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "POSSystemUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
