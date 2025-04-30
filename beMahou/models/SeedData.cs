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

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
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
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                
                // Crear perfil de usuario asociado
                context.Usuarios.Add(new UsuarioMahou
                {
                    Id = Guid.NewGuid().ToString(),
                    Nombre = "Administrador",
                    UsuarioId = adminUser.Id,
                    EstrellasAcumuladas = 100,
                    AvatarPath = "/images/default-avatar.png"
                });
                await context.SaveChangesAsync();
            }
        }

        // Crear usuario premium de ejemplo
        var premiumEmail = "premium@bemahou.com";
        var premiumUser = await userManager.FindByEmailAsync(premiumEmail);
        if (premiumUser == null)
        {
            premiumUser = new IdentityUser
            {
                UserName = premiumEmail,
                Email = premiumEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(premiumUser, "Premium123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(premiumUser, "PremiumUser");
                
                context.Usuarios.Add(new UsuarioMahou
                {
                    Id = Guid.NewGuid().ToString(),
                    Nombre = "Usuario Premium",
                    UsuarioId = premiumUser.Id,
                    EstrellasAcumuladas = 50,
                    AvatarPath = "/images/premium-avatar.png"
                });
                await context.SaveChangesAsync();
            }
        }

        // Crear usuario normal de ejemplo
        var userEmail = "user@bemahou.com";
        var normalUser = await userManager.FindByEmailAsync(userEmail);
        if (normalUser == null)
        {
            normalUser = new IdentityUser
            {
                UserName = userEmail,
                Email = userEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(normalUser, "User123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(normalUser, "User");
                
                context.Usuarios.Add(new UsuarioMahou
                {
                    Id = Guid.NewGuid().ToString(),
                    Nombre = "Usuario Normal",
                    UsuarioId = normalUser.Id,
                    EstrellasAcumuladas = 10,
                    AvatarPath = "/images/user-avatar.png"
                });
                await context.SaveChangesAsync();
            }
        }

        // Datos iniciales de ejemplo
        if (!context.Publicaciones.Any())
        {
            var now = DateTime.Now;
            
            context.Publicaciones.AddRange(
                new Publicacion
                {
                    Usuario = "admin@bemahou.com",
                    Bar = "Bar Mahou Original",
                    Experiencia = "La mejor Mahou Clásica que he probado en mi vida. Ambiente auténtico y tapas increíbles.",
                    Estrellas = 15, // Puntos calculados
                    Evento = TipoEvento.Bar,
                    Fecha = now.AddDays(-5),
                    FotoPath = "/images/sample-bares/bar-mahou.jpg",
                    UsuarioId = adminUser.Id
                },
                new Publicacion
                {
                    Usuario = "premium@bemahou.com",
                    Bar = "Festival de Cerveza Madrid",
                    Experiencia = "Increíble variedad de cervezas Mahou, incluyendo ediciones especiales. La Mahou Cinco Estrellas fue mi favorita.",
                    Estrellas = 12, // Puntos calculados
                    Evento = TipoEvento.Festival,
                    Fecha = now.AddDays(-3),
                    FotoPath = "/images/sample-bares/festival-madrid.jpg",
                    UsuarioId = premiumUser?.Id
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
                        Usuario = "user@bemahou.com",
                        PublicacionId = primeraPublicacion.Id,
                        EsUtil = true
                    }
                );

                await context.SaveChangesAsync();
            }
        }

        // Actualizar puntos basados en interacciones
        foreach (var publicacion in context.Publicaciones)
        {
            publicacion.CalcularEstrellas();
        }
        await context.SaveChangesAsync();

        // Actualizar estrellas acumuladas de usuarios
        foreach (var usuario in context.Usuarios)
        {
            usuario.EstrellasAcumuladas = context.Publicaciones
                .Where(p => p.UsuarioId == usuario.UsuarioId)
                .Sum(p => p.Estrellas);
        }
        await context.SaveChangesAsync();

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