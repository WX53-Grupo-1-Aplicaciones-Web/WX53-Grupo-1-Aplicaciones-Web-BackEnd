using _1.API.Mapper;
using _2.Domain;
using _2.Domain.ArtisanDomain;
using _2.Domain.OrderDomain;
using _2.Domain.ProductDomain;
using _3.Data;
using _3.Data.Context;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerData, CustomerMySqlData>();
builder.Services.AddScoped<ICustomerDomain, CustomerDomain>();
builder.Services.AddScoped<IProductData, ProductMySqlData>(); 
builder.Services.AddScoped<IProductDomain, ProductDomain>(); 
builder.Services.AddScoped<IArtisanData, ArtisanMySqlData>(); 
builder.Services.AddScoped<IArtisanDomain, ArtisanDomain>();
builder.Services.AddScoped<IOrderData, OrderMySqlData>();
builder.Services.AddScoped<IOrderDomain, OrderDomain>();
builder.Services.AddAutoMapper(typeof(RequestToModel), typeof(ModelToRequest),typeof(ModelToResponse));

var connectionString = builder.Configuration.GetConnectionString("ArtisaniaDB");

builder.Services.AddDbContext<ArtisaniaDBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<ArtisaniaDBContext>())
{
    context.Database.EnsureCreated();
}


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