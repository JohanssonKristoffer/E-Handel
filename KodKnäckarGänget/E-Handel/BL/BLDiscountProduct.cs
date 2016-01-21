using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Handel.BL
{
    public class BLDiscountProduct
    {
        public int Id { get; set; }
        public double Discount { get; set; }

        public BLDiscountProduct(int id, double discount)
        {
            Id = id;
            Discount = discount;
        }

        public override string ToString() => $"Id = {Id}, Discount = {Discount}";

    }
}