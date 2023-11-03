using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class CreateFoodRequest
    {
        public int FoodTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public IFormFile? Thumbnails { get; set; }
        public int AccessRange { get; set; } = 1;
    }
}
