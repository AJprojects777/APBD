using CW10.Models;
using CW10.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ➕ Dodaj DbContext z connection stringiem
builder.Services.AddDbContext<MasterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ➕ Dodaj serwisy
builder.Services.AddScoped<ITripService, TripService>();

// ➕ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// ➕ Użyj Swaggera tylko w development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();