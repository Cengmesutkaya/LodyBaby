using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    [Table("OrderDetail")]

    public class OrderDetail:EntityBase
    {
        public int OrderId { get; set; }

        public int ProductCode { get; set; }

        public string ProductName { get; set; }

        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }



    }
}
