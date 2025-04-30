using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using beMahou.Data;
using beMahou.Models;

namespace beMahou.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();

        public async Task OnGetAsync()
        {
            Publicaciones = await _context.Publicaciones
                .Include(p => p.Comentarios) // Incluir comentarios si los necesitas
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }
    }
}