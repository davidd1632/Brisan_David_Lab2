using Brisan_David_Lab2.Models;
using Microsoft.EntityFrameworkCore;

public class Brisan_David_Lab2Context : DbContext
{
    public Brisan_David_Lab2Context(DbContextOptions<Brisan_David_Lab2Context> options)
        : base(options)
    {
    }

    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<Category> Category { get; set; }
    public DbSet<BookCategory> BookCategory { get; set; }
    public DbSet<Publisher> Publishers { get; set; } = default!;
    public DbSet<Author> Author { get; set; } = default!;
    public DbSet<Publisher> Publisher { get; set; }

}
