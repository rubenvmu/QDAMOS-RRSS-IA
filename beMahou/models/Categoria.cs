using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

public enum Categoria
{
    [Display(Name = "Concursos")]
    Clasica,
    [Display(Name = "Festival")]
    Festival,
    [Display(Name = "Crear Publicación")]
    Rincon,
    [Display(Name = "Mapa")]
    Concurso,
    [Display(Name = "Quedadas")]
    Noche
}