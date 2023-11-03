using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Respositories;

namespace CookingRecipeApi.Repositories
{
    public class TaskItemRepository : BaseRespository<TaskItem>
    {
        private IMapper _mapper;
        public TaskItemRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
