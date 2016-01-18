using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Handel.BL
{
    public class BLProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Popularity { get; set; }
        public int StockQuantity { get; set; }
        public double VAT { get; set; }

        public BLProduct(int id, int categoryId, string name, double price, int popularity, int stockQuantity, double VAT)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Price = price;
            Popularity = popularity;
            StockQuantity = stockQuantity;
            this.VAT = VAT;
        }

        public override string ToString() => $"Id = {Id}, CategoryId = {CategoryId}, Name = {Name}, Price = {Price}, Popularity = {Popularity}, StockQuantity = {StockQuantity}, VAT = {VAT}";
    }
}