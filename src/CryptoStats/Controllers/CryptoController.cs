using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using CryptoStats.Models;

namespace CryptoStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoContext _context;

        public CryptoController(CryptoContext context)
        {
            _context = context;

            //Populate DB with dummy data if none is present already
            if(_context.Exchanges.Count() == 0)
            {
                _context.Exchanges.Add(new Exchange {Name = "Exchange1"});
                _context.SaveChangesAsync();
            }
            if(_context.Stats.Count() == 0)
            {
                _context.Stats.Add(new Stat {startDate = DateTime.Now, endDate = DateTime.Now, HighestLatestDiff = 0.00, avgDiff = 0.00, avgGrowthTime = 0, avgDeclineTime = 0});
                _context.SaveChangesAsync();
            }
        }
    }
}