using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using beMahou.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

public class NuevaPublicacionModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public NuevaPublicacionModel(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [BindProperty]
    public required Publicacion Publicacion { get; set; }

    [BindProperty]
    public required IFormFile? Imagen { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
    {
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }
        return Page();
    }

    // Set all required fields
    Publicacion.Usuario = User.FindFirstValue(ClaimTypes.Name) ?? "Anónimo";
    Publicacion.UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    Publicacion.Fecha = DateTime.Now;
    Publicacion.Estrellas = 0; // Initialize stars

    // Handle image upload
    if (Imagen != null && Imagen.Length > 0)
    {
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await Imagen.CopyToAsync(fileStream);
        }

        Publicacion.FotoUrl = $"/uploads/{uniqueFileName}";
    }

    _context.Publicaciones.Add(Publicacion);
    await _context.SaveChangesAsync();

    return RedirectToPage("./Index");
}

    // Obtener todas las publicaciones
    public async Task<IActionResult> GetPublicaciones()
    {
        var publicaciones = await _context.Publicaciones
            .OrderByDescending(p => p.Fecha)
            .ToListAsync();
        return new JsonResult(publicaciones);
    }

    // Incrementar estrellas de una publicación
    public async Task<IActionResult> IncrementarEstrella(int id)
    {
        var publicacion = await _context.Publicaciones.FindAsync(id);
        if (publicacion == null)
        {
            return NotFound(new { message = "Publicación no encontrada" });
        }

        publicacion.Estrellas++;
        await _context.SaveChangesAsync();

        return new JsonResult(new { estrellas = publicacion.Estrellas });
    }

    // DTO para la creación de publicaciones
    public class PublicacionDto
    {
        [Required]
        public required string Bar { get; set; }

        [Required]
        public required string Experiencia { get; set; }

        [Required]
        public required string Categoria { get; set; }

        public IFormFile? Imagen { get; set; }
    }
}