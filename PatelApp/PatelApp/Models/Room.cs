using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatelApp.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Range(1, 15, ErrorMessage = "Can only be between 1 .. 15")]
        public int Capacity { get; set; }
        [Required]
        public string Type { get; set; }
        public bool Occupied { get; set; }

        public double AdultPrice { get; set; }

        public double ChildPrice { get; set; }

        public enum RoomStatus
        {
            TwoBeds = 1,
            Apartment = 2,
            DoubleBed = 3,
            Penthouse = 4,
            Maisonette = 5
        }
    }
}
