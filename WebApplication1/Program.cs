using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interface;
using WebApplication1.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMainPoseRepository, MainPoseRepository>();
//เพิ่ม database 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();
//ตอนรันแอพก็จะเรียงใช้ program ก็จะทำให้ Seed.SeedData ทำงาน ทำให้ data update to Database
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Seed.SeedData(app);
    //Seed.SeedData(app);
}
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
