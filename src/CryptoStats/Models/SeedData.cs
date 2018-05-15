using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CryptoStats.Models
{
    public static class SeedData
    {
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