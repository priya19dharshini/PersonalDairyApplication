
using Microsoft.EntityFrameworkCore;
using PersonalDiary.Data;
using PersonalDiary.Repositories;
using PersonalDiary.Repositories.Interfaces;
using PersonalDiary.Services;
using PersonalDiary.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Controllers
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 2. Register OpenAPI
builder.Services.AddOpenApi();

// 3. Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Register Repository
builder.Services.AddScoped<
    IDiaryEntryRepository,
    DiaryEntryRepository>();

// 5. Register Service
builder.Services.AddScoped<
    IDiaryEntryService,
    DiaryEntryService>();

// 6. Register Authorization
builder.Services.AddAuthorization();

// 7. Build application
var app = builder.Build();

// 8. Configure middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
}

app.UseHttpsRedirection();

app.UseCors("AngularFrontend");

app.UseAuthorization();

// 9. Map controllers
app.MapControllers();

// 10. Run application
app.Run();
