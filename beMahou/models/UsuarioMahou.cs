using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace beMahou.Models
{
    public class UsuarioMahou : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre de usuario")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Estrellas acumuladas")]
        public int EstrellasAcumuladas { get; set; } = 0;

        [Display(Name = "Avatar")]
        [StringLength(255)]
        public string? AvatarPath { get; set; }

        public List<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}