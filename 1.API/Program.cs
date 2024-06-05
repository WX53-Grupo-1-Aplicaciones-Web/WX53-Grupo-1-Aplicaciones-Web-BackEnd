using System.Reflection;
using _1.API.Mapper;
using _2.Domain;
using _2.Domain.ArtisanDomain;
using _2.Domain.OrderDomain;
using _2.Domain.ProductDomain;
using _3.Data;
using _3.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Artisania API",
        Description = "An ASP.NET Core Web API for managing Artisania's customers,artisans and products.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Artisania Landing page startup",
            Url = new Uri("https://wx53-grupo-1-aplicaciones-web.github.io/WX53-Grupo-1-Aplicaciones-Web-LandingPage/")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
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