using Complexion.Api.Middleware;
using Complexion.Migrations;
using Complexion.Repository.Catalogue;
using Complexion.Repository.Config;
using Complexion.Repository.Skin;
using Serilog;
using FluentValidation;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using Complexion.Services.Skin;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MigrationRunner.Run(connectionString);

builder.Services.AddSingleton(connectionString);

builder.Services.AddScoped<ISkinShadeRepository, SkinShadeRepository>();
builder.Services.AddScoped<ISkinUndertoneRepository, SkinUndertoneRepository>();
builder.Services.AddScoped<ICatalogueCategoryRepository, CatalogueCategoryRepository>();
builder.Services.AddScoped<IConfigPriceTierRepository, ConfigPriceTierRepository>();
builder.Services.AddScoped<ISkinProfileRepository, SkinProfileRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductRecommendationRepository, ProductRecommendationRepository>();

builder.Services.AddScoped<ISkinProfileService, SkinProfileService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRecommendationService, ProductRecommendationService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
