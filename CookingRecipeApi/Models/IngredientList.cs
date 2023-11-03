namespace CookingRecipeApi.Models
{
    public class IngredientList : BaseModel
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

    }
}
