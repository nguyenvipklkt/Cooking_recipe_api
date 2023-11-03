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
    public class FoodController : BaseApiController<FoodController>
    {
        private readonly FoodService _foodService;
        public FoodController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _foodService = new FoodService(apiConfig, databaseContext, mapper, webHost);
        }


        [HttpPost]
        [Route("CreateFood")]
        public MessageData CreateFood([FromForm] CreateFoodRequest request)
        {
            try
            {
                var res = _foodService.CreateFood(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFoodList")]
        public MessageData GetFoodList()
        {
            try
            {
                var res = _foodService.GetFoodList(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
