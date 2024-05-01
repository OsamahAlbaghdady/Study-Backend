using BackEndStructuer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEndStructuer.DATA;


    public  class DbInitializer
    {
        public  void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                       serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Check if the database already exists
                context.Database.EnsureCreated();

                // If the database is empty, seed it with initial data
                if (!context.Settings.Any())
                {
                    // Add your seeding logic here
                    var initialData = new Setting()
                    {
                        WelcomeMessage = "Welcome to our website!",
                        WelcomeVideoUrl = "https://www.youtube.com/watch?v=123456",
                        ContactWhatsApp = "+1234567890",
                        ContactTelegram = "@telegram",
                    };
                    
                    // Add the initial data to the context
                    context.Settings.Add(initialData);
                    context.SaveChanges();
                }
            }
        }
    }
