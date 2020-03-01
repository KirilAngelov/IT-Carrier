using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatelApp.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Clients { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Lunch { get; set; }
        public bool AllInclusive { get; set; }

        public double Bill { get; set; }
    }
}
