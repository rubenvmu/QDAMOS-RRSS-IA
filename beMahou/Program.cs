using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using beMahou.Data;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Builder;
using beMahou.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddRazorPages();

// Configuración de la base de datos SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Identity con UsuarioMahou
builder.Services.AddIdentity<UsuarioMahou, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Configuración de cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Crear directorios necesarios
var avatarsPath = Path.Combine(app.Environment.WebRootPath, "avatars");
if (!Directory.Exists(avatarsPath))
{
    Directory.CreateDirectory(avatarsPath);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(avatarsPath),
    RequestPath = "/avatars"
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();