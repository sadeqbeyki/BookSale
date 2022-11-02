using AppFramework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg;

public interface IOrderRepository : IBaseRepository<long, Order>
{
    double GetAmountBy(long id);
    List<OrderItemViewModel> GetItems(long orderId);
    List<OrderViewModel> Search(OrderSearchModel searchModel);
}