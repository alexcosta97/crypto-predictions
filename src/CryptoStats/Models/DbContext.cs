using Microsoft.EntityFrameworkCore;

namespace CryptoStats.Models
{
    public class CryptoStatsContext : DbContext
    {
        public CryptoStatsContext(DbContextOptions<CryptoStatsContext> options) : base(options)
        {}

        public DbSet<Exchange> Exchanges{get;set;}
        public DbSet<Stats> Stats{get;set;}
    }
}