using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Brisan_David_Lab2.Models;

namespace Brisan_David_Lab2.Pages.Authors;

public class IndexModel : PageModel
{
    private readonly Brisan_David_Lab2Context _context;

    public IndexModel(Brisan_David_Lab2Context context)
    {
        _context = context;
    }
    public IList<Book> Book { get; set; } 
    public BookData BookD { get; set; }
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();
        BookD.Books = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .OrderBy(b => b.Title)
            .ToListAsync();
        if (id is not null)
        {
            BookID = id.Value;
            Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();
            BookD.Categories = book.BookCategories.Select(s => s.Category);
            
        }
    }

    public IList<Author> Author { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Author = await _context.Author.ToListAsync();
    }
}
