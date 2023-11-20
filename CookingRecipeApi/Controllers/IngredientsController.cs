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
    public class IngredientsController : BaseApiController<IngredientsController>
    {
        private readonly IngredientsService _ingredientsService;
        public IngredientsController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _ingredientsService = new IngredientsService(apiConfig, databaseContext, mapper, webHost);
        }

        
        [HttpPost]
        [Route("CreateIngredient")]
        public MessageData CreateIngredient(CreateIngredientsRequest request)
        {
            try
            {
                var res = _ingredientsService.CreateIngredient(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetIngredientList")]
        public MessageData GetIngredientList(int foodId)
        {
            try
            {
                var res = _ingredientsService.GetIngredientList(foodId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

    }


}
