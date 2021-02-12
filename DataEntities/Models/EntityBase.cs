using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    public class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string Createby { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(50)]
        public string Updateby { get; set; }

        [StringLength(50)]
        public string Guid { get; set; }
    }
}
