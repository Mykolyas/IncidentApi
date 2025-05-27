using Microsoft.EntityFrameworkCore;

namespace IncidentApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Ініціалізація тестових даних (якщо таких даних не існує)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Створити базу, якщо ще не існує
                dbContext.Database.EnsureCreated();

                // Перевірити, чи вже є акаунт з таким ім’ям
                if (!dbContext.Accounts.Any(a => a.Name == "TestAccount"))
                {
                    var contact = new Contact
                    {
                        FirstName = "Test",
                        LastName = "User",
                        Email = "testuser@example.com"
                    };

                    var account = new Account
                    {
                        Name = "TestAccount",
                        Contacts = new List<Contact> { contact }
                    };

                    dbContext.Accounts.Add(account);
                    dbContext.SaveChanges();
                }
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
