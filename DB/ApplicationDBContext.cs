using Microsoft.EntityFrameworkCore;
using Scrapper_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB;
public class ApplicationDBContext : DbContext
{
    public DbSet<ScrappedElement> scrappedElements {  get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(@"Data Source=C:\sqlite\DB\LiteDB.db");
}
