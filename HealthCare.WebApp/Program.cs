using HealthCare.Core;
using Microsoft.EntityFrameworkCore;
using HealthCare.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using HealthCare.WebApp.Areas.Identity;
using HealthCare.Data;
using HealthCare.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMvc();
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<UserPageService>();
builder.Services.AddScoped<DoctorPageService>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<Account>>();

builder.Services.AddDbContext<HealthContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddIdentity<Account,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HealthContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Doctor", "Patient" };

    foreach(var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();
    var userStore = scope.ServiceProvider.GetRequiredService<IUserStore<Account>>();
    //Load seeded doctors
    var context = scope.ServiceProvider.GetRequiredService<HealthContext>();
    if (!context.Staff.Any())
    {
        var creator = new SeedData(userManager,userStore);
        await creator.SeedDataToDB(context);
    }
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

