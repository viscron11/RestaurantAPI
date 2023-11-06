﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI3.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Descritpion { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
