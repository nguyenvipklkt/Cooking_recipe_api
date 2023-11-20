using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Helpers.SocketHelper;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;
using CookingRecipeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CookingRecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodStepController : BaseApiController<FoodStepController>
    {
        private readonly FoodStepService _foodStepService;
        public FoodStepController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _foodStepService = new FoodStepService(apiConfig, databaseContext, mapper, webHost);
        }

        
        [HttpPost]
        [Route("CreateFoodStep")]
        public MessageData CreateFoodStep(CreateFoodStepRequest request)
        {
            try
            {
                var res = _foodStepService.CreateFoodStep(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFoodStepList")]
        public MessageData GetFoodStepList(int FoodId)
        {
            try
            {
                var res = _foodStepService.GetFoodStepList(FoodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
