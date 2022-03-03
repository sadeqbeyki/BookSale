﻿using AppFramework.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        EditArticle GetDetails(long id);
        Article GetArticleWithCategory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);

    }
}