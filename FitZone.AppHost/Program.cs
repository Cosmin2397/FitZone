var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitZone_GymsManagement>("fitzone-gymsmanagement");

builder.AddProject<Projects.FitZone_ScheduleService>("fitzone-scheduleservice");

builder.AddProject<Projects.FitZone_EmployeeManagementAPI>("fitzone-employeemanagementapi");

builder.AddProject<Projects.FitZone_SubscriptionService>("fitzone-subscriptionservice");

builder.AddProject<Projects.FitZone_CalorieTrackerService>("fitzone-calorietrackerservice");

builder.Build().Run();
