using Front.Models;
using Microsoft.EntityFrameworkCore;

namespace Front;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }
    public DbSet<ScrappedElement> scrappedElements { get; set; }
    public DbSet<User> Users { get; set; }
}
