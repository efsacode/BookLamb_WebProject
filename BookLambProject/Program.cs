using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookLambProject.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookLambProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookLambProjectContext") ?? throw new InvalidOperationException("Connection string 'BookLambProjectContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
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
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
