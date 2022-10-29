﻿using AppFramework.Infrastructure;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.Inventory.Agg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration;

public class InventoryConfigureServices
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IInventoryApplication, InventoryApplication>();
        services.AddTransient<IInventoryRepository, InventoryRepository>();

        services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

        services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
    }
}