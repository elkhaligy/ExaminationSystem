using ExaminationSystem.Core.Interfaces;
using ExaminationSystem.Data;
using ExaminationSystem.Data.Repositories;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        builder.Services.AddDbContext<ExaminationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        
        app.Run();
    }
}

