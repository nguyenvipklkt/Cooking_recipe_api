namespace CookingRecipeApi.Models
{
    public class Food : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Thumbnails { get; set; } = "";
        public int ViewNumber { get; set; } = 0;
        public int LikeNumber { get; set; } = 0;
        public int ShareNumber { get; set; } = 0;
        public int AccessRange { get; set; } = 0;
    }
}
