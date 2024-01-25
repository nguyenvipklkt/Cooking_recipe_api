using CookingRecipeApi.Models;

namespace CookingRecipeApi.Dto
{
    public class NotifyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReactingUserId { get; set; }
        public int FoodId { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public string ReactingUserFirstName { get; set; }
        public string ReactingUserLastName { get; set; }
        public string ReactingUserAvatar { get; set; }
        public string FoodName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
