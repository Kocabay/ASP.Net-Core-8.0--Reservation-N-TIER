using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TraversalCoreProje.CQRS.Handlers.DestinationHandlers;
using TraversalCoreProje.CQRS.Results.DestinationResults;
using TraversalCoreProje.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<Mediator>();

builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    x.SetMinimumLevel(LogLevel.Debug);
    x.AddDebug();
});

builder.Services.AddScoped<GetAllDestinationQueryHandler>();
builder.Services.AddScoped<GetAllDestinationQueryResult>();
builder.Services.AddScoped<GetAllDestinationQueryHandler>();
builder.Services.AddScoped<GetDestinationByIDQueryHandler>();
builder.Services.AddScoped<CreateDestinationCommandHandler>();
builder.Services.AddScoped<RemoveDestinationCommandHandler>();
builder.Services.AddScoped<UpdateDestinationCommandHandler>();
        
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>();
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.ContainerDependencies();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/SýgnIn";
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.CustomerValidator();
 

builder.Services.AddControllersWithViews().AddFluentValidation();


builder.Services.AddMvc();
ILoggerFactory loggerFactory = new LoggerFactory();
var app = builder.Build();
var path = Directory.GetCurrentDirectory();
loggerFactory.AddFile($"{path}\\Logs\\Log1.txt");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/", "?code={0}");   ////ERROR SAYFASI ÝÇÝN YAZILAN SERVÝS.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "areas",
//        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//        );
//});

app.Run();
