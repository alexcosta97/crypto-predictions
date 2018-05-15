using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using CryptoStats.GDAX;

namespace CryptoStats.Models
{
    public static class SeedData
    {
        private GDAXClient client = new GDAXClient();
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new CryptoContext(serviceProvider.GetRequiredService<DbContextOptions<CryptoContext>>()))
            {
                if(context.Exchanges.Any())
                {
                    return;
                }

                
            }
        }
    }
}