﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatelApp.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public bool Senior { get; set; }
    }
}
