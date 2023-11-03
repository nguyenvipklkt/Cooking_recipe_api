namespace CookingRecipeApi.Request
{
    public class UpdateProfileRequest
    {
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public IFormFile? Avatar { get; set; }
        public IFormFile? Cover { get; set; }
        public string Address { get; set; }
    }
}
