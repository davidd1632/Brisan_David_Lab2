using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Brisan_David_Lab2.Models;

namespace Brisan_David_Lab2.Pages.BookCategoryPages;

public class IndexModel : PageModel
{
    private readonly Brisan_David_Lab2Context _context;

    public IndexModel(Brisan_David_Lab2Context context)
    {
        _context = context;
    }

    public IList<BookCategory> BookCategory { get; set; } = default!;

    public async Task OnGetAsync()
    {
        BookCategory = await _context.BookCategory.ToListAsync();
    }
}
