using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using beMahou.Data;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Publicacion> Publicaciones { get; set; } = new();

    public async Task OnGetAsync()
    {
        Publicaciones = await _context.Publicaciones
            .OrderByDescending(p => p.Fecha)
            .Take(20)
            .ToListAsync();
    }
}