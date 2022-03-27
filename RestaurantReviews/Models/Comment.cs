namespace RestaurantReviews.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Text { get; set; }

        public string? Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ReviewId { get; set; }

        public Review Review { get; set; }

    }
}
