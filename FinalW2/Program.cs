using FLoanAPI.Data;
using FLoanAPI.Data.Services;
using LoanAPI.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FinalW2.Middleware;

namespace FinalW2
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {

                
                 options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<ILoggerService, LoggerService>();
            builder.Services.AddTransient<ILoggerService, LoggerService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ILoanService, LoanService>();

            var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, 
                        IssuerSigningKey = new SymmetricSecurityKey(key),

                        ValidateIssuer = true, 
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

                        ValidateAudience = true, 
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],

                        ValidateLifetime = true, 
                        ClockSkew = TimeSpan.Zero 
                    };
                });

            

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
          
            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
