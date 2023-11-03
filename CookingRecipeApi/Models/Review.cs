namespace CookingRecipeApi.Models
{
    public class Review : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public string Content { get; set; }

    }
}
