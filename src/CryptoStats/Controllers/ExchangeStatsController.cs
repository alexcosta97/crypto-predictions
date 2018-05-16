using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using CryptoStats.Models;

namespace CryptoStats.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ExchangeStatsController : ControllerBase
    {
        private readonly CryptoContext _context;

        public ExchangeStatsController(CryptoContext context)
        {
            _context = context;
        }

        [HttpGet("exchanges/{id}/stats")]
        public ActionResult<List<Stat>> GetAll(int id)
        {
            var exchange = _context.Exchanges.Where(e => e.ExchangeId == id).First();
            if(exchange == null)
            {
                return NotFound();
            }
            else
            {
                return exchange.Stats;
            }
        }

        [HttpGet("exchanges/{id}/stats/{statId}")]
        public ActionResult<Stat> GetByID(int id, int statId)
        {
            var exchange = _context.Exchanges.Where(e => e.ExchangeId == id).First();
            if(exchange == null)
            {
                return NotFound();
            }
            else
            {
                return exchange.Stats.Where(s => s.StatId == statId).First();
            }
        }

        [HttpGet("exchanges/{id}/stats/{startDate}")]
        public ActionResult<List<Stat>> GetByStartDate(int id, DateTime startDate)
        {
            var exchange = _context.Exchanges.Where(e => e.ExchangeId == id).First();
            if(exchange == null)
            {
                return NotFound();
            }
            else
            {
                return exchange.Stats.Where(s => s.startDate.Equals(startDate)).ToList();
            }
        }

        [HttpGet("exchanges/{id}/stats/{endDate}")]
        public ActionResult<List<Stat>> GetByEndDate(int id, DateTime endDate)
        {
            var exchange = _context.Exchanges.Where(e => e.ExchangeId== id).First();
            if(exchange == null)
            {
                return NotFound();
            }
            else
            {
                return exchange.Stats.Where(s => s.endDate.Equals(endDate)).ToList();
            }
        }

        [HttpGet("exchanges/{id}/stats/{startDate}/{endDate}")]
        public ActionResult<Stat> GetByDates(int id, DateTime startDate, DateTime endDate)
        {
            var exchange = _context.Exchanges.Where(e => e.ExchangeId == id).First();
            if(exchange == null)
            {
                return NotFound();
            }
            else
            {
                return exchange.Stats.Where(s => s.startDate.Equals(startDate) && s.endDate.Equals(endDate)).First();
            }
        }
    }
}