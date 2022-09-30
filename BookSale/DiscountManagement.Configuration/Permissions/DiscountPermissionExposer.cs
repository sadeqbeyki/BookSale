using AppFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Configuration.Permissions
{
    public class DiscountPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Discount", new List<PermissionDto>
                    {
                    new PermissionDto(DiscountPermissions.ListDiscounts,"ListDiscounts"),
                    new PermissionDto(DiscountPermissions.SearchDiscount,"SearchDiscount"),
                    new PermissionDto(DiscountPermissions.CreateDiscount,"CreateDiscount"),
                    new PermissionDto(DiscountPermissions.EditDiscount,"EditDiscount"),

                    }
                }
            };
        }
    }
}
