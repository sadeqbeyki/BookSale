using AppFramework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.Inventory.Agg
{
    public interface IInventoryRepository:IRepository<long, Inventory>
    {
        EditInventory GetDetails(long id);
        Inventory GetBy(long productId);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
