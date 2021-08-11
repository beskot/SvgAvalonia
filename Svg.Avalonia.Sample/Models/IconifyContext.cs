using Microsoft.EntityFrameworkCore;

namespace Svg.Avalonia.Sample.Models
{
    public class IconifyContext : DbContext
    {
        public string Path { get; }
        public DbSet<Iconify> Iconifies { get; set; } = default!;

        public IconifyContext(string path)
        {
            Path = path;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@$"Data Source={Path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Iconify>(
                    eb =>
                    {
                        eb.HasNoKey();  
                    });
        }
    }
}