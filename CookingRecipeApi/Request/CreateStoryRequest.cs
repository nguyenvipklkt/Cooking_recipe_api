using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class CreateStoryRequest
    {
        public int FoodTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string Ingredients { get; set; }
        public string FoodSteps { get; set; }
    }
}
