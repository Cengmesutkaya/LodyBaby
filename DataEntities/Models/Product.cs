using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    
    [Table("Product")]
    public class Product : EntityBase
    {
        [StringLength(100)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public string PropertyProduct { get; set; }

        [StringLength(150)]
        public string Summary { get; set; }

        [StringLength(150)]
        public string ImageOne { get; set; }

        [StringLength(150)]
        public string ImageTwo { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public int Stock { get; set; }

        public int MinValue { get; set; }

        public bool IsActive { get; set; }
        
        [StringLength(250)]
        public string Tags { get; set; }

        public int ReadingCount { get; set; }

        public bool Vitrin { get; set; }

        public int ProductCode { get; set; }

    }
}
