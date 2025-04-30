using beMahou.Data;
using beMahou.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, IWebHostEnvironment env)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();

        var userManager = services.GetRequiredService<UserManager<UsuarioMahou>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Crear roles
        string[] roleNames = { "Admin", "User", "PremiumUser" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Crear usuario admin
        var adminEmail = "admin@bemahou.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new UsuarioMahou
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                Nombre = "Administrador",
                EstrellasAcumuladas = 100,
                AvatarPath = "/images/default-avatar.png"
            };
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Crear usuario premium de ejemplo
        var premiumEmail = "premium@bemahou.com";
        var premiumUser = await userManager.FindByEmailAsync(premiumEmail);
        if (premiumUser == null)
        {
            premiumUser = new UsuarioMahou
            {
                UserName = premiumEmail,
                Email = premiumEmail,
                EmailConfirmed = true,
                Nombre = "Usuario Premium",
                EstrellasAcumuladas = 50,
                AvatarPath = "/images/premium-avatar.png"
            };
            var result = await userManager.CreateAsync(premiumUser, "Premium123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(premiumUser, "PremiumUser");
            }
        }

        // Crear usuario normal de ejemplo
        var userEmail = "user@bemahou.com";
        var normalUser = await userManager.FindByEmailAsync(userEmail);
        if (normalUser == null)
        {
            normalUser = new UsuarioMahou
            {
                UserName = userEmail,
                Email = userEmail,
                EmailConfirmed = true,
                Nombre = "Usuario Normal",
                EstrellasAcumuladas = 10,
                AvatarPath = "/images/user-avatar.png"
            };
            var result = await userManager.CreateAsync(normalUser, "User123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(normalUser, "User");
            }
        }

        // Datos iniciales de ejemplo
        if (!context.Publicaciones.Any())
        {
            var now = DateTime.Now;
            
            context.Publicaciones.AddRange(
                new Publicacion
                {
                    Usuario = "Administrador",
                    Bar = "Bar Mahou Original",
                    Experiencia = "La mejor Mahou Clásica que he probado en mi vida. Ambiente auténtico y tapas increíbles.",
                    Estrellas = 15,
                    Evento = TipoEvento.Bar,
                    Fecha = now.AddDays(-5),
                    FotoPath = "/images/sample-bares/bar-mahou.jpg",
                    UsuarioId = adminUser.Id
                },
                new Publicacion
                {
                    Usuario = "Usuario Premium",
                    Bar = "Festival de Cerveza Madrid",
                    Experiencia = "Increíble variedad de cervezas Mahou, incluyendo ediciones especiales. La Mahou Cinco Estrellas fue mi favorita.",
                    Estrellas = 12,
                    Evento = TipoEvento.Festival,
                    Fecha = now.AddDays(-3),
                    FotoPath = "/images/sample-bares/festival-madrid.jpg",
                    UsuarioId = premiumUser.Id
                }
            );

            await context.SaveChangesAsync();

            // Añadir algunos comentarios
            if (!context.Comentarios.Any())
            {
                var primeraPublicacion = context.Publicaciones.First();
                
                context.Comentarios.AddRange(
                    new Comentario
                    {
                        Texto = "¡Totalmente de acuerdo! El Bar Mahou es mi favorito también.",
                        Fecha = now.AddDays(-4),
                        Usuario = "Usuario Normal",
                        PublicacionId = primeraPublicacion.Id,
                        EsUtil = true,
                        UsuarioId = normalUser.Id
                    }
                );

                await context.SaveChangesAsync();
            }
        }

        // Crear directorios necesarios
        CreateDirectoryIfNotExists(Path.Combine(env.WebRootPath, "uploads"));
        CreateDirectoryIfNotExists(Path.Combine(env.WebRootPath, "images", "sample-bares"));
    }

    private static void CreateDirectoryIfNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}