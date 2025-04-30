using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using beMahou.Data;
using beMahou.Models;
using Microsoft.AspNetCore.Identity;

namespace beMahou.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<UsuarioMahou> _userManager;

        public IndexModel(AppDbContext context, UserManager<UsuarioMahou> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<PublicacionConAvatar> Publicaciones { get; set; } = new List<PublicacionConAvatar>();

        public class PublicacionConAvatar : Publicacion
        {
            public string AvatarUsuario { get; set; } = "/images/default-avatar.png";
        }

        public async Task OnGetAsync()
        {
            var publicaciones = await _context.Publicaciones
                .Include(p => p.Comentarios)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            // Obtener avatares para cada publicación
            foreach (var pub in publicaciones)
            {
                var usuario = await _userManager.FindByNameAsync(pub.Usuario);
                Publicaciones.Add(new PublicacionConAvatar
                {
                    Id = pub.Id,
                    Usuario = pub.Usuario,
                    Bar = pub.Bar,
                    Fecha = pub.Fecha,
                    FotoPath = pub.FotoPath,
                    Estrellas = pub.Estrellas,
                    Evento = pub.Evento,
                    Experiencia = pub.Experiencia,
                    Comentarios = pub.Comentarios,
                    AvatarUsuario = usuario?.AvatarPath ?? "/images/default-avatar.png"
                });
            }
        }
    }
}