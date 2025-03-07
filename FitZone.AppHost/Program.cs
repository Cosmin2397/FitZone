var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var api = builder.AddProject<Projects.FitZone_CalorieTrackerService>("calorieTrackerApi")
                 .WithReference(redis);

builder.AddProject<Projects.FitZone_GymsManagement>("fitzone-gymsmanagement");

builder.AddProject<Projects.FitZone_ScheduleService>("fitzone-scheduleservice");

builder.AddProject<Projects.FitZone_EmployeeManagementAPI>("fitzone-employeemanagementapi");

builder.AddProject<Projects.FitZone_SubscriptionService>("fitzone-subscriptionservice");

builder.AddProject<Projects.FitZone_SubscriptionValidationService>("fitzone-subscriptionvalidationservice");

builder.AddProject<Projects.FitZone_CalorieTrackerService>("fitzone-calorietrackerservice");

builder.AddProject<Projects.FitZone_StatisticsService>("fitzone-statisticsservice");

builder.Build().Run();
