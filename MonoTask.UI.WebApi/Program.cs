using Microsoft.EntityFrameworkCore;
using MonoTask.Application.Services.Vehicle;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Infrastructure.Data.DbContexts;
using MonoTask.Infrastructure.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the interface if needed
builder.Services.AddScoped<IDataContext, DataContext>();

//builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IVehicleBrandService, VehicleBrandService>();

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
