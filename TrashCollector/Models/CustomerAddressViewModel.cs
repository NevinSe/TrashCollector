using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerAddressViewModel
    {
        public Customer customer { get; set; }
        public Address address { get; set; }
        public PickUps PickUps { get; set; }
    }
}