using AppFramework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class CreateInventory
    {
        [Range(1,100000,ErrorMessage =ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
