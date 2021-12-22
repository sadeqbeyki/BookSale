namespace ShopManagement.Infrastructure.EFCore.Migrations.Operation
{
    [System.Diagnostics.DebuggerDisplay("ALTER TABLE {Products} RENAME COLUMN {IsInStock} TO {InStock}")]
    public class RenameColumnOperation : Microsoft.EntityFrameworkCore.Migrations.Operations.MigrationOperation, Microsoft.EntityFrameworkCore.Migrations.Operations.ITableMigrationOperation
    {
        public string? Schema => throw new NotImplementedException();

        public string Table => throw new NotImplementedException();


    }
}
