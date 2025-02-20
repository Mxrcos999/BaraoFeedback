
using BaraoFeedback.Api.Extensions;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaraoFeedback.Api
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
            builder.Services.AddSwagger();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddDbContext<BaraoFeedbackContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("strConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddRoles<IdentityRole>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<BaraoFeedbackContext>()
               .AddUserManager<UserManager<ApplicationUser>>()
               .AddSignInManager<SignInManager<ApplicationUser>>()
               .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(builder.Configuration);
            builder.Services.AddAuthorizationPolicies();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
