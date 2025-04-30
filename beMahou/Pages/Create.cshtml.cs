using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using beMahou.Data;
using beMahou.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace beMahou.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<UsuarioMahou> _userManager;

        public CreateModel(
            AppDbContext context, 
            IWebHostEnvironment environment,
            UserManager<UsuarioMahou> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            Publicacion = new Publicacion
            {
                UsuarioId = user.Id,
                Usuario = user.Nombre // Usamos el campo Nombre de UsuarioMahou
            };

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

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            Publicacion.UsuarioId = user.Id;
            Publicacion.Usuario = user.Nombre; // Usamos el campo Nombre de UsuarioMahou
            Publicacion.Fecha = DateTime.Now;

            if (Publicacion.ImagenFile != null && Publicacion.ImagenFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Publicacion.ImagenFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Publicacion.ImagenFile.CopyToAsync(fileStream);
                }

                Publicacion.FotoPath = "/uploads/" + uniqueFileName;
            }

            _context.Publicaciones.Add(Publicacion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}