using FitZone.SubscriptionService.Features.Subscription.GetSubscriptionById;
using FitZone.SubscriptionService.Features.Subscription.GetSubscriptions;
using FitZone.SubscriptionService.gRPC;
using FitZone.SubscriptionService.RabbitMQ;
using FitZone.SubscriptionService.Shared.Abstractions;
using FitZone.SubscriptionService.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddGrpc();
builder.AddRabbitMQClient(connectionName: "messaging");
builder.Services.AddHostedService<UserDeletedConsumer>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
var app = builder.Build();

app.MapGrpcService<SubscriptionGrpcService>();
app.MapGrpcService<StatisticsService>();
app.MapDefaultEndpoints();

app.RegisterAllEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
