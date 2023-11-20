using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class CreateCommentRequest
    {
        public int FoodId { get; set; }
        public string Content { get; set; }
    }
}
