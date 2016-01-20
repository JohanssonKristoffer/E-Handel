using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Handel.BL
{
    public class BLCartProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public BLCartProduct(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public override string ToString() => $"Id = {Id}, Quantity = {Quantity}";
    }
}