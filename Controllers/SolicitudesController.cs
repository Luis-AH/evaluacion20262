using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using evaluacion20262.Data;
using evaluacion20262.Models;

namespace evaluacion20262.Controllers
{
    public class SolicitudesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Solicitudes
        public async Task<IActionResult> Index()
        {
            var solicitudes = await _context.Solicitudes
                .OrderByDescending(s => s.FechaRegistro)
                .ToListAsync();
            return View(solicitudes);
        }

        // GET: Solicitudes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Solicitudes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Cliente,Telefono,Distrito,TipoServicio,Descripcion")] SolicitudServicio solicitud)
        {
            if (ModelState.IsValid)
            {
                solicitud.FechaRegistro = DateTime.Now;
                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "¡Solicitud registrada correctamente!";
                return RedirectToAction(nameof(Index));
            }
            return View(solicitud);
        }
    }
}
