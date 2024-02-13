using System.Text;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.UseCases;
using CleanArchitecture.Domain.Interfaces.Configuration;
using CleanArchitecture.Domain.Interfaces.Data;
using CleanArchitecture.Domain.Interfaces.Mail;
using CleanArchitecture.Domain.Interfaces.Security;
using CleanArchitecture.Domain.Interfaces.Server;
using CleanArchitecture.Infrastructure.Configuration;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Mail;
using CleanArchitecture.Infrastructure.Security;
using CleanArchitecture.Infrastructure.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Cross-Origin Resource Sharing (CORS) for the application.
// This setup allows any origin (domain), any header, and any HTTP method to access resources.
// Note: This is a very permissive configuration and should be carefully reviewed, especially for production environments.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture API", Version = "v1" });

    c.MapType<String>(() => new OpenApiSchema { Type = "string", Nullable = true });
    c.MapType<int>(() => new OpenApiSchema { Type = "integer", Nullable = true });

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        }
    );
});

// Add JWT Auth
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();

// Add dependencies

// Configuration
builder.Services.AddScoped<IConnectionStrings, ConnectionStrings>();
builder.Services.AddScoped<ICryptoConfiguration, CryptoConfiguration>();
builder.Services.AddScoped<IEmailConfiguration, EmailConfiguration>();
builder.Services.AddScoped<IJWTConfiguration, JWTConfiguration>();
builder.Services.AddScoped<ITestingUsersConfiguration, TestingUsersConfiguration>();

// Security
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<ICryptoUtils, CryptoUtils>();

// Mail
builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IMailTemplateRepository, MailTemplateRepository>();

//Server
builder.Services.AddScoped<IImageStorageService, ImageStorageLocalServerAdapter>();

// Data: Repos
builder.Services.AddScoped(typeof(IRepository<,>), typeof(GenericRepositoryDapperAdapter<,>));

// Data: Queries
builder.Services.AddScoped<IAccountCustomQueries, AccountCustomQueriesDapperAdapter>();
builder.Services.AddScoped<
    IAuthorizationTokenCustomQueries,
    AuthorizationTokenCustomQueriesDapperAdapter
>();

// Use Cases
builder.Services.AddScoped<IAuthUseCases, AuthUseCases>();
builder.Services.AddScoped<IAccountUseCases, AccountUseCases>();

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
