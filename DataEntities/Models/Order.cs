using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    [Table("Order")]
    public class Order : EntityBase
    {
        public int MemberId { get; set; }

        public Member Member { get; set; }

        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public string Adress { get; set; }

        public virtual List<OrderDetail> OrderDetail { get; set; }
    }
}
