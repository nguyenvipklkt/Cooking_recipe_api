using Microsoft.EntityFrameworkCore;
using CookingRecipeApi.Models;
using CookingRecipeApi.Utility;

namespace CookingRecipeApi.Seeder
{
    class UserSeeder
    {
        private readonly ModelBuilder _modelBuilder;
        public UserSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        /// <summary>
        /// Excute data
        /// </summary>
        public void SeedData()
        {
            _modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Password = UtilityFunction.CreateMD5("Test"),
                    Email = "Test@gmail.com",
                    FirstName = "Test",
                    LastName = "Test",
                    Address = "Ha Noi",
                    Birthday = new DateTime(2001, 05, 14),
                    Avatar = "",
                    Status = 1,
                }
                );
        }
    }
}
