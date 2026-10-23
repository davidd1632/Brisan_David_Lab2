using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Brisan_David_Lab2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisan_David_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Brisan_David_Lab2Context _context;

        public EditModel(Brisan_David_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Aducem cartea cu toate relatiile ei (Editura, Autor, Categorii)
            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            // Populam checkbox-urile pentru categorii
            PopulateAssignedCategoryData(_context, Book);

            // Populam listele derulante (dropdown) pentru Editura si Autor
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

            // ATENTIE: Daca in clasa Author ai alta proprietate pentru nume (ex: "Name" sau "FullName"), schimb-o mai jos!
            ViewData["AuthorID"] = new SelectList(_context.Author.Select(a => new { a.ID, NumeComplet = a.LastName + " " + a.FirstName }), "ID", "NumeComplet");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(i => i.Publisher)
                .Include(i => i.BookCategories).ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            // Actualizam proprietatile de baza
            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title,
                i => i.AuthorID, // <-- Actualizam si ID-ul autorului
                i => i.Price,
                i => i.PublishingDate,
                i => i.PublisherID))
            {
                // Actualizam categoriile
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Daca validarea a esuat, reincarcam datele pentru afisare
            PopulateAssignedCategoryData(_context, bookToUpdate);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Author.Select(a => new { a.ID, NumeComplet = a.LastName + " " + a.FirstName }), "ID", "NumeComplet");

            return Page();
        }
    }
}