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
        [Route("GetFoodListWithUser")]
        public MessageData GetFoodListWithUser()
        {
            try
            {
                var res = _foodService.GetFoodListWithUser(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFoodListWithOtherUser")]
        public MessageData GetFoodListWithOtherUser(int userId)
        {
            try
            {
                var res = _foodService.GetFoodListWithUser(userId);
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
                var res = _foodService.GetFoodList();
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFood")]
        public MessageData GetFood(int foodId)
        {
            try
            {
                var res = _foodService.GetFood(foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetUserInFood")]
        public MessageData GetUserInFood(int foodId)
        {
            try
            {
                var res = _foodService.GetUserInFood(foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
