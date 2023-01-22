using Juan.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<JuanContext>(opt =>
{
	opt.UseSqlServer("Server=LAPTOP-TER04LK1\\SQLEXPRESS;Database=Juan;Trusted_Connection=true");
});
var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
		   name: "areas",
		   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		 );

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
