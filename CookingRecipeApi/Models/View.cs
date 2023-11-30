using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipeApi.Models
{
    public class View : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodId { get; set; }
    }
}
