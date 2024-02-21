global using dotNet_wepApi_entityFrameWork.Dtos;
global using dotNet_wepApi_entityFrameWork.Model;
using dotNet_wepApi_entityFrameWork.Data;
using dotNet_wepApi_entityFrameWork.Repository.EmployeeRepository;
using dotNet_wepApi_entityFrameWork.Repository.PositionRepository;
using dotNet_wepApi_entityFrameWork.Services.EmployeeService;
using dotNet_wepApi_entityFrameWork.Services.PositionService;
using Microsoft.EntityFrameworkCore;

;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<DataContext>();

// if (app.Environment.IsDevelopment())
// {
//     context.Database.EnsureDeleted();
// }
context.Database.EnsureCreated();
DbInitializer.Initialize(context);

app.UseHttpsRedirection();

// app.UseCors(x =>
//     x.AllowAnyMethod()
//         .AllowAnyHeader()
//         .AllowCredentials()
//         //.WithOrigins("https://localhost:3000))
//         .SetIsOriginAllowed(origin => true)
// );

app.UseAuthorization();

app.MapControllers();

app.Run();
