using AppFramework.Application;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit (EditCustomerDiscount command);
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDisciuntViewModel> Search(CustomerDiscountSearchModel searchModel);

    }
}
