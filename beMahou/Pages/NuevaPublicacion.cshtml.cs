using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using beMahou.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class NuevaPublicacionModel(AppDbContext context, IWebHostEnvironment env) : PageModel
{
    private readonly AppDbContext _context = context;
    private readonly IWebHostEnvironment _env = env;

    [BindProperty]
    public required Publicacion Publicacion { get; set; }

    [BindProperty]
    public required IFormFile Imagen { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Asignar usuario actual
        Publicacion.Usuario = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("User identifier not found.");
        Publicacion.Fecha = DateTime.Now;

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
}