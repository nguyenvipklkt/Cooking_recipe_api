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
    public class ReviewController : BaseApiController<ReviewController>
    {
        private readonly ReviewService _reviewService;
        public ReviewController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _reviewService = new ReviewService(apiConfig, databaseContext, mapper, webHost, connectionManager);
        }

        [HttpGet]
        [Route("GetListComment")]
        public MessageData GetListComment(int foodId)
        {
            try
            {
                var res = _reviewService.GetCommentList(foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("CreateComent")]
        public MessageData CreateComent(CreateCommentRequest request)
        {
            try
            {
                var res = _reviewService.Comment(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteComment")]
        public MessageData DeleteComment(int commentId)
        {
            try
            {
                var res = _reviewService.DeleteComment(UserId, commentId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
