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
    public class DetailsModel : PageModel
    {
        private readonly beMahou.Data.AppDbContext _context;

        public DetailsModel(beMahou.Data.AppDbContext context)
        {
            _context = context;
        }

        public Publicacion Publicacion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones.FirstOrDefaultAsync(m => m.Id == id);

            if (publicacion is not null)
            {
                Publicacion = publicacion;

                return Page();
            }

            return NotFound();
        }
    }
}
