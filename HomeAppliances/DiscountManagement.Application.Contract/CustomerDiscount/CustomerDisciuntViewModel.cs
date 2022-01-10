﻿namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDisciuntViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public DateTime StartDateGr { get; set; }
        public DateTime EndDateGr { get; set; }
    }
}
