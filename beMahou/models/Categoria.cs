using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

public enum Categoria
{
    [Display(Name = "Experiencia Clásica")]
    Clasica,
    [Display(Name = "Festival Mahou")]
    Festival,
    [Display(Name = "Rincón Especial")]
    Rincon,
    [Display(Name = "Concurso")]
    Concurso,
    [Display(Name = "Noche Mahou")]
    Noche
}