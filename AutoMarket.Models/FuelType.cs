using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Models
{
    public class FuelType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Fuel Type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
