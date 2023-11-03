using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using System.Threading.Tasks;

namespace CookingRecipeApi.Services
{
    public class IngredientsService
    {
        private readonly IngredientListRepository _ingredientListRepository;
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public IngredientsService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _ingredientListRepository = new IngredientListRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object CreateIngredient(CreateIngredientsRequest request)
        {
            try
            {
                var newIngredient = _mapper.Map<IngredientList>(request);

                _ingredientListRepository.Create(newIngredient);
                _ingredientListRepository.SaveChange();
                return newIngredient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetIngredientList(GetFoodStepRequest request)
        {
            try
            {
                return _ingredientListRepository.FindByCondition(row => request.FoodId == row.FoodId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
