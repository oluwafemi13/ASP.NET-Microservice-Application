﻿namespace Discount.grpc.Entities
{
    public class Coupon
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
