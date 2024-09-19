using EmailService.Helpers.EmailHelper;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Project2023.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer("Data Source=MSI\\SQL2019;Initial Catalog=Registations1;Integrated Security=True;Pooling=False"));

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

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
