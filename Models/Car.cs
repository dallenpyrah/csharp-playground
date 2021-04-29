using System.ComponentModel.DataAnnotations;

namespace csharp_playground.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Color { get; set; }

        public int Price { get; set; }

        public int TopSpeed { get; set; }

        [Required]
        public int Year { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }
    }
}