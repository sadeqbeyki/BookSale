﻿using AppFramework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.Inventory.Agg;
using ShopManagement.Domain.Services;

namespace InventoryManagement.Application;

public class InventoryApplication : IInventoryApplication
{
    private readonly IShopAccountAcl _shopAccountAcl;
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryApplication(IInventoryRepository inventoryRepository, IShopAccountAcl shopAccountAcl)
    {
        _inventoryRepository = inventoryRepository;
        _shopAccountAcl = shopAccountAcl;
    }

    public OperationResult Create(CreateInventory command)
    {
        var operation = new OperationResult();
        if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var inventory = new Inventory(command.ProductId, command.UnitPrice);
        _inventoryRepository.Create(inventory);
        _inventoryRepository.SaveChanges();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.Id);
        if (inventory == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        inventory.Edit(command.ProductId, command.UnitPrice);
        _inventoryRepository.SaveChanges();
        return operation.Succeeded();
    }

    public EditInventory GetDetails(long id)
    {
        return _inventoryRepository.GetDetails(id);
    }

    public List<InventoryOperationViewModel> GetOperationLog(long operationId)
    {
        return _inventoryRepository.GetOperationLog(operationId);
    }

    public OperationResult Increase(IncreaseInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        var operatorId = _shopAccountAcl.CurrentAccountId();
        inventory.Increase(command.Count, operatorId, command.Description);
        _inventoryRepository.SaveChanges();
        return operation.Succeeded();
    }

    public OperationResult Reduce(ReduceInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.Get(command.InventoryId);
        if (inventory == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        var operatorId = _shopAccountAcl.CurrentAccountId();
        inventory.Reduce(command.Count, operatorId, command.Description, 0);
        _inventoryRepository.SaveChanges();
        return operation.Succeeded();
    }

    public OperationResult Reduce(List<ReduceInventory> command)
    {
        var operation = new OperationResult();
        var operatorId = _shopAccountAcl.CurrentAccountId();
        foreach (var item in command)
        {
            var inventory = _inventoryRepository.GetBy(item.ProductId);
            inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
        }
        _inventoryRepository.SaveChanges();
        return operation.Succeeded();
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        return _inventoryRepository.Search(searchModel);
    }
}
