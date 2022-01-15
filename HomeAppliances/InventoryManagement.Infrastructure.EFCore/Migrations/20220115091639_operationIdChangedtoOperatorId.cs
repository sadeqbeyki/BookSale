using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Infrastructure.EFCore.Migrations
{
    public partial class operationIdChangedtoOperatorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperationId",
                table: "InventoryOperations",
                newName: "OperatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperatorId",
                table: "InventoryOperations",
                newName: "OperationId");
        }
    }
}
