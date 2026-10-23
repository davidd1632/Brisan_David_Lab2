using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Brisan_David_Lab2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisan_David_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Brisan_David_Lab2Context _context;

        public IndexModel(Brisan_David_Lab2Context context)
        {
            _context = context;
        }

        public BookData BookD { get; set; } = default!;
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookD = new BookData();

           
            BookD.Books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author) 
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            BookD.Categories = await _context.Category.ToListAsync();
            BookD.BookCategories = await _context.BookCategory.ToListAsync();

            BookID = id ?? 0;
            CategoryID = categoryID ?? 0;
        }
    }
}