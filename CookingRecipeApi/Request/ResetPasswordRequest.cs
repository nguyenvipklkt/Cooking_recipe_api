namespace CookingRecipeApi.Request
{
    public class ResetPasswordRequest
    {
        public string Username { get; set; }
        public string KeySecret { get; set; }
        public string NewPassword { get; set; }
    }
}
