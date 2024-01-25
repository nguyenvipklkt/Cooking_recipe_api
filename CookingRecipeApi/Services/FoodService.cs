using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using System.Drawing;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CookingRecipeApi.Services
{
    public class FoodService
    {
        private readonly ReviewPointRepository _reviewPointRepository;
        private readonly FoodRepository _foodRepository;
        private readonly FollowRepository _followRepository;
        private readonly KeySearchRepository _keySearchRepository;
        private readonly UserRepository _userRepository;
        DatabaseContext databaseContext;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public FoodService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _reviewPointRepository = new ReviewPointRepository(apiOption, databaseContext, mapper);
            _foodRepository = new FoodRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _followRepository = new FollowRepository(apiOption, databaseContext, mapper);
            _keySearchRepository = new KeySearchRepository(apiOption, databaseContext, mapper);
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

                if (request.Video != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\foods\\videos\\" + date + request.Video.FileName))
                    {
                        request.Video.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    newFood.Video = "foods/videos/" + date + request.Video.FileName;
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

        public object GetFoodListWithUser(int userId, int checkUserId)
        {
            try
            {
                var food = _foodRepository.FindByCondition(row=> userId == row.UserId).ToList();
                if(userId == checkUserId)
                {
                    List<object> results = new List<object>();
                    for (int i = 0; i < food.Count; i++)
                    {
                        var reviewPointList = _reviewPointRepository.FindByCondition(row => food[i].Id == row.FoodId).ToList();
                        double averagePoint;
                        if (reviewPointList.Count == 0)
                        {
                            averagePoint = 5;
                        }
                        else
                        {
                            List<int> pointList = new List<int>();
                            for (int j = 0; j < reviewPointList.Count; j++)
                            {
                                pointList.Add(reviewPointList[j].Point);
                            }
                            averagePoint = pointList.Average();
                        }
                        var story = new
                        {
                            food = food[i],
                            point = averagePoint,
                        };
                        results.Add(story);
                    }
                    return results;
                }
                else
                {
                    List<object> results = new List<object>();
                    var checkFollowing = _followRepository.FindByCondition(row => userId == row.FollowingUserId && checkUserId == row.UserId).FirstOrDefault();
                    if (checkFollowing == null)
                    {
                        for (int i = 0; i < food.Count; i++)
                        {
                            if (food[i].AccessRange == 1)
                            {
                                var reviewPointList = _reviewPointRepository.FindByCondition(row => food[i].Id == row.FoodId).ToList();
                                double averagePoint;
                                if (reviewPointList.Count == 0)
                                {
                                    averagePoint = 5;
                                }
                                else
                                {
                                    List<int> pointList = new List<int>();
                                    for (int j = 0; j < reviewPointList.Count; j++)
                                    {
                                        pointList.Add(reviewPointList[j].Point);
                                    }
                                    averagePoint = pointList.Average();
                                }
                                var story = new
                                {
                                    food = food[i],
                                    point = averagePoint,
                                };
                                results.Add(story);
                            }
                        }
                        
                    }
                    else
                    {
                        for (int i = 0; i < food.Count; i++)
                        {
                            var reviewPointList = _reviewPointRepository.FindByCondition(row => food[i].Id == row.FoodId).ToList();
                            double averagePoint;
                            if (reviewPointList.Count == 0)
                            {
                                averagePoint = 5;
                            }
                            else
                            {
                                List<int> pointList = new List<int>();
                                for (int j = 0; j < reviewPointList.Count; j++)
                                {
                                    pointList.Add(reviewPointList[j].Point);
                                }
                                averagePoint = pointList.Average();
                            }
                            var story = new
                            {
                                food = food[i],
                                point = averagePoint,
                            };
                            results.Add(story);
                        }
                    }

                    return results;
                }
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
                    var reviewPointList = _reviewPointRepository.FindByCondition(row => food[i].Id == row.FoodId).ToList();
                    double averagedPoint;
                    if (reviewPointList.Count == 0)
                    {
                        averagedPoint = 5;
                    }
                    else
                    {
                        List<int> pointList = new List<int>();
                        for (int j = 0; j < reviewPointList.Count; j++)
                        {
                            pointList.Add(reviewPointList[j].Point);
                        }
                        averagedPoint = pointList.Average();
                    }
                    var storyInf = new StoryDto
                    {
                        Id = food[i].Id,
                        UserId = food[i].UserId,
                        Name = food[i].Name,
                        Thumbnails = food[i].Thumbnails,
                        FoodPlaceId = food[i].FoodPlaceId,
                        SeasonalFoodId = food[i].SeasonalFoodId,
                        FoodTypeId = food[i].FoodTypeId,
                        Title = food[i].Title,
                        ViewNumber = food[i].ViewNumber,
                        LikeNumber = food[i].LikeNumber,
                        ShareNumber = food[i].ShareNumber,
                        CookingTime = food[i].CookingTime,
                        PreparationTime = food[i].PreparationTime,
                        Meal = food[i].Meal,
                        LevelOfDifficult = food[i].LevelOfDifficult,
                        Video = food[i].Video,
                        reviewPoint = averagedPoint,
                        AuthorFirstName = _userRepository.FindByCondition(row => food[i].UserId == row.Id).FirstOrDefault().FirstName,
                        AuthorLastName = _userRepository.FindByCondition(row => food[i].UserId == row.Id).FirstOrDefault().LastName,
                        AuthorAvatar = _userRepository.FindByCondition(row => food[i].UserId == row.Id).FirstOrDefault().Avatar,
                        CreatedDate = food[i].CreatedDate,
                    };
                    if (food[i].AccessRange == 1)
                    {
                        storyList.Add(storyInf);
                    }
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

        public object GetFoodByFollowingUser(int userId)
        {
            try
            {
                var follow = _followRepository.FindByCondition(row => userId == row.UserId).ToList();
                if (follow == null)
                {
                    return "No follow, follow other people to receive new post!";
                }

                List<StoryDto> storyList = new List<StoryDto>();

                for (int i = 0; i < follow.Count; i ++)
                {
                    var food = _foodRepository.FindByCondition(row => follow[i].FollowingUserId == row.UserId).ToList();
                    for (int j = 0; j < food.Count; j ++)
                    {
                        var reviewPointList = _reviewPointRepository.FindByCondition(row => food[j].Id == row.FoodId).ToList();
                        double averagePoint;
                        if (reviewPointList.Count == 0)
                        {
                            averagePoint = 5;
                        }
                        else
                        {
                            List<int> pointList = new List<int>();
                            for (int k = 0; k < reviewPointList.Count; k++)
                            {
                                pointList.Add(reviewPointList[k].Point);
                            }
                            averagePoint = pointList.Average();
                        }
                        var user = _userRepository.FindByCondition(a => food[j].UserId == a.Id).FirstOrDefault();
                        var storyDto = new StoryDto 
                        {
                            Id = food[j].Id,
                            UserId = food[j].UserId,
                            FoodTypeId = food[j].FoodTypeId,
                            Name = food[j].Name,
                            Title = food[j].Title,
                            FoodPlaceId = food[j].FoodPlaceId,
                            SeasonalFoodId = food[j].SeasonalFoodId,
                            Thumbnails = food[j].Thumbnails,
                            ViewNumber = food[j].ViewNumber,
                            LikeNumber  = food[j].LikeNumber,
                            ShareNumber = food[j].ShareNumber,
                            CookingTime = food[j].CookingTime,
                            PreparationTime = food[j].PreparationTime,
                            Meal = food[j].Meal,
                            LevelOfDifficult = food[j].LevelOfDifficult,
                            Video = food[j].Video,
                            reviewPoint = averagePoint,
                            AuthorFirstName = user.FirstName,
                            AuthorLastName = user.LastName,
                            AuthorAvatar = user.Avatar,
                            CreatedDate = food[j].CreatedDate,
                            
                        };
                        storyList.Add(storyDto);
                    }
                }
                storyList.Sort((food1, food2) => food2.CreatedDate.CompareTo(food1.CreatedDate));
                return storyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFoodFromFoodType(int? foodTypeId, int? foodPlaceId, int? seasonalFoodId)
        {
            try
            {
                List<Food> foodList = new List<Food>();
                if (foodTypeId == null && foodPlaceId == null && seasonalFoodId == null)
                {
                    throw new Exception("No search");
                }
                else if (foodTypeId == null && foodPlaceId == null)
                {
                    foodList = _foodRepository.FindByCondition(row => seasonalFoodId == row.SeasonalFoodId).ToList();
                }
                else if (foodPlaceId == null && seasonalFoodId == null)
                {
                    foodList = _foodRepository.FindByCondition(row => foodTypeId == row.FoodTypeId).ToList();
                }
                else if (foodTypeId == null && seasonalFoodId == null)
                {
                    foodList = _foodRepository.FindByCondition(row => foodPlaceId == row.FoodPlaceId).ToList();
                }
                else if (foodTypeId == null)
                {
                    foodList = _foodRepository.FindByCondition(row => foodPlaceId == row.FoodPlaceId && seasonalFoodId == row.SeasonalFoodId).ToList();
                }
                else if (foodPlaceId == null)
                {
                    foodList = _foodRepository.FindByCondition(row => foodTypeId == row.FoodTypeId && seasonalFoodId == row.SeasonalFoodId).ToList();
                }
                else if (seasonalFoodId == null)
                {
                    foodList = _foodRepository.FindByCondition(row => foodTypeId == row.FoodTypeId && foodPlaceId == row.FoodPlaceId).ToList();
                }
                else
                {
                    foodList = _foodRepository.FindByCondition(row => foodTypeId == row.FoodTypeId && foodPlaceId == row.FoodPlaceId && seasonalFoodId == row.SeasonalFoodId).ToList();
                }
                List<StoryDto> stories = new List<StoryDto>();
                for (int i = 0; i < foodList.Count; i++)
                {
                    var user = _userRepository.FindByCondition(a => foodList[i].UserId == a.Id).FirstOrDefault();
                    var reviewPointList = _reviewPointRepository.FindByCondition(row => foodList[i].Id == row.FoodId).ToList();
                    double averagePoint;
                    if (reviewPointList.Count == 0)
                    {
                        averagePoint = 5;
                    }
                    else
                    {
                        List<int> pointList = new List<int>();
                        for (int k = 0; k < reviewPointList.Count; k++)
                        {
                            pointList.Add(reviewPointList[k].Point);
                        }
                        averagePoint = pointList.Average();
                    }
                    var storyDto = new StoryDto
                    {
                        Id = foodList[i].Id,
                        UserId = foodList[i].UserId,
                        FoodTypeId = foodList[i].FoodTypeId,
                        FoodPlaceId = foodList[i].FoodPlaceId,
                        SeasonalFoodId = foodList[i].SeasonalFoodId,
                        Name = foodList[i].Name,
                        Title = foodList[i].Title,
                        Thumbnails = foodList[i].Thumbnails,
                        ViewNumber = foodList[i].ViewNumber,
                        LikeNumber = foodList[i].LikeNumber,
                        ShareNumber = foodList[i].ShareNumber,
                        CookingTime = foodList[i].CookingTime,
                        PreparationTime = foodList[i].PreparationTime,
                        Meal = foodList[i].Meal,
                        LevelOfDifficult = foodList[i].LevelOfDifficult,
                        Video = foodList[i].Video,
                        reviewPoint = averagePoint,
                        AuthorFirstName = user.FirstName,
                        AuthorLastName = user.LastName,
                        AuthorAvatar = user.Avatar,
                        CreatedDate = foodList[i].CreatedDate,

                    };
                    if (foodList[i].AccessRange == 1)
                    {
                        stories.Add(storyDto);
                    }
                }
                return stories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object Search(int UserId, string keyword)
        {
            try
            {
                var food = _foodRepository.FindAll();
                if (!string.IsNullOrEmpty(keyword))
                {
                    food = food.Where(row => row.Name.ToLower().Contains(keyword.ToLower()) || keyword.ToLower().Contains(row.Name.ToLower()));
                }
                if (keyword == "" || keyword == null)
                {
                    throw new Exception("Not key search !");
                }
                var foodList = food.ToList();
                var checkKeyWord = _keySearchRepository.FindByCondition(row => keyword == row.Keyword && UserId == row.UserId).FirstOrDefault();
                if (checkKeyWord != null)
                {
                    checkKeyWord.UpdatedDate = DateTime.Now;
                    _keySearchRepository.UpdateByEntity(checkKeyWord);
                    _keySearchRepository.SaveChange();
                }
                else
                {
                    checkKeyWord = new KeySearch();
                    checkKeyWord.UserId = UserId;
                    checkKeyWord.Keyword = keyword;
                    checkKeyWord.UpdatedDate = DateTime.Now;
                    checkKeyWord.CreatedDate = DateTime.Now;
                    _keySearchRepository.Create(checkKeyWord);
                    _keySearchRepository.SaveChange();
                }
                List<StoryDto> stories = new List<StoryDto>();
                for (int i = 0; i < foodList.Count; i++)
                {
                    var user = _userRepository.FindByCondition(a => foodList[i].UserId == a.Id).FirstOrDefault();
                    var reviewPointList = _reviewPointRepository.FindByCondition(row => foodList[i].Id == row.FoodId).ToList();
                    double averagePoint;
                    if (reviewPointList.Count == 0)
                    {
                        averagePoint = 5;
                    }
                    else
                    {
                        List<int> pointList = new List<int>();
                        for (int k = 0; k < reviewPointList.Count; k++)
                        {
                            pointList.Add(reviewPointList[k].Point);
                        }
                        averagePoint = pointList.Average();
                    }
                    var storyDto = new StoryDto
                    {
                        Id = foodList[i].Id,
                        UserId = foodList[i].UserId,
                        FoodTypeId = foodList[i].FoodTypeId,
                        FoodPlaceId = foodList[i].FoodPlaceId,
                        SeasonalFoodId = foodList[i].SeasonalFoodId,
                        Name = foodList[i].Name,
                        Title = foodList[i].Title,
                        Thumbnails = foodList[i].Thumbnails,
                        ViewNumber = foodList[i].ViewNumber,
                        LikeNumber = foodList[i].LikeNumber,
                        ShareNumber = foodList[i].ShareNumber,
                        CookingTime = foodList[i].CookingTime,
                        PreparationTime = foodList[i].PreparationTime,
                        Meal = foodList[i].Meal,
                        LevelOfDifficult = foodList[i].LevelOfDifficult,
                        Video = foodList[i].Video,
                        reviewPoint = averagePoint,
                        AuthorFirstName = user.FirstName,
                        AuthorLastName = user.LastName,
                        AuthorAvatar = user.Avatar,
                        CreatedDate = foodList[i].CreatedDate,

                    };
                    if (foodList[i].AccessRange == 1 )
                    {
                        stories.Add(storyDto);
                    }
                }
                stories.Sort((food1, food2) => food2.CreatedDate.CompareTo(food1.CreatedDate));
                return stories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetKeySearch(int userId)
        {
            try
            {
                var keyList = _keySearchRepository.FindByCondition(row => userId == row.UserId).ToList();
                keyList.OrderByDescending(keysearch => keysearch.UpdatedDate).ToList();
                for (int i = 0; i < keyList.Count; i++)
                {
                    for (int j = 0; j < keyList.Count; j ++)
                    {
                        if (keyList[i].UpdatedDate > keyList[j].UpdatedDate)
                        {
                            var a = keyList[i];
                            keyList[i] = keyList[j];
                            keyList[j] = a;
                        }
                    }
                }
                return keyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object DeleteSearch(int id)
        {
            var keySearch = _keySearchRepository.FindByCondition(row => id == row.Id).FirstOrDefault();
            if (keySearch == null)
            {
                throw new ValidateError(1001, "Keysearch dont exist!");
            }
            _keySearchRepository.DeleteByEntity(keySearch);
            _keySearchRepository.SaveChange();
            return keySearch;
        }

        public object UpdateStatusFood(int foodId, int userId)
        {
            try
            {
                var food = _foodRepository.FindByCondition(row => foodId == row.Id).FirstOrDefault();
                if (food == null)
                {
                    throw new Exception("Food not exist!");
                }
                if (userId == food.UserId)
                {
                    if (food.AccessRange == 0)
                    {
                        food.AccessRange = 1;
                        _foodRepository.UpdateByEntity(food);
                        _foodRepository.SaveChange();
                        return true;
                    }
                    else
                    {
                        food.AccessRange = 0;
                        _foodRepository.UpdateByEntity(food);
                        _foodRepository.SaveChange();
                        return true;
                    }
                }
                else
                {
                    throw new Exception("You do not have the right to change!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public object GetFoodInTrend()
        {
            try
            {
                var currentDate = DateTime.Now;
                List<Food> foodList = new List<Food>();
                if (currentDate.Month == 2 || currentDate.Month == 3 || currentDate.Month == 4)
                {
                    foodList = _foodRepository.FindByCondition(row => row.SeasonalFoodId == 1 || row.SeasonalFoodId == 6).ToList();
                }
                else if (currentDate.Month == 5 || currentDate.Month == 6 || currentDate.Month == 7)
                {
                    foodList = _foodRepository.FindByCondition(row => row.SeasonalFoodId == 2 || row.SeasonalFoodId == 5).ToList();
                }
                else if (currentDate.Month == 8 || currentDate.Month == 9 || currentDate.Month == 10)
                {
                    foodList = _foodRepository.FindByCondition(row => row.SeasonalFoodId == 3 || row.SeasonalFoodId == 5).ToList();
                }
                else if (currentDate.Month == 11 || currentDate.Month == 12 || currentDate.Month == 01)
                {
                    foodList = _foodRepository.FindByCondition(row => row.SeasonalFoodId == 4 || row.SeasonalFoodId == 6).ToList();
                }
                List<StoryDto> stories = new List<StoryDto>();
                for (int i = 0; i < foodList.Count; i++)
                {
                    var user = _userRepository.FindByCondition(a => foodList[i].UserId == a.Id).FirstOrDefault();
                    var storyDto = new StoryDto
                    {
                        Id = foodList[i].Id,
                        UserId = foodList[i].UserId,
                        FoodTypeId = foodList[i].FoodTypeId,
                        FoodPlaceId = foodList[i].FoodPlaceId,
                        SeasonalFoodId = foodList[i].SeasonalFoodId,
                        Name = foodList[i].Name,
                        Title = foodList[i].Title,
                        Thumbnails = foodList[i].Thumbnails,
                        ViewNumber = foodList[i].ViewNumber,
                        LikeNumber = foodList[i].LikeNumber,
                        ShareNumber = foodList[i].ShareNumber,
                        CookingTime = foodList[i].CookingTime,
                        PreparationTime = foodList[i].PreparationTime,
                        Meal = foodList[i].Meal,
                        LevelOfDifficult = foodList[i].LevelOfDifficult,
                        Video = foodList[i].Video,
                        AuthorFirstName = user.FirstName,
                        AuthorLastName = user.LastName,
                        AuthorAvatar = user.Avatar,
                        CreatedDate = foodList[i].CreatedDate,

                    };
                    if (foodList[i].AccessRange == 1)
                    {
                        stories.Add(storyDto);
                    }
                }
                //List<StoryDto> ranDomStories = new List<StoryDto>();
                //Random random = new Random();
                //for (int i = 0; i < 5; i++)
                //{
                //    int index = random.Next(0,stories.Count - 1);
                //    ranDomStories.Add(stories[index]);
                //    stories.RemoveAt(index);
                //}
                //return ranDomStories;
                return stories;
            }

            catch(Exception ex)
            {
                throw ex;
            }

        }
        //public object UpdateSSFandPF()
        //{
        //    var food = _foodRepository.FindByCondition(row => row.FoodPlaceId == 0 && row.SeasonalFoodId == 0).ToList();
        //    for (int i = 0; i < food.Count; i++)
        //    {
        //        Random TenBienRanDom = new Random();
        //        food[i].FoodPlaceId = TenBienRanDom.Next(1, 3);
        //        if (food[i].FoodPlaceId == 1 || food[i].FoodPlaceId == 2)
        //        {
        //            food[i].SeasonalFoodId = TenBienRanDom.Next(1, 4);
        //        }
        //        else if (food[i].FoodPlaceId == 3) {
        //            food[i].SeasonalFoodId = TenBienRanDom.Next(5, 6);
        //        }
        //        _foodRepository.UpdateByEntity(food[i]);
        //        _foodRepository.SaveChange();
        //    }
        //    return true;
        //}

    }
    
}
