using ShopManagement.Application.Contracts.Order;

namespace AppQuery.Contracts
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}