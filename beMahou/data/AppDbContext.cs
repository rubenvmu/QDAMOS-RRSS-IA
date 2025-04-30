using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using beMahou.Models;

namespace beMahou.Data
{
    public class AppDbContext : IdentityDbContext<UsuarioMahou>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Ignorar propiedad no mapeada
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
                    
                entity.Property(p => p.Estrellas)
                    .HasDefaultValue(0);
                    
                entity.Property(p => p.FotoPath)
                    .HasMaxLength(255);
                    
                entity.Property(p => p.Evento)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(50);
                    
                // Relación con UsuarioMahou
                entity.HasOne<UsuarioMahou>()
                    .WithMany(u => u.Publicaciones)
                    .HasForeignKey(p => p.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                // Relación con Comentarios
                entity.HasMany(p => p.Comentarios)
                    .WithOne(c => c.Publicacion)
                    .HasForeignKey(c => c.PublicacionId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                // Índices
                entity.HasIndex(p => p.Evento);
                entity.HasIndex(p => p.Fecha);
                entity.HasIndex(p => p.UsuarioId);
                entity.HasIndex(p => p.Estrellas);
            });

            // Configuración de Comentario
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(c => c.Id);
                
                entity.Property(c => c.Texto)
                    .IsRequired()
                    .HasMaxLength(500);
                    
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
                    
                // Índices
                entity.HasIndex(c => c.PublicacionId);
                entity.HasIndex(c => c.UsuarioId);
            });

            // Configuración de UsuarioMahou (heredado de IdentityUser)
            modelBuilder.Entity<UsuarioMahou>(entity =>
            {
                entity.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(u => u.AvatarPath)
                    .HasMaxLength(255);
                    
                entity.Property(u => u.EstrellasAcumuladas)
                    .HasDefaultValue(0);
            });
        }
    }
}