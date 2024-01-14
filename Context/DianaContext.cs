using DianaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DianaApp.Context
{
    public class DianaContext : DbContext
    {
        public DianaContext(DbContextOptions opt) : base(opt) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
