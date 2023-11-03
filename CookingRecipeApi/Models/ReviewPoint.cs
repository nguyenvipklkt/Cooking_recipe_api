namespace CookingRecipeApi.Models
{
    public class ReviewPoint : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public int Point { get; set; }

    }
}
