using Complexion.Services.Skin;
using Complexion.Migrations;
using Complexion.Models.Config;
using Complexion.Repository.Catalogue;
using Complexion.Repository.Config;
using Complexion.Repository.Skin;
using Complexion.Repository.Dbo;
using Complexion.Services.Catalogue;
using Complexion.Services.Config;
using Complexion.Services.Dbo;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Add services to the container.
builder.Services.AddControllers().AddApplicationPart(typeof(Complexion.Controllers.Skin.SkinShadeController).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MigrationRunner.Run(connectionString);

builder.Services.AddScoped<ISkinShadeRepository, SkinShadeRepository>(provider => new SkinShadeRepository(connectionString));
builder.Services.AddScoped<ISkinUndertoneRepository, SkinUndertoneRepository>(provider => new SkinUndertoneRepository(connectionString));
builder.Services.AddScoped<ICatalogueCategoryRepository, CatalogueCategoryRepository>(provider => new CatalogueCategoryRepository(connectionString));
builder.Services.AddScoped<IConfigPriceTierRepository, ConfigPriceTierRepository>(provider => new ConfigPriceTierRepository(connectionString));
builder.Services.AddScoped<ISkinProfileRepository, SkinProfileRepository>(provider => new SkinProfileRepository(connectionString));
builder.Services.AddScoped<IProductRepository, ProductRepository>(provider => new ProductRepository(connectionString));
builder.Services.AddScoped<IProductRecommendationRepository, ProductRecommendationRepository>(provider => new ProductRecommendationRepository(connectionString));

builder.Services.AddScoped<ISkinShadeService, SkinShadeService>();
builder.Services.AddScoped<ISkinUndertoneService, SkinUndertoneService>();
builder.Services.AddScoped<ICatalogueCategoryService, CatalogueCategoryService>();
builder.Services.AddScoped<IConfigPriceTierService, ConfigPriceTierService>();
builder.Services.AddScoped<ISkinProfileService, SkinProfileService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRecommendationService, ProductRecommendationService>();

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
