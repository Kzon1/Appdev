using AppDev.Areas.StoreOwner.ViewModels;
using AppDev.Data;
using AppDev.Helpers;
using AppDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = Roles.StoreOwner)]
    public class BooksController : Controller
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

        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books
                .Include(b => b.Category)
                .Where(b => b.StoreId == StoreId);

            return View(await applicationDbContext.Include(x => x.Authors).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Image)
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == id && b.StoreId == StoreId);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["AuthorID"] = new MultiSelectList(_context.Authors.ToList(),"Id","Name");
            var model = new BookCreate()
            {
                StoreId = StoreId,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreate model)
        {
            if (ModelState.IsValid)
            {
                var extension = Path.GetExtension(model.UploadImage.FileName);
                var auth = new List<Author>();
                    foreach(int authorID in model.Authors)
                        {
                            var newAut = _context.Authors.FirstOrDefault(x => x.Id == authorID);
                            auth.Add(newAut);
                        }
                var book = new Book()
                {
                    Title = model.Title,
                    Price = model.Price,
                    StoreId = model.StoreId,
                    Authors = auth,
                    CategoryId = model.CategoryId,
                    Image = new(model.UploadImage.FileName, extension),
                };
                _context.Add(book);
                using (var stream = new FileStream(book.Image.Path, FileMode.CreateNew))
                {
                    await model.UploadImage.CopyToAsync(stream);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", model.CategoryId);
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Update(book);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
