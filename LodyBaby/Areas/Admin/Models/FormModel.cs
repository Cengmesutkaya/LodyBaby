using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Package_Ecommerce.Areas.Admin.Models
{
    public class FormModel
    {

        public int Id { get; set; }

        public string personName { get; set; }

        public string personEmail { get; set; }

        public string gsm { get; set; }

        public string companyName { get; set; }

        public string addedBy { get; set; }

        public string addedDate { get; set; }

        public string[] boy { get; set; }

        public string[] ozellik { get; set; }

        public string[] adet { get; set; }

        public string[] fiyat { get; set; }


    }
}