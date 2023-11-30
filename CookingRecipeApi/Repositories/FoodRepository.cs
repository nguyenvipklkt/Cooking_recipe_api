﻿using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;
using CookingRecipeApi.Respositories;
using CookingRecipeApi.Utility;

namespace CookingRecipeApi.Repositories
{
    public class FoodRepository : BaseRespository<Food>
    {
        private IMapper _mapper;
        public FoodRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
