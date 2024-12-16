
using JournyTask.AutoMapper;
using JournyTask.IRepository;
using JournyTask.Models;
using JournyTask.Repository;
using Microsoft.EntityFrameworkCore;

namespace JournyTask
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            //Add Connection String
            builder.Services.AddDbContext<FCarePlus3Context>
                (option => option.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
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
