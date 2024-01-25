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
    public class ReviewPointService
    {
        private readonly ReviewPointRepository _reviewPointRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly UserRepository _userRepository;
        private readonly FoodRepository _foodRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public ReviewPointService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _reviewPointRepository = new ReviewPointRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _notificationRepository = new NotificationRepository(apiOption, databaseContext, mapper, connectionManager);
            _foodRepository = new FoodRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }
        public object ReviewPoint(int userId, CreateReviewPointRequest request) 
        {
            try
            {
                var story = _foodRepository.FindOrFail(request.FoodId);
                if (story == null)
                {
                    throw new Exception("Story doesn't exist!");
                }
                var checkedReviewPoint = _reviewPointRepository.FindByCondition(row => userId == row.UserId && request.FoodId == row.FoodId).FirstOrDefault();
                if (checkedReviewPoint != null)
                {
                    throw new Exception("Story was reviewed !");
                }
                if (request.Point < 1 || request.Point > 5)
                {
                    throw new Exception("Point must be in range (1, 5)");
                }
                var reviewPoint = new ReviewPoint();
                reviewPoint.UserId = userId;
                reviewPoint.FoodId = request.FoodId;
                reviewPoint.Point = request.Point;
                _reviewPointRepository.Create(reviewPoint);
                _reviewPointRepository.SaveChange();
                _notificationRepository.CreateNotification(story.UserId, userId, story.Id, 0);
                return reviewPoint;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object CheckReviewPoint(int userId, int foodId)
        {
            try
            {
                var checkedReviewPoint = _reviewPointRepository.FindByCondition(row => userId == row.UserId && foodId == row.FoodId).FirstOrDefault();
                if (checkedReviewPoint == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UpdatePoint(int userId, UpdatePointRequest request)
        {
            try
            {
                var checkedReviewPoint = _reviewPointRepository.FindByCondition(row => userId == row.UserId && request.FoodId == row.FoodId).FirstOrDefault();
                if (checkedReviewPoint == null)
                {
                    throw new Exception("You have not rated this story yet !");
                }
                checkedReviewPoint.Point = request.Point;
                checkedReviewPoint.UpdatedDate = DateTime.Now;
                _reviewPointRepository.UpdateByEntity(checkedReviewPoint);
                _reviewPointRepository.SaveChange();
                return checkedReviewPoint;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public object GetListReviewPoint(int foodId) 
        {
            try
            {
                return _reviewPointRepository.FindByCondition(row => foodId == row.FoodId).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public object getPointInStory(int foodId)
        {
            try
            {
                var reviewPointList = _reviewPointRepository.FindByCondition(row => foodId == row.FoodId).ToList();
                if (reviewPointList.Count == 0)
                {
                    int averagedPoint = 5;
                    return averagedPoint;
                } 
                List<int> pointList = new List<int>();
                for(int i = 0; i < reviewPointList.Count; i ++)
                {
                    pointList.Add(reviewPointList[i].Point);
                }
                var averagePoint = pointList.Average();
                return averagePoint;
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }
    }
    
}
