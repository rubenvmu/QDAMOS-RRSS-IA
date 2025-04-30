using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using beMahou.Data;


[Route("api/[controller]")]
[ApiController]
public class PublicacionesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PublicacionesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("{id}/estrella")]
    public async Task<IActionResult> AddEstrella(int id)
    {
        var publicacion = await _context.Publicaciones.FindAsync(id);
        if (publicacion == null) return NotFound();

        publicacion.Estrellas++;
        _context.SaveChanges();

        return Ok(new { estrellas = publicacion.Estrellas });
    }
}