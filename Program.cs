
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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<FCarePlus3Context>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins"); // Apply the CORS policy

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}
