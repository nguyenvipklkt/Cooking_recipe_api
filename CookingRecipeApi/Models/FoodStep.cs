namespace CookingRecipeApi.Models
{
    public class FoodStep : BaseModel
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int NoStep { get; set; }

    }
}
