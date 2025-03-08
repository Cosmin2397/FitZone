using FitZone.StatisticsService.gRPC;
using FitZone.StatisticsService.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStatisticsClient, StatisticsClient>();
builder.Services.AddGrpc();
builder.Services.AddGrpcClient<FitnessStats.FitnessStatsClient>(o =>
{
    o.Address = new Uri("https://localhost:7103"); // Adresa serverului gRPC
});
builder.Services.AddGrpcClient<FitnessStatsPeriod.FitnessStatsPeriodClient>(o =>
{
    o.Address = new Uri("https://localhost:7103"); // Adresa serverului gRPC
});
builder.Services.AddGrpcClient<StatisticsGrpc.StatisticsGrpcClient>(o =>
{
    o.Address = new Uri("https://localhost:7278"); // Adresa serverului gRPC
});
builder.Services.AddGrpcClient<TrainingsGrpc.TrainingsGrpcClient>(o =>
{
    o.Address = new Uri("https://localhost:7082"); // Adresa serverului gRPC
});
var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGrpcService<StatisticsClient>();
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
