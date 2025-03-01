using FitZone.CalorieTrackerService.Configurations;
using FitZone.CalorieTrackerService.Repositories;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using FitZone.CalorieTrackerService.Services;
using FitZone.CalorieTrackerService.Services.Interfaces;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.Configure<NutritionixSettings>(builder.Configuration.GetSection("NutritionixSettings"));

//  Configurare MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
// Configurarea pentru Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"))
);


builder.Services.AddScoped<INutritionixRepository, NutritionixRepository>();
builder.Services.AddScoped<IMealRepository, MealRepository>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<INutritionixService, NutritionixService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddSingleton<MongoDbInitializer>();
//  Configurare Redis
builder.Services.AddSingleton<CacheService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var mongoInitializer = scope.ServiceProvider.GetRequiredService<MongoDbInitializer>();
    mongoInitializer.Initialize();
}
app.MapDefaultEndpoints();

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
