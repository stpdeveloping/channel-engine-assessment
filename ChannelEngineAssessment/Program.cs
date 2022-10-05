using ChannelEngineAssessment.Shared;
using ChannelEngineAssessment.Shared.Interfaces;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
services.AddSingleton(new RestClient(RestClientParams.API_ROUTE));
services.AddScoped<IChannelEngineClient, ChannelEngineClient>(svcProvider => 
    new ChannelEngineClient(svcProvider.GetRequiredService<RestClient>(), 
        RestClientParams.API_KEY));
// Add services to the container.
services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
