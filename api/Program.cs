using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Services
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<INeighbourhoodService, NeighbourhoodService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// Database connection
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Auth0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = builder.Configuration.GetValue<string>("Auth0:Domain");
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration.GetValue<string>("Auth0:Audience"),
            ValidIssuer = builder.Configuration.GetValue<string>("Auth0:Domain")
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
// Pay attention to the order of the middleware: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#middleware-order
app.UseHttpsRedirection();

app.UseCors(policy => policy.WithOrigins("https://localhost:7010")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
