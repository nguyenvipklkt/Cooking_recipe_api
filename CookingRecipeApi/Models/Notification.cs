namespace CookingRecipeApi.Models
{
    public class Notification : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReactingUserId { get; set; }
        public int FoodId { get; set; }
        public int Type { get; set; } // 0: reviewPoint ; 1: Comment ; 2: Follow; 3: Have a new post
        public string Content { get; set; }
    }
}
