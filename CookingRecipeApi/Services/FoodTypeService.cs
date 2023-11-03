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
    public class FoodTypeService
    {
        private readonly FoodTypeRepository _foodTypeRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public FoodTypeService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _foodTypeRepository = new FoodTypeRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object CreateFoodType(CreateFoodTypeRequest request)
        {
            try
            {
                var newFoodType = _mapper.Map<FoodType>(request);

                _foodTypeRepository.Create(newFoodType);
                _foodTypeRepository.SaveChange();
                return newFoodType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFoodTypeList()
        {
            try
            {
                return _foodTypeRepository.FindAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
