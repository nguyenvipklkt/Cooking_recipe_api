﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class CreateIngredientInStoryRequest
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
    }
}
