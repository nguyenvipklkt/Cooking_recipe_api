﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingRecipeApi.Request
{
    public class CreateFoodStepInStoryRequest
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
