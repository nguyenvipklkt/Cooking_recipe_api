using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class CreateFoodStepRequest
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int NoStep { get; set; }
    }
}
