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
    public class ReviewPointController : BaseApiController<ReviewPointController>
    {
        private readonly ReviewPointService _reviewPointService;
        public ReviewPointController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _reviewPointService = new ReviewPointService(apiConfig, databaseContext, mapper, webHost, connectionManager);
        }


        [HttpPost]
        [Route("ReviewPoint")]
        public MessageData ReviewPoint(CreateReviewPointRequest request)
        {
            try
            {
                var res = _reviewPointService.ReviewPoint(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPut]
        [Route("UpdatePoint")]
        public MessageData UpdatePoint(UpdatePointRequest request)
        {
            try
            {
                var res = _reviewPointService.UpdatePoint(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetListReviewPoint")]
        public MessageData GetListReviewPoint(int foodId)
        {
            try
            {
                var res = _reviewPointService.GetListReviewPoint(foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("CheckReviewPoint")]
        public MessageData CheckReviewPoint(int foodId)
        {
            try
            {
                var res = _reviewPointService.CheckReviewPoint(UserId,foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("getPointInStory")]
        public MessageData getPointInStory(int foodId)
        {
            try
            {
                var res = _reviewPointService.getPointInStory(foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }


}
