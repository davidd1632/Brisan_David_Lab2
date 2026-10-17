using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Brisan_David_Lab2.Models;

namespace Brisan_David_Lab2.Pages.Publishers;

public class DeleteModel : PageModel
{
    private readonly Brisan_David_Lab2Context _context;

    public DeleteModel(Brisan_David_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Publisher Publisher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publisher = await _context.Publishers.FirstOrDefaultAsync(m => m.ID == id);
        if (publisher is null)
        {
            return NotFound();
        }
        else
        {
            Publisher = publisher;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher != null)
        {
            Publisher = publisher;
            _context.Publishers.Remove(Publisher);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
