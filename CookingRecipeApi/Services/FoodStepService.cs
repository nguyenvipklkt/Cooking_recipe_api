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
    public class FoodStepService
    {
        private readonly FoodStepRepository _foodStepRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public FoodStepService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _foodStepRepository = new FoodStepRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object CreateFoodStep(CreateFoodStepRequest request)
        {
            try
            {
                var newFoodStep = _mapper.Map<FoodStep>(request);

                _foodStepRepository.Create(newFoodStep);
                _foodStepRepository.SaveChange();
                return newFoodStep;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFoodStepList(GetFoodStepRequest request)
        {
            try
            {
                return _foodStepRepository.FindOrFail(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
