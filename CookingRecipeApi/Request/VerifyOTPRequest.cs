namespace CookingRecipeApi.Request
{
    public class VerifyOTPRequest
    {
        public string Username { get; set; }
        public string Otp { get; set; }
    }
}
