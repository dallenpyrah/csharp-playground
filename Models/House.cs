using System.ComponentModel.DataAnnotations;

namespace csharp_playground.Models
{
    public class House
    {
        public int Id { get; set; }

        [Required]
        public int PricePerNight { get; set; }

        [Required]
        [Range(100, 1000000)]
        public int SqaureFeet { get; set; }
        [Required]
        public string Location { get; set; }

        [Required]
        [Range(0, 100)]
        public int Bedrooms { get; set; }
        [Required]
        [Range(0, 100)]
        public int Bathrooms { get; set; }

        public int GuestLimit { get; set; }

        public string Image { get; set; }
        [Range(0, 5)]
        public int Reviews { get; set; }

        public string DateAvaliable { get; set; }

        public bool SuperHost { get; set; }
        
    }
}