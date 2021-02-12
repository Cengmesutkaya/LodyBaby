using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    [Table("Offer")]
    public class Offer:EntityBase
    {

        [StringLength(150)]
        public string PersonName { get; set; }

        [StringLength(150)]
        public string CompanyName { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Gsm { get; set; }

        [StringLength(150)]
        public string Height { get; set; }

        [StringLength(150)]
        public string Property { get; set; }

        [StringLength(150)]
        public string Number { get; set; }

        [StringLength(150)]
        public string Price { get; set; }


    }
}
