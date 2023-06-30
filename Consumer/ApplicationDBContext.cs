using Consumer.Models;
using Microsoft.EntityFrameworkCore;

namespace Consumer;
public class ApplicationDBContext : DbContext
{
    public DbSet<ScrappedElement> scrappedElements {  get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(@"Data Source=C:\sqlite\DB\LiteDB.db");
}
