using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Helpers.SocketHelper;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using System.Threading.Tasks;

namespace CookingRecipeApi.Services
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly FoodRepository _foodRepository;
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public NotificationService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _notificationRepository = new NotificationRepository(apiOption, databaseContext, mapper, connectionManager);
            _foodRepository = new FoodRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object GetNotification(int userId)
        {
            try
            {
                var notificationList = _notificationRepository.FindByCondition(row => row.UserId == userId).ToList();
                List<NotifyDto> notificationListDto = new List<NotifyDto>();
                for (int i = 0; i < notificationList.Count(); i++)
                {
                    if (notificationList[i].Type == 2)
                    {
                        var notifyDto = new NotifyDto
                        {
                            Id = notificationList[i].Id,
                            UserId = notificationList[i].UserId,
                            ReactingUserId = notificationList[i].ReactingUserId,
                            FoodId = notificationList[i].FoodId,
                            Type = notificationList[i].Type,
                            Content = notificationList[i].Content,
                            ReactingUserFirstName = _userRepository.FindByCondition(row => notificationList[i].ReactingUserId == row.Id).FirstOrDefault().FirstName,
                            ReactingUserLastName = _userRepository.FindByCondition(row => notificationList[i].ReactingUserId == row.Id).FirstOrDefault().LastName,
                            ReactingUserAvatar = _userRepository.FindByCondition(row => notificationList[i].ReactingUserId == row.Id).FirstOrDefault().Avatar,
                            FoodName = "",
                            CreatedDate = notificationList[i].CreatedDate,
                        };
                        notificationListDto.Add(notifyDto);

                    }
                    else
                    {
                        var notifyDto = new NotifyDto
                        {
                            Id = notificationList[i].Id,
                            UserId = notificationList[i].UserId,
                            ReactingUserId = notificationList[i].ReactingUserId,
                            FoodId = notificationList[i].FoodId,
                            Type = notificationList[i].Type,
                            Content = notificationList[i].Content,
                            ReactingUserFirstName = _userRepository.FindByCondition(row => notificationList[i].ReactingUserId == row.Id).FirstOrDefault().FirstName,
                            ReactingUserLastName = _userRepository.FindByCondition(row => notificationList[i].ReactingUserId == row.Id).FirstOrDefault().LastName,
                            ReactingUserAvatar = _userRepository.FindByCondition(row => notificationList[i].ReactingUserId == row.Id).FirstOrDefault().Avatar,
                            FoodName = _foodRepository.FindByCondition(row => notificationList[i].FoodId == row.Id).FirstOrDefault().Name,
                            CreatedDate = notificationList[i].CreatedDate,
                        };
                        notificationListDto.Add(notifyDto);

                    }
                }
                List<NotifyDto> get10notificationList = notificationListDto.Skip(Math.Max(0, notificationList.Count - 10)).ToList();
                return notificationListDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
