﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevopsDemoOrdersApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public int ProductId { get; set; }
        public double OrderAmount { get; set; }
    }
}
