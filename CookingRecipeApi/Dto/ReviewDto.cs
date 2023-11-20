using CookingRecipeApi.Models;

namespace CookingRecipeApi.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public string Content { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
