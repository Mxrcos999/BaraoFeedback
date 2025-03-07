
using BaraoFeedback.Api.Extensions;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Application.Services.Email;
using BaraoFeedback.Application.Services.Institution;
using BaraoFeedback.Application.Services.Location;
using BaraoFeedback.Application.Services.Ticket;
using BaraoFeedback.Application.Services.TicketCategory;
using BaraoFeedback.Application.Services.User;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Infra.Repositories;
using BaraoFeedback.Infra.Repositories.Location;
using BaraoFeedback.Infra.Repositories.Ticket;
using BaraoFeedback.Infra.Repositories.TicketCategory;
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
            builder.Services.AddScoped<ITicketCategoryService, TicketCategoryService>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IInstitutionService, InstitutionService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization(); 
            
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
