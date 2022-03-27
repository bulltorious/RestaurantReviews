using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(14,2)")]
        [Range(0, 4)]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        [Range(0, 4)]
        public decimal Rating { get; set; }

        public string? Text { get; set; }

        public string? Bullet { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
