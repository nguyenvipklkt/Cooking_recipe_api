using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using System.Threading.Tasks;

namespace CookingRecipeApi.Services
{
    public class FoodService
    {
        private readonly FoodRepository _foodRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public FoodService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _foodRepository = new FoodRepository(apiOption, databaseContext, mapper);
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

        public object GetFoodList(int userId)
        {
            try
            {
                return _foodRepository.FindByCondition(row => userId == row.UserId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    
}
