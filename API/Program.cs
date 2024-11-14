using APP.Interfaces;
using APP.Services;
using APP.Validation;
using BankSystem.App.Interfaces;
using BankSystem.App.Services;
using DATA.Storages;
using Domain;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IStorage<User, SearchRequest>, UserStorage>();
builder.Services.AddScoped<IStorage<Building, SearchRequest>, BuildingStorage>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>();
    config.RegisterValidatorsFromAssemblyContaining<BuildingDtoValidator>();
    config.RegisterValidatorsFromAssemblyContaining<SensorDtoValidator>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();