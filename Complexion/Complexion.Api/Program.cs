using Complexion.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISkinShadeRepository, SkinShadeRepository>(provider =>
    new SkinShadeRepository(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddScoped<ISkinUndertoneRepository, SkinUndertoneRepository>(provider =>
    new SkinUndertoneRepository(builder.Configuration.GetConnectionString("DefaultConnection")!));

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
