using CookingRecipeApi.Models;
using CookingRecipeApi.Seeder;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipeApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }

        public DbSet<TaskItem> task_items { get; set; }

        public DbSet<Admin> admins { get; set; }

        public DbSet<Food> foods { get; set; }
        public DbSet<FoodStep> food_steps { get; set; }
        public DbSet<Follow> follows { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<ReviewPoint> reviewPoints { get; set; }
        public DbSet<IngredientList> ingredientlists { get; set; }
        public DbSet<FoodType> foodTypes { get; set; }

        public static void UpdateDatabase(DatabaseContext context)
        {
            context.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var sqlConnection = "Server=localhost;Port=3306;Database=cooking_recipe;Uid=root;Pwd=;MaximumPoolSize=500;";
                var sqlConnection = "data source=DESKTOP-V87NI7H;initial catalog=cooking_recipe;user id=sa;password=1234$;MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(sqlConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            new UserSeeder(modelBuilder).SeedData();
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}