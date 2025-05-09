var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var rabbitmq = builder.AddRabbitMQ("messaging")
                      .WithDataVolume(isReadOnly: false);

var calorieTrackerApi = builder.AddProject<Projects.FitZone_CalorieTrackerService>("calorieTrackerApi")
                 .WithReference(redis);

builder.AddProject<Projects.FitZone_GymsManagement>("fitzone-gymsmanagement");

builder.AddProject<Projects.FitZone_ScheduleService>("fitzone-scheduleservice")
        .WaitFor(rabbitmq)
        .WithReference(rabbitmq);

builder.AddProject<Projects.FitZone_EmployeeManagementAPI>("fitzone-employeemanagementapi")
        .WaitFor(rabbitmq)
        .WithReference(rabbitmq); ;

builder.AddProject<Projects.FitZone_SubscriptionService>("fitzone-subscriptionservice")
    .WaitFor(rabbitmq)
    .WithReference(rabbitmq);

builder.AddProject<Projects.FitZone_SubscriptionValidationService>("fitzone-subscriptionvalidationservice");

builder.AddProject<Projects.FitZone_AuthentificationService>("fitzone-authservice")
    .WaitFor(rabbitmq)
    .WithReference(rabbitmq);

builder.AddProject<Projects.FitZone_StatisticsService>("fitzone-statistics");

builder.AddProject<Projects.FitZone_Ocelot>("fitzone-ocelot");

builder.AddProject<Projects.FitZone_Client_Web>("fitzone-client-web");

builder.Build().Run();
