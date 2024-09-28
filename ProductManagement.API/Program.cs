using Microsoft.EntityFrameworkCore;
using ProductManagement.Repository;
using ProductManagement.Repository.Models;
using ProductManagement.Service.Interfaces;
using ProductManagement.Service.Mapper;
using ProductManagement.Service.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.KebabCaseLower;
                    option.JsonSerializerOptions.WriteIndented = true;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FstoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FstoreDB"), options => options.EnableRetryOnFailure());
});

builder.Services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);

// add DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

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
