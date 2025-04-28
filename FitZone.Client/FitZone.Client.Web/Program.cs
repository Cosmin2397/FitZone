using FitZone.Client.Web.Components;
using FitZone.Client.Web.Services;
using FitZone.Client.Shared.Services.Interfaces;
using FitZone.Client.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the FitZone.Client.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSharedServices(builder.Configuration);
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(FitZone.Client.Shared._Imports).Assembly);

app.Run();
