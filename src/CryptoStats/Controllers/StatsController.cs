using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using CryptoStats.Models;

namespace CryptoStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly CryptoContext _context;

        public StatsController(CryptoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Stat>> GetAll()
        {
            return _context.Stats.ToList();
        }

        [HttpGet("/id/{id}")]
        public ActionResult<Stat> GetById(int id)
        {
            var item = _context.Stats.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("/startDate/{startDate}")]
        public ActionResult<List<Stat>> GetByStartDate(DateTime startDate)
        {
            var items = _context.Stats.Where(s => s.startDate.Equals(startDate)).ToList();
            if(items == null)
            {
                return NotFound();
            }
            return items;
        }

        [HttpGet("/endDate/{endDate}")]
        public ActionResult<List<Stat>> GetByEndDate(DateTime endDate)
        {
            var items = _context.Stats.Where(s => s.endDate.Equals(endDate)).ToList();
            if(items == null)
            {
                return NotFound();
            }
            return items;
        }
    }
}