using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Text { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public int ReviewId { get; set; }

        public Review Review { get; set; }

    }
}
