using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipeApi.Models
{
    public class KeySearch : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Keyword { get; set; } = "";
    }
}
