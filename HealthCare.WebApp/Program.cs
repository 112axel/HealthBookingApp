using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HealthCare.Core;
using HealthCare.WebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HealthCare.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<BookingService>();


builder.Services.AddDbContext<HealthContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));

    //Allan
    //options.UseSqlServer(@"Server=MSI\SQLEXPRESS;Database=Jobscout;Integrated Security=true;TrustServerCertificate=true;");
});

builder.Services.AddDefaultIdentity<Account>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HealthContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;


    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

