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
    public class FoodTypeController : BaseApiController<FoodTypeController>
    {
        private readonly FoodTypeService _foodTypeService;
        public FoodTypeController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _foodTypeService = new FoodTypeService(apiConfig, databaseContext, mapper, webHost);
        }

        
        [HttpPost]
        [Route("CreateFoodType")]
        public MessageData CreateFoodType(CreateFoodTypeRequest request)
        {
            try
            {
                var res = _foodTypeService.CreateFoodType(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFoodTypeList")]
        public MessageData GetFoodTypeList()
        {
            try
            {
                var res = _foodTypeService.GetFoodTypeList();
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
