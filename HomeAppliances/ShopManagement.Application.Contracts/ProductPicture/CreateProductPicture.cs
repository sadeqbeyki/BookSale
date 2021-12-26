using AppFramework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1,100000,ErrorMessage=ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Required(ErrorMessage =ValidationMessage.IsRequired)]
        public string Picture { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
