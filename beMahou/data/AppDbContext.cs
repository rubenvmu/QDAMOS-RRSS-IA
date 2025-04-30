using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using beMahou.Models;

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
            base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Publicacion>().Ignore(p => p.ImagenFile);

            // Configuración de Publicacion
            modelBuilder.Entity<Publicacion>(entity =>
            {
                entity.HasKey(p => p.Id);
                
                entity.Property(p => p.Usuario)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(p => p.Bar)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(p => p.Experiencia)
                    .IsRequired()
                    .HasMaxLength(1000);
                    
                entity.Property(p => p.Fecha)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                    
                // Cambiado a valor por defecto 0 y comentario actualizado
                entity.Property(p => p.Estrellas)
                    .HasDefaultValue(0)
                    .HasComment("Puntos acumulables (antes llamado estrellas)");
                    
                entity.Property(p => p.FotoPath)
                    .HasMaxLength(255);
                    
                entity.Property(p => p.Evento)
                    .IsRequired()
                    .HasConversion<string>() // Guarda el enum como string
                    .HasMaxLength(50);
                    
                entity.Property(p => p.UsuarioId)
                    .HasMaxLength(450); // Tamaño para IdentityUser Id

                // Relación con Comentarios
                entity.HasMany(p => p.Comentarios)
                    .WithOne()
                    .HasForeignKey(c => c.PublicacionId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                // Índices para mejor performance
                entity.HasIndex(p => p.Evento);
                entity.HasIndex(p => p.Fecha);
                entity.HasIndex(p => p.UsuarioId);
                entity.HasIndex(p => p.Estrellas); // Nuevo índice para búsquedas por puntos
            });

            // Configuración de Comentario
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(c => c.Id);
                
                entity.Property(c => c.Texto)
                    .IsRequired()
                    .HasMaxLength(500); // Aumentado de 300 a 500
                    
                entity.Property(c => c.Fecha)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                    
                entity.Property(c => c.Usuario)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(c => c.UsuarioId)
                    .IsRequired()
                    .HasMaxLength(450);
                    
                entity.Property(c => c.EsUtil)
                    .HasDefaultValue(false);
                    
                // Índices para mejor performance
                entity.HasIndex(c => c.PublicacionId);
                entity.HasIndex(c => c.UsuarioId);
            });

            // Configuración de UsuarioMahou
            modelBuilder.Entity<UsuarioMahou>(entity =>
            {
                entity.HasKey(u => u.Id);
                
                entity.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(u => u.AvatarPath)
                    .HasMaxLength(255);
                    
                entity.Property(u => u.EstrellasAcumuladas)
                    .HasDefaultValue(0)
                    .HasComment("Puntos totales acumulados por el usuario");
                    
                entity.Property(u => u.UsuarioId)
                    .IsRequired()
                    .HasMaxLength(450);
                    
                // Relación con IdentityUser
                entity.HasOne<IdentityUser>()
                    .WithOne()
                    .HasForeignKey<UsuarioMahou>(u => u.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                // Índices para mejor performance
                entity.HasIndex(u => u.UsuarioId)
                    .IsUnique();
            });
        }
    }
}