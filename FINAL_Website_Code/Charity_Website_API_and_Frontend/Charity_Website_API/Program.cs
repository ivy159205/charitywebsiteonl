using System.Text;
using Microsoft.EntityFrameworkCore;
using Charity_Website_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Charity_Website_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var secretKey = builder.Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Charity Website API",
                    Version = "v1"
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    // tự mình cấp mà không thông qua dịch vụ nào cả
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // ký token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                policy.RequireClaim("group_permission", "Admin"));

                options.AddPolicy("CustomerPolicy", policy =>
                policy.RequireClaim("group_permission", "Customer"));

                options.AddPolicy("VolunteerPolicy", policy =>
                policy.RequireClaim("group_permission", "Volunteer"));

                //options.AddPolicy("EditorPolicy", policy =>
                //policy.RequireAssertion(context =>
                //    context.User.HasClaim(c => c.Type == "group_permission" &&
                //                               (c.Value == "Editor" || c.Value == "Admin"))));

            });

            // Use connection string from code (you can move this to appsettings.json later)
            builder.Services.AddDbContext<DBCNhom1>(options =>
                options.UseSqlServer("Data Source=cmcsv.ric.vn,10000;Initial Catalog=N10_NHOM1;Persist Security Info=True;User ID=cmcsv;Password=cM!@#2025;encrypt=false")
            );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            var app = builder.Build();

            // Apply CORS policy
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            // Swagger will be available also on production hosting
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}