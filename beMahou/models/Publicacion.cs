using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using beMahou.Attributes; // Añade este using
using System.ComponentModel.DataAnnotations.Schema;

namespace beMahou.Models
{
    public class Publicacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [Display(Name = "Usuario")]
        [StringLength(100)]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del establecimiento es obligatorio")]
        [Display(Name = "Establecimiento")]
        [StringLength(100)]
        public string Bar { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cuéntanos tu experiencia")]
        [Display(Name = "Experiencia")]
        [StringLength(1000)]
        public string Experiencia { get; set; } = string.Empty;

        [Display(Name = "Fecha de publicación")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Display(Name = "Estrellas")]
        [DefaultValue(0)]
        public int Estrellas { get; set; } = 0;

        [Display(Name = "Ruta de la imagen")]
        [StringLength(255)]
        public string? FotoPath { get; set; }

        [Display(Name = "Tipo de evento")]
        [Required(ErrorMessage = "Selecciona un tipo de evento")]
        public TipoEvento Evento { get; set; }

        [Display(Name = "ID de Usuario")]
        [StringLength(450)]
        public string? UsuarioId { get; set; }

        [Display(Name = "Comentarios")]
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();

        [NotMapped]
        [Display(Name = "Subir imagen")]
        [DataType(DataType.Upload)]
        [MaxFileSize(20 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile? ImagenFile { get; set; }

        public void CalcularEstrellas()
        {
            int puntosBase = 10;
            int puntosComentario = 2;
            int puntosFoto = 5;
            int puntosPopularidad = Comentarios.Count(c => c.EsUtil) * 3;

            Estrellas = puntosBase + 
                      (Comentarios.Count * puntosComentario) + 
                      (string.IsNullOrEmpty(FotoPath) ? 0 : puntosFoto) + 
                      puntosPopularidad;
        }
    }

    public enum TipoEvento
    {
        [Display(Name = "Bar/Tapería")]
        Bar,
        [Display(Name = "Discoteca/Pub")]
        Discoteca,
        [Display(Name = "Festival de Cerveza")]
        Festival,
        [Display(Name = "Concierto")]
        Concierto,
        [Display(Name = "Evento Deportivo")]
        Deportivo,
        [Display(Name = "Terrazas")]
        Terraza,
        [Display(Name = "Feria Local")]
        Feria,
        [Display(Name = "Otro")]
        Otro
    }
}