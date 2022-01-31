using AppQuery.Contracts.ArticleCategory;
using AppQuery.Contracts.ProductCategory;

namespace AppQuery
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
