using System;
using System.Collections.Generic;

#nullable disable

namespace NorthwAPI2021syksy.Models
{
    public partial class SummaryOfSalesByQuarter
    {
        public DateTime? ShippedDate { get; set; }
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
