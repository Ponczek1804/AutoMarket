using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1900,2023)]
        [Display(Name = "Year of production")]
        public int YearOfProduction { get; set; }

        [Required]
        [Range(1000, 2000000)]
        public int Mileage { get; set; }

        [Required]
        [Range(1,1000000)]
        [Display(Name = "Discount price")]
        public double DiscountPrice { get; set; }

        [Required]
        [Range(1, 1000000)]
        [Display(Name = "Regular price")]
        public double Price { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand Brand { get; set; }

        [Required]
        [Display(Name = "Fuel type")]
        public int FuelTypeId { get; set; }
        [ForeignKey("FuelTypeId")]
        [ValidateNever]
        public FuelType FuelType { get; set; }
    }
}
