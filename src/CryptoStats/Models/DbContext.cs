using Microsoft.EntityFrameworkCore;

namespace CryptoStats.Models
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions<CryptoContext> options) : base(options)
        {            
        }

        //Add DbSets to DbContext to store and retrieve tables from/to the Database
        public DbSet<Exchange> Exchanges {get;set;}
        public DbSet<Stat> Stats {get;set;}
    }
}