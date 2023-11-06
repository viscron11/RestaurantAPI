using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI3.Models
{
    public class CreateCustomerDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
        public int RestaurantId { get; set; }
    }
}
