using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    [Table("Team")]
    public class Team : EntityBase
    {
        [StringLength(50)]
        public string MemberType { get; set; }

        [StringLength(50)]
        public string NameSurname { get; set; }

        [StringLength(50)]
        public string Job { get; set; }

        [StringLength(250)]
        public string About { get; set; }

        [StringLength(150)]
        public string Image { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
