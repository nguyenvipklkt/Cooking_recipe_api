namespace CookingRecipeApi.Models
{
    public class Admin : BaseModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Avatar { get; set; } = "";
        public string Address { get; set; }
        public string Role { get; set; }
        public string OTP { get; set; } = "";

    }
}   
