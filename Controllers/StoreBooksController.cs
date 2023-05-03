using AppDev.Data;
using AppDev.Helpers;
using AppDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Controllers
{
    [Authorize(Roles = Roles.StoreOwner)]
    public class StoreBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        private string? _storeId;

        private string StoreId
        {
            get
            {
                _storeId = _storeId ?? userManager.GetUserId(User);
                return _storeId;
            }
        }

        public StoreBooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books
                .Include(b => b.Category)
                .Where(b => b.StoreId == StoreId);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id && b.StoreId == StoreId);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }



       
}
