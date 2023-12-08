using CookingRecipeApi.Models;

namespace CookingRecipeApi.Dto
{
    public class StoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodTypeId { get; set; }
        public int FoodPlaceId { get; set; }
        public int SeasonalFoodId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Thumbnails { get; set; } = "";
        public int ViewNumber { get; set; } = 0;
        public int LikeNumber { get; set; } = 0;
        public int ShareNumber { get; set; } = 0;

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }

        public int Meal { get; set; }

        public int LevelOfDifficult { get; set; }

        public string Video { get; set; } = "";
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorAvatar { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
