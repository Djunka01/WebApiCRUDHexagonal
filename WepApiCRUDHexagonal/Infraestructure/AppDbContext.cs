using Microsoft.EntityFrameworkCore;
using WepApiCRUDHexagonal.Domain.Entities;


namespace WepApiCRUDHexagonal.Infraestructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
