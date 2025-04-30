using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using beMahou.Data;
using beMahou.Models;
using Microsoft.AspNetCore.Hosting;

namespace beMahou.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publicacion Publicacion { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Procesar la imagen solo si se subió un archivo
            if (Publicacion.ImagenFile != null && Publicacion.ImagenFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                
                // Crear directorio si no existe
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generar nombre único para el archivo
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Publicacion.ImagenFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Guardar el archivo
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Publicacion.ImagenFile.CopyToAsync(fileStream);
                }

                // Guardar la ruta en la base de datos
                Publicacion.FotoPath = "/uploads/" + uniqueFileName;
            }

            // Establecer fecha actual
            Publicacion.Fecha = DateTime.Now;

            _context.Publicaciones.Add(Publicacion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}