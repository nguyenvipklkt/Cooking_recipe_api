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
                var res = _foodService.GetFoodListWithUser(UserId, UserId);
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
                var res = _foodService.GetFoodListWithUser(userId, UserId);
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

        [HttpGet]
        [Route("GetFoodByFollowingUser")]
        public MessageData GetFoodByFollowingUser()
        {
            try
            {
                var res = _foodService.GetFoodByFollowingUser(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFoodFromFoodType")]
        public MessageData GetFoodFromFoodType(int foodTypeId)
        {
            try
            {
                var res = _foodService.GetFoodFromFoodType(foodTypeId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("Search")]
        public MessageData Search(string keyword)
        {
            try
            {
                var res = _foodService.Search(UserId, keyword);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteSearch")]
        public MessageData DeleteSearch(int id)
        {
            try
            {
                var res = _foodService.DeleteSearch(id);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetKeySearch")]
        public MessageData GetKeySearch()
        {
            try
            {
                var res = _foodService.GetKeySearch(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPut]
        [Route("UpdateStatusFood")]
        public MessageData UpdateStatusFood(int foodId)
        {
            try
            {
                var res = _foodService.UpdateStatusFood(foodId, UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }


}
