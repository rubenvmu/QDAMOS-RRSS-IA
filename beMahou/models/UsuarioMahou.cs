using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace beMahou.Models
{
    public class UsuarioMahou
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre de usuario")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Estrellas acumuladas")]
        public int EstrellasAcumuladas { get; set; } = 0;

        [Display(Name = "Avatar")]
        [StringLength(255)]
        public string? AvatarPath { get; set; }

        [Required]
        [StringLength(450)]
        public string UsuarioId { get; set; } = string.Empty;

        public List<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}