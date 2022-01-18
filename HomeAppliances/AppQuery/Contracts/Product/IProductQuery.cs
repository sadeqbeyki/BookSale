﻿namespace AppQuery.Contracts.Product
{
    public interface IProductQuery
    {
        //ProductQueryModel GetProductDetails(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
