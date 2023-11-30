using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class UpdateStoryRequest
    {
        public int Id { get; set; }
        public int FoodTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string Ingredients { get; set; }
        public string FoodSteps { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int Meal { get; set; }
        public int LevelOfDifficult { get; set; }
        public IFormFile? Video { get; set; }
    }
}
