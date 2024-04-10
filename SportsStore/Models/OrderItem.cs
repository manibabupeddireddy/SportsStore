using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}