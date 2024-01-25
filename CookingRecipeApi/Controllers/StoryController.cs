using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Helpers.SocketHelper;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;
using CookingRecipeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CookingRecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : BaseApiController<StoryController>
    {
        private readonly StoryService _storyService;
        public StoryController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _storyService = new StoryService(apiConfig, databaseContext, mapper, webHost, connectionManager);
        }


        [HttpPost]
        [Route("CreateStory")]
        public MessageData CreateStory([FromForm] CreateStoryRequest request)
        {
            try
            {
                var res = _storyService.CreateStory(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPut]
        [Route("UpdateStory")]
        public MessageData UpdateStory([FromForm] UpdateStoryRequest request)
        {
            try
            {
                var res = _storyService.updateStory(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteStory")]

        public MessageData DeleteStory(int foodId)
        {
            try
            {
                var res = _storyService.deleteStory(UserId, foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("AddView")]

        public MessageData AddView(int foodId)
        {
            try
            {
                var res = _storyService.addView(UserId, foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetStoryHighestView")]
        public MessageData GetStoryHighestView()
        {
            try
            {
                var res = _storyService.GetStoryHighestView();
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }


}
