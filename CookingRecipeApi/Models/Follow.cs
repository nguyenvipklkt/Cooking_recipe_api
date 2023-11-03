using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingRecipeApi.Models
{
    public class Follow : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowingUserId { get; set; }
    }
}
