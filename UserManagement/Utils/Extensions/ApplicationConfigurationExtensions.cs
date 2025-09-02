using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserManagement.Persistance.DataContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserManagement.Utils.Extensions
{
    public static class ApplicationConfigurationExtensions
    {
        public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfiguration config)
        {
            //Swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "SDD,UserManagement.Backend.API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",

                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme
                    { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"
                        }
                    },
                    new string[] {}

                }
            });
            });
            services.AddDbContext<SddTestDbContext>(options =>
            {

                var connStr = config.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connStr, sqlServerOptionsAction: sOptions =>
                {
                    sOptions.EnableRetryOnFailure(
                         maxRetryCount: 5,
                         maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null
                        );
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
        };
    });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);

            services.AddHttpContextAccessor();

            services.AddScoped<ClaimsPrincipal>(provider =>
                provider.GetService<IHttpContextAccessor>().HttpContext?.User);
            return services;
        }
    }
    }

