namespace CookingRecipeApi.Models
{
    public class TaskItem : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public string Image { get; set; } = "";
        public int Position { get; set; } = 1;
        public int Status { get; set; } = 1;
    }
}
