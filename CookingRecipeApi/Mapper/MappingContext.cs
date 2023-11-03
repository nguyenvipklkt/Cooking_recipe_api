using AutoMapper;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;

namespace CookingRecipeApi.Mapper
{
    public class MappingContext : Profile
    {
        public MappingContext()
        {
            CreateMap<CreateTaskRequest, TaskItem>();
            CreateMap<FollowRequest, Follow>();
            CreateMap<CreateFoodTypeRequest, FoodType>();
            CreateMap<CreateFoodStepRequest, FoodStep>();
        }
    }
}
