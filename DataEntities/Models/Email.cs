using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Models
{
    [Table("Email")]
    public class Email : EntityBase
    {
        [StringLength(50)]
        public string SmtpClient { get; set; }

        public bool EnableSsl { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string CCEmailOne { get; set; }

        [StringLength(50)]
        public string CCEmailTwo { get; set; }

        [StringLength(50)]
        public string CCEmailThree { get; set; }

        [StringLength(50)]
        public string Host { get; set; }

        public int Port { get; set; }

    }
}
