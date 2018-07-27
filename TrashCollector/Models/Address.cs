using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Address
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string Zipcode { get; set; }
    }
}