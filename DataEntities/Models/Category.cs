using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    [Table("Category")]
    public class Category:EntityBase
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(700)]
        public string Tags { get; set; }

        public bool IsVitrin { get; set; }
    }
}
