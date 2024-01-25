using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class CreateReviewPointRequest
    {
        public int FoodId { get; set; }
        public int Point { get; set; }
    }
}
