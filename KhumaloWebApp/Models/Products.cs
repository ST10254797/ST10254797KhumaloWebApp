using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KhumaloWebApp.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Price { get; set; }

        [Required]
        public int AvailabilityNumber { get; set; }

        [NotMapped]
        public bool Availability
        {
            get { return AvailabilityNumber != 0; }
            set { AvailabilityNumber = value ? 1 : 0; }
        }

        [StringLength(255)]
        public string ImageUrl { get; set; }


    }
}
