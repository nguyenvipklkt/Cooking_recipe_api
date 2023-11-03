using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Respositories;

namespace CookingRecipeApi.Repositories
{
    public class FoodStepRepository : BaseRespository<FoodStep>
    {
        private IMapper _mapper;
        public FoodStepRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
