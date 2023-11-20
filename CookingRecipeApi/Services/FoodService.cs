using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CookingRecipeApi.Services
{
    public class FoodService
    {
        private readonly FoodRepository _foodRepository;
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public FoodService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _foodRepository = new FoodRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object CreateFood(int userId ,CreateFoodRequest request)
        {
            try
            {
                var newFood = _mapper.Map<Food>(request);
                newFood.Thumbnails = "";
                if (request.Thumbnails != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\foods\\thumbnails\\" + date + request.Thumbnails.FileName))
                    {
                        request.Thumbnails.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    newFood.Thumbnails = "foods/thumbnails/" + date + request.Thumbnails.FileName;
                }

                newFood.UserId = userId;
                _foodRepository.Create(newFood);
                _foodRepository.SaveChange();
                return newFood;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFoodListWithUser(int userId)
        {
            try
            {
                return _foodRepository.FindByCondition(row=> userId == row.UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFoodList()
        {
            try
            {

                var food = _foodRepository.FindAll().ToList();
                
                List<StoryDto> storyList = new List<StoryDto>();
                for (int i = 0; i < food.Count; i ++)
                {
                    var storyInf = new StoryDto
                    {
                        Id = food[i].Id,
                        UserId = food[i].UserId,
                        Name = food[i].Name,
                        Thumbnails = food[i].Thumbnails,
                        Title = food[i].Title,
                        ViewNumber = food[i].ViewNumber,
                        LikeNumber = food[i].LikeNumber,
                        ShareNumber = food[i].ShareNumber,
                        AuthorFirstName = _userRepository.FindByCondition(row => food[i].UserId == row.Id).FirstOrDefault().FirstName,
                        AuthorLastName = _userRepository.FindByCondition(row => food[i].UserId == row.Id).FirstOrDefault().LastName,
                        AuthorAvatar = _userRepository.FindByCondition(row => food[i].UserId == row.Id).FirstOrDefault().Avatar,
                        CreatedDate = food[i].CreatedDate,
                    };
                    storyList.Add(storyInf);
                }

                return storyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFood(int foodId)
        {
            try
            {
                return _foodRepository.FindByCondition(row => foodId == row.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetUserInFood(int foodId)
        {
            try
            {
                var food = _foodRepository.FindByCondition(row => foodId == row.Id).FirstOrDefault();
                var user = _userRepository.FindByCondition(row => food.UserId == row.Id).FirstOrDefault();
                var userInf = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Avatar = user.Avatar,
                };
                return userInf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
