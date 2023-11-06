using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI3.Models
{
    public class CustomerDto2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }
    }
}
