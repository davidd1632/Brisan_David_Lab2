using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Brisan_David_Lab2.Models;

namespace Brisan_David_Lab2.Pages.BookCategoryPages
{
    public class CreateModel : PageModel
    {
        private readonly Brisan_David_Lab2Context _context;

        public CreateModel(Brisan_David_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Aici populam listele pentru dropdown-uri
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title");
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName");
            return Page();
        }

        [BindProperty]
        public BookCategory BookCategory { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.BookCategory == null || BookCategory == null)
            {
                // Daca sunt erori, reincarcam listele
                ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title");
                ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "CategoryName");
                return Page();
            }

            _context.BookCategory.Add(BookCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}