using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CookingRecipeApi.Services
{
    public class StoryService
    {
        private readonly FoodRepository _foodRepository;
        private readonly FoodStepRepository _foodStepRepository;
        private readonly IngredientListRepository _ingredientListRepository;
        private readonly ViewRepository _viewRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public StoryService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _foodStepRepository = new FoodStepRepository(apiOption, databaseContext, mapper);
            _foodRepository = new FoodRepository(apiOption, databaseContext, mapper);
            _ingredientListRepository = new IngredientListRepository(apiOption, databaseContext, mapper);
            _viewRepository = new ViewRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object CreateStory(int userId, CreateStoryRequest request)
        {
            try
            {
                var ingredientList = JsonConvert.DeserializeObject<List<CreateIngredientInStoryRequest>>(request.Ingredients);
                var foodStepList = JsonConvert.DeserializeObject<List<CreateFoodStepInStoryRequest>>(request.FoodSteps);
                var newFood = new Food();
                newFood.Thumbnails = "";
                if (request.Image != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\foods\\thumbnails\\" + date + request.Image.FileName))
                    {
                        request.Image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    newFood.Thumbnails = "foods/thumbnails/" + date + request.Image.FileName;
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
                newFood.FoodTypeId = request.FoodTypeId;
                newFood.UserId = userId;
                newFood.Name = request.Name;
                newFood.Title = request.Title;
                newFood.PreparationTime = request.PreparationTime;
                newFood.CookingTime =  request.CookingTime;
                newFood.Meal = request.Meal;
                newFood.LevelOfDifficult = request.LevelOfDifficult;
                newFood.AccessRange = 1;
                _foodRepository.Create(newFood);

                _foodRepository.SaveChange();

                for (int i = 0; i < ingredientList.Count; i++)
                {
                    var newIngredients = new IngredientList();

                    newIngredients.FoodId = newFood.Id;
                    if (ingredientList[i] == null)
                    {
                        continue;
                    }
                    newIngredients.Name = ingredientList[i].Name;
                    newIngredients.Unit = ingredientList[i].Unit;
                    newIngredients.Quantity = ingredientList[i].Quantity;
                    _ingredientListRepository.Create(newIngredients);
                }

                _ingredientListRepository.SaveChange();

                for (int i = 0; i < foodStepList.Count; i++)
                {
                    var newFoodSteps = new FoodStep();

                    newFoodSteps.FoodId = newFood.Id;
                    if (foodStepList[i] == null)
                    {
                        continue;
                    }
                    newFoodSteps.Name = foodStepList[i].Name;
                    newFoodSteps.Content = foodStepList[i].Content;
                    newFoodSteps.NoStep = (i + 1);
                    _foodStepRepository.Create(newFoodSteps);
                }
                _foodStepRepository.SaveChange();
                return newFood;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object updateStory(int userId, UpdateStoryRequest request)
        {
            var reIngredientList = JsonConvert.DeserializeObject<List<CreateIngredientInStoryRequest>>(request.Ingredients);
            var reFoodStepList = JsonConvert.DeserializeObject<List<CreateFoodStepInStoryRequest>>(request.FoodSteps);
            var food = _foodRepository.FindByCondition(row => userId == row.UserId && request.Id == row.Id).FirstOrDefault();
            if (food == null)
            {
                throw new Exception("Food doesn't exist !");
            }
            if (request.Image != null && request.Image.FileName != food.Thumbnails)
            {
                var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\foods\\thumbnails\\" + date + request.Image.FileName))
                {
                    request.Image.CopyTo(fileStream);
                    fileStream.Flush();
                }
                food.Thumbnails = "foods/thumbnails/" + date + request.Image.FileName;
            }
            if (request.Video != null && request.Video.FileName != food.Video)
            {
                var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\foods\\videos\\" + date + request.Video.FileName))
                {
                    request.Video.CopyTo(fileStream);
                    fileStream.Flush();
                }
                food.Video = "foods/videos/" + date + request.Video.FileName;
            }
            food.Name = request.Name;
            food.Title = request.Title;
            food.FoodTypeId = request.FoodTypeId;
            food.PreparationTime = request.PreparationTime;
            food.CookingTime = request.CookingTime;
            food.Meal = request.Meal;
            food.LevelOfDifficult = request.LevelOfDifficult;
            food.UpdatedDate = DateTime.UtcNow;
            _foodRepository.UpdateByEntity(food);
            _foodRepository.SaveChange();
            var ingredientList = _ingredientListRepository.FindByCondition(row => request.Id == row.FoodId).ToList();
            for (int i = 0; i < ingredientList.Count; i++)
            {
                if (ingredientList[i] == null)
                {
                    continue;
                }
                _ingredientListRepository.DeleteByEntity(ingredientList[i]);
            }
            _ingredientListRepository.SaveChange();
            for (int i = 0; i < reIngredientList.Count; i++)
            {
                var newIngredients = new IngredientList();

                newIngredients.FoodId = food.Id;
                if (reIngredientList[i] == null)
                {
                    continue;
                }
                newIngredients.Name = reIngredientList[i].Name;
                newIngredients.Unit = reIngredientList[i].Unit;
                newIngredients.Quantity = reIngredientList[i].Quantity;
                newIngredients.UpdatedDate = DateTime.UtcNow;

                _ingredientListRepository.Create(newIngredients);
            }
            _ingredientListRepository.SaveChange();
            var foodStepList = _foodStepRepository.FindByCondition(row => request.Id == row.FoodId).ToList();
            for (int i = 0; i < foodStepList.Count; i++)
            {
                if (foodStepList[i] == null)
                {
                    continue;
                }
                _foodStepRepository.DeleteByEntity(foodStepList[i]);
            }
            for (int i = 0; i < reFoodStepList.Count; i++)
            {
                var newFoodSteps = new FoodStep();

                newFoodSteps.FoodId = food.Id;
                if (reFoodStepList[i] == null)
                {
                    continue;
                }
                newFoodSteps.Name = reFoodStepList[i].Name;
                newFoodSteps.Content = reFoodStepList[i].Content;
                newFoodSteps.NoStep = (i + 1);
                newFoodSteps.UpdatedDate = DateTime.UtcNow;
                _foodStepRepository.Create(newFoodSteps);
            }
            _foodStepRepository.SaveChange();
            return food;
        }

        public object deleteStory(int userId, int foodId)
        {
            var food = _foodRepository.FindByCondition(row => userId == row.UserId && foodId == row.Id).FirstOrDefault();
            _foodRepository.DeleteByEntity(food);
            _foodRepository.SaveChange();
            var ingredientList = _ingredientListRepository.FindByCondition(row => foodId == row.FoodId).ToList();
            for (int i = 0; i < ingredientList.Count; i++)
            {
                _ingredientListRepository.DeleteByEntity(ingredientList[i]);
            }
            _ingredientListRepository.SaveChange();

            var foodStepList = _foodStepRepository.FindByCondition(row => foodId == row.FoodId).ToList();
            for (int i = 0; i < foodStepList.Count; i++)
            {
                _foodStepRepository.DeleteByEntity(foodStepList[i]);
            }
            _foodStepRepository.SaveChange();
            return true;
        }

        public object addView(int userId, int foodId)
        {
            var checkView = _viewRepository.FindByCondition(row => userId == row.UserId && foodId == row.FoodId).FirstOrDefault();
            if (checkView != null)
            {
                throw new Exception("View existed !");
            }
            var view = new View();
            view.UserId = userId;
            view.FoodId = foodId;
            view.CreatedDate = _foodRepository.FindByCondition(row => foodId == row.Id).FirstOrDefault().CreatedDate;
            _viewRepository.Create(view);
            _viewRepository.SaveChange();
            return view;
        }

        public object UpdateView(int userId, int foodId)
        {
            try
            {
                var view = _viewRepository.FindByCondition(row => userId == row.UserId && foodId == row.FoodId).FirstOrDefault();
                if (view == null) 
                {
                    throw new Exception("View does not exist !");
                }
                view.CreatedDate = DateTime.Now;
                _viewRepository.UpdateByEntity(view);
                _viewRepository.SaveChange();
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetStoryHighestView()
        {
            try
            {
                var viewListInt = _viewRepository.GetHighestViews();
                List<Food> storyList = new List<Food>();
                for (int i = 0; i < viewListInt.Count; i ++)
                {
                    var story = _foodRepository.FindByCondition(row => viewListInt[i] == row.Id).FirstOrDefault();
                    storyList.Add(story);
                }
                return storyList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
