﻿using AppFramework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contract.ColleagueDiscount;

public class DefineColleagueDiscount
{
    [Range(1,100000,ErrorMessage=ValidationMessage.IsRequired)]
    public long ProductId { get; set; }
    [Range(1, 99, ErrorMessage = ValidationMessage.IsRequired)]

    public int DiscountRate { get; set; }
    public List<ProductViewModel>  Products { get; set; }
}


