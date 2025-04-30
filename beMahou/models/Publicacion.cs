using System.ComponentModel.DataAnnotations;

public class Publicacion
{
    public int Id { get; set; }
    
    [Required]
    public required string Usuario { get; set; }
    
    [Required]
    public required string Bar { get; set; }
    
    [Required]
    public required string Experiencia { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.Now;
    public int Estrellas { get; set; } = 0;
    public string? FotoUrl { get; set; }
    public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    public Categoria Categoria { get; set; }
    public string? UsuarioId { get; set; } // Make this nullable or set it in OnPost
}