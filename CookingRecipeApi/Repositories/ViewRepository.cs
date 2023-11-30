using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;
using CookingRecipeApi.Respositories;
using CookingRecipeApi.Utility;

namespace CookingRecipeApi.Repositories
{
    public class ViewRepository : BaseRespository<View>
    {
        private IMapper _mapper;
        public ViewRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }

        public List<int> GetHighestViews()
        {
            try
            {
                var view = DbContext.views.GroupBy(x => x.FoodId).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToList();
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
