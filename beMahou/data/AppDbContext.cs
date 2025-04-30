using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using beMahou.Data;

namespace beMahou.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<UsuarioMahou> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Necesario para Identity

            // Configuración de Publicacion
            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Bar).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Experiencia).IsRequired().HasMaxLength(500);
                entity.Property(p => p.Fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(p => p.Estrellas).HasDefaultValue(0);
                
                // Relación con Comentarios
                entity.HasMany(p => p.Comentarios)
                    .WithOne()
                    .HasForeignKey(c => c.PublicacionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Comentario
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Texto).IsRequired().HasMaxLength(300);
                entity.Property(c => c.Fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Configuración de UsuarioMahou
            modelBuilder.Entity<UsuarioMahou>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(u => u.EstrellasAcumuladas).HasDefaultValue(0);
            });
        }
    }
}