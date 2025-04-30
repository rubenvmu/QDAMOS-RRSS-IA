using System.ComponentModel.DataAnnotations;

namespace beMahou.Models
{
    public class Comentario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El texto del comentario es obligatorio")]
        [Display(Name = "Comentario")]
        [StringLength(500, ErrorMessage = "El comentario no puede exceder 500 caracteres")]
        public string Texto { get; set; } = string.Empty;

        [Display(Name = "Fecha de creación")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Autor")]
        [StringLength(100, ErrorMessage = "El nombre de usuario no puede exceder 100 caracteres")]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        [Display(Name = "ID de Usuario")]
        [StringLength(450)]
        public string UsuarioId { get; set; } = string.Empty;

        [Display(Name = "Es útil")]
        public bool EsUtil { get; set; } = false;

        public int PublicacionId { get; set; }
        public Publicacion? Publicacion { get; set; }
    }
}