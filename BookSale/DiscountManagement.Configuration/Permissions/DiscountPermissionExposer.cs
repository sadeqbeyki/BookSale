using AppFramework.Infrastructure;

namespace DiscountManagement.Configuration.Permissions
{
    public class DiscountPermissionExposer: IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "CustomerDiscount", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.ListCustomerDiscounts,"ListCustomerDiscounts"),
                        new PermissionDto(DiscountPermissions.SearchCustomerDiscounts,"SearchCustomerDiscounts"),
                        new PermissionDto(DiscountPermissions.DefineCustomerDiscount,"DefineCustomerDiscount"),
                        new PermissionDto(DiscountPermissions.EditCustomerDiscount,"EditCustomerDiscount"),
                    }
                },
                {
                    "ColleagueDiscount", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.ListColleagueDiscounts,"ListColleagueDiscounts"),
                        new PermissionDto(DiscountPermissions.SearchColleagueDiscounts,"SearchColleagueDiscounts"),
                        new PermissionDto(DiscountPermissions.DefineColleagueDiscount,"DefineColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.EditColleagueDiscount,"EditColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.RemoveColleagueDiscount,"RemoveColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.RestoreColleagueDiscount,"RestoreColleagueDiscount"),
                    }
                }
            };
        }
    }
}
