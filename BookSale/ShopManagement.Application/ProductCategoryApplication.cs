using AppFramework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operationResult = new OperationResult();
            if (_productCategoryRepository.Exists(x=>x.Name==command.Name))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
       
            var slug = command.Slug.Slugify();

            var picturePath = $"{command.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            var prodeuctCategory = new ProductCategory(command.Name, command.Description,
                pictureName, command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Create(prodeuctCategory);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operationResult = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_productCategoryRepository.Exists(x=>x.Name == command.Name && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var picturePath=$"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture,picturePath);
            productCategory.Edit(command.Name, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}