using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    [Table("Member")]

    public class Member : EntityBase
    {
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Password { get; set; }      

        public bool IsActive { get; set; }

        public string Adress { get; set; }

        [StringLength(50)]
        public string MemberType { get; set; }


    }
}
