using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class UsuarioMahou
{
    [Required]
    public required string Id { get; set; }
    
    [Required]
    public required string Nombre { get; set; }
    
    public int EstrellasAcumuladas { get; set; } = 0;
    
    public List<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
}