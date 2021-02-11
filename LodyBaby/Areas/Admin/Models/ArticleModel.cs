using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Package_Ecommerce.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PropertyProduct { get; set; }

        public int Minvalue { get; set; }

        public string Summary { get; set; }

        public DateTime? Date { get; set; }

        public int ReadingCount { get; set; }
        
        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }

        public string ImageOne { get; set; }

        public string ImageTwo { get; set; }

        public string CreatedBy { get; set; }

        public string Guid { get; set; }

        public string[] Tags { get; set; }

        public string AllTags { get; set; }


        public int ProductCode { get; set; }

    }
}