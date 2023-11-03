using System;

namespace CookingRecipeApi.Models
{
    public class BaseModel
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
    }
}
