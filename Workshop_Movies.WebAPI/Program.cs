using DataAccess.Implementations;
using DataAccess.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using Services.Helpers;

namespace Workshop_Movies.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //get conn string from appSettings.json
            string connectionString = builder.Configuration.GetConnectionString("ConnectionString");
            //string connString = "Server=NB-5TH1P53\\MSSQLSERVER01;Database=MoviesWorkshopDb;Trusted_Connection=True";
            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.RegisterDbContext(connectionString);
            builder.Services.RegisterRepository();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
