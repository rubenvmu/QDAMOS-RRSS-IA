using System.ComponentModel.DataAnnotations;

public class Comentario
{
    public int Id { get; set; }
    
    [Required]
    public required string Usuario { get; set; }
    
    [Required]
    public required string Texto { get; set; }
    
    public DateTime Fecha { get; set; } = DateTime.Now;
    
    public int PublicacionId { get; set; }
    public Publicacion? Publicacion { get; set; }
}