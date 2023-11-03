using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Respositories;

namespace CookingRecipeApi.Repositories
{
    public class FoodTypeRepository : BaseRespository<FoodType>
    {
        private IMapper _mapper;
        public FoodTypeRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
