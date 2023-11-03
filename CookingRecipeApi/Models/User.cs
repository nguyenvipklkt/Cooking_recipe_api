using NetTopologySuite.Operation.OverlayNG;

namespace CookingRecipeApi.Models
{
    public class User : BaseModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Title { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string Avatar { get; set; } = "";
        public string Cover { get; set; } = "";
        public string Address { get; set; } = "";
        public string OTP { get; set; } = "";
        public int Status { get; set; } = 1;
    }
}
