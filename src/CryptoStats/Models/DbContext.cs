using Microsoft.EntityFrameworkCore;

namespace CryptoStats.Models
{
    public class CryptoContext: DbContext
    {
        public CryptoContext(DbContextOptions<CryptoContext> options) : base(options)
        {
        }

        public DbSet<Exchange> Exchanges{get;set;}
        public DbSet<Stat> Stats{get;set;}
    }
}