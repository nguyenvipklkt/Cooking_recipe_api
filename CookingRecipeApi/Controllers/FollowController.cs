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
    public class FollowController : BaseApiController<FollowController>
    {
        private readonly FollowService _followService;
        public FollowController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _followService = new FollowService(apiConfig, databaseContext, mapper, webHost, connectionManager);
        }

        [HttpGet]
        [Route("GetFollowUser")]
        public MessageData GetFollowUser()
        {
            try
            {
                var res = _followService.GetFollowUser(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFollowOtherUser")]
        public MessageData GetFollowOtherUser(int userId)
        {
            try
            {
                var res = _followService.GetFollowUser(userId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFollower")]
        public MessageData GetFollower()
        {
            try
            {
                var res = _followService.GetFollower(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetFollowerInOther")]
        public MessageData GetFollowerInOther(int userId)
        {
            try
            {
                var res = _followService.GetFollower(userId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("Follow")]
        public MessageData Follow(FollowRequest request)
        {
            try
            {
                var res = _followService.Follow(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpDelete]
        [Route("UnFollow")]
        public MessageData UnFollow(int followingUserId)
        {
            try
            {
                var res = _followService.UnFollow(UserId, followingUserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
