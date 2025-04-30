using Microsoft.AspNetCore.Identity;
using System;       
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using beMahou.Data;
using System.Threading.Tasks;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Crear roles
        string[] roleNames = { "Admin", "User" };
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
            }
        }

        // Datos iniciales de ejemplo
        if (!context.Publicaciones.Any())
        {
            context.Publicaciones.AddRange(
                new Publicacion
                {
                    Bar = "Bar Mahou",
                    Experiencia = "La mejor Mahou del mundo!",
                    Categoria = Categoria.Clasica,
                    Estrellas = 5,
                    Usuario = adminUser.Id,
                    Fecha = DateTime.Now.AddDays(-2),
                    FotoUrl = "/images/mahou-sample.jpg"
                },
                new Publicacion
                {
                    Bar = "La Taberna de Pepe",
                    Experiencia = "Noche inolvidable con amigos y Mahou",
                    Categoria = Categoria.Noche,
                    Estrellas = 8,
                    Usuario = adminUser.Id,
                    Fecha = DateTime.Now.AddDays(-1),
                    FotoUrl = "/images/mahou-night.jpg"
                }
            );
            await context.SaveChangesAsync();
        }
    }
}