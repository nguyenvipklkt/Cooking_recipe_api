using CookingRecipeApi.Models;

namespace CookingRecipeApi.Dto
{
    public class UserLoginDto
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
