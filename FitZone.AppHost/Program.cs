var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitZone_GymsManagement>("fitzone-gymsmanagement");

builder.Build().Run();
