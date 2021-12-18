using AppFramework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operationResult = new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
       
            var slug = command.Slug.Slugify();
            var prodeuctCategory = new ProductCategory(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Create(prodeuctCategory);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operationResult = new OperationResult();
            var productCategoryRepository = _productCategoryRepository.Get(command.Id);
            if (productCategoryRepository == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            var slug = command.Slug.Slugify();
            if (_productCategoryRepository.Exists(x=>x.Name == command.Name && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            productCategoryRepository.Edit(command.Name, command.Description,
                command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}