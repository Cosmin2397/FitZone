using FitZone.SubscriptionValidationService.Data;
using FitZone.SubscriptionValidationService.Protos;
using FitZone.SubscriptionValidationService.Repositories;
using FitZone.SubscriptionValidationService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpcClient<SubscriptionGrpc.SubscriptionGrpcClient>(o =>
{
    o.Address = new Uri("https://localhost:7278");
});
builder.Services.AddGrpc();
builder.Services.AddScoped<ISubscriptionValidationService, SubscriptionValidationService>();
builder.Services.AddScoped<IValidationsRepository, ValidationsRepository>();
builder.Services.AddScoped<IValidationsService, ValidationsService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGrpcService<SubscriptionValidationService>();
app.MapGrpcService<ValidationsService>();
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
