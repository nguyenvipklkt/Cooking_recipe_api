
namespace CookingRecipeApi.Request
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public IFormFile? Image { get; set; }
        public int Position { get; set; } = 1;
    }
}
