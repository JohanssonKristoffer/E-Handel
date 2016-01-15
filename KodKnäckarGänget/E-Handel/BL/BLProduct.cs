using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Handel.BL
{
    public class BLProduct
    {
        public BLProduct()
        {
            
        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageFilename { get; set; }
        public string ThumbnailFilename { get; set; }
        public double Price { get; set; }
        public int Popularity { get; set; }
        public int StockQuantity { get; set; }
        public double VAT { get; set; }
    }
}