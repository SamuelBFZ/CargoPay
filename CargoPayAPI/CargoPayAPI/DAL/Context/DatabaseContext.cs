using CargoPayAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoPayAPI.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
