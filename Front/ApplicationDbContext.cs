using Front.Models;
using Microsoft.EntityFrameworkCore;

namespace Front;

public class ApplicationDBContext : DbContext
{
    public DbSet<ScrappedElement> ScrappedElements { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(@"Data Source=C:\sqlite\DB\LiteDB.db");
}
