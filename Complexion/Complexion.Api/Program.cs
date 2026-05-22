using Complexion.Api.Repositories;
using Complexion.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISkinShadeRepository, SkinShadeRepository>(provider =>
    new SkinShadeRepository(connectionString));

builder.Services.AddScoped<ISkinUndertoneRepository, SkinUndertoneRepository>(provider =>
    new SkinUndertoneRepository(connectionString));

builder.Services.AddScoped<ICatalogueCategoryRepository, CatalogueCategoryRepository>(provider =>
    new CatalogueCategoryRepository(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddScoped<ISkinShadeService, SkinShadeService>();
builder.Services.AddScoped<ISkinUndertoneService, SkinUndertoneService>();

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
