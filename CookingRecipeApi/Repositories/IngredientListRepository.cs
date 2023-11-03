using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;
using CookingRecipeApi.Respositories;
using CookingRecipeApi.Utility;

namespace CookingRecipeApi.Repositories
{
    public class IngredientListRepository : BaseRespository<IngredientList>
    {
        private IMapper _mapper;
        public IngredientListRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
 
    }
}
