var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitZone_GymsManagement>("fitzone-gymsmanagement");

builder.AddProject<Projects.FitZone_ScheduleService>("fitzone-scheduleservice");

builder.Build().Run();
