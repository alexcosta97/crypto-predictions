using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using CryptoStats.Models;

namespace CryptoStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StatsController: ControllerBase
    {
        private readonly CryptoContext _context;

        public StatsController(CryptoContext context)
        {
            _context = context;

            //Populate DB with dummy data if none is present already
            if(_context.Stats.Count() == 0)
            {
                _context.Stats.Add(new Stat {startDate = DateTime.Now, endDate = DateTime.Now, HighestLatestDiff = 0.00, avgDiff = 0.00, avgGrowthTime = 0, avgDeclineTime = 0});
                _context.SaveChangesAsync();
            }
        }

        [HttpGet]
        public ActionResult<List<Stat>> GetAll()
        {
            return _context.Stats.ToList();
        }

        [HttpGet("{id}", Name="GetStat")]
        public ActionResult<Stat> GetById(int id)
        {
            var item = _context.Stats.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }

        //Action to get all the stats with a specific start date
        [HttpGet]
        public ActionResult<List<Stat>> GetAll([FromBody] DateTime startDate)
        {
            var items = _context.Stats.Where(s => s.startDate == startDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            return items;
        }

        //Action to get all the stats with a specific end date
        [HttpGet]
        public ActionResult<List<Stat>> GetByEndDate([FromBody] DateTime endDate)
        {
            var items = _context.Stats.Where(s => s.endDate == endDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            return items;
        }
    }
}