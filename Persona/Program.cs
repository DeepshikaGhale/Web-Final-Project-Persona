using Persona;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persona.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonaAuth>(options =>
    options.UseSqlite("Data Source=/Users/deepshikaghale/Data/myUserDB.db;"));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PersonaAuth>();

// Add services to the container.
//setup api
builder.Services.AddHttpClient(
    "PersonaAPIClient",
    (client) =>{
        client.BaseAddress = new Uri("https://localhost:7022/");
}
);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

