﻿using AppFramework.Infrastructure;
using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.Configuration.Permissions;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastructure.Configuration;

public class CommentConfigureServices
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<ICommentApplication, CommentApplication>();

        services.AddScoped<IPermissionExposer, CommentPermissionExposer>();

        services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
    }
}
