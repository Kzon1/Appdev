using AppDev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Controllers;

public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Books
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Books
            .Include(b => b.Category)
            .Include(b => b.Image);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Books == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .Include(b => b.Category)
            .Include(b => b.Store)
            .Include(b => b.Image)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }
}
