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
    public class NotificationController : BaseApiController<NotificationController>
    {
        private readonly NotificationService _notificationService;
        public NotificationController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _notificationService = new NotificationService(apiConfig, databaseContext, mapper, webHost, connectionManager);
        }

        [HttpGet]
        [Route("GetNotification")]
        public MessageData GetNotification()
        {
            try
            {
                var res = _notificationService.GetNotification(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
