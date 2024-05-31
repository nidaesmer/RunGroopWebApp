using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using RunGroopWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>(); //dashboard �c�n 
builder.Services.AddScoped<IUserRepository, UserRepository>(); //user �c�n 
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings")); //cloudinary i�in servis kodu
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>()  //identityi uygulamaya ekler
    .AddEntityFrameworkStores<ApplicationDbContext>(); //entity frm kullan�larak vb baglam�nda saklancag� bel�rt�l�r
builder.Services.AddMemoryCache(); //bellek i�i �nbellekleme hizmeti ekler, verilere h�zl� er�s�m saglar
builder.Services.AddSession();     // oturum h�zmet� ekler
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie();  //kimlik dogrulama �emas�n� bel�rt�r
var app = builder.Build();

if(args.Length == 1 && args[0].ToLower() =="seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app); //uygulaman�n baslang�cta kullan�c�lar� ve roller� tohumlamas� saglan�r
    // Seed.SeedData(app);
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
