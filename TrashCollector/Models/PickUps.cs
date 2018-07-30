using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class PickUps
    {
        [Key]
        public int PickUpId { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime? PickUpDate { get; set; }
        public double Cost { get; set; }
    }
}