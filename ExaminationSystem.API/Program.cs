using ExaminationSystem.Core.Interfaces;
using ExaminationSystem.Data;
using ExaminationSystem.Data.Repositories;
using ExaminationSystem.Service;
using ExaminationSystem.Service.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        // Create a builder object
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ExaminationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddScoped<IExamService, ExamService>();
        builder.Services.AddControllers();

        // Add AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Create an app object
        var app = builder.Build();
        // Define the endpoints, map the controllers to the endpoints
        app.MapControllers();
        // Redirect to HTTPS
        app.UseHttpsRedirection();
        // Configure the Swagger UI
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examination System API V1");
            });
        }
        // Match the endpoint to the controller
        app.UseRouting();
        // Authenticate the user by checking the cookie and editing the HttpContext
        app.UseAuthentication();
        // Authorize the user by checking the role and the permission and the endpoint he wants to access
        app.UseAuthorization();
        
        app.Run();
    }
}

