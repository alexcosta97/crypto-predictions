using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using CryptoStats.Models;

namespace CryptoStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly CryptoContext _context;

        public ExchangeController(CryptoContext context)
        {
            _context = context;

            //Populate DB with dummy data if none is present already
            if(_context.Exchanges.Count() == 0)
            {
                _context.Exchanges.Add(new Exchange {Name = "Exchange1"});
                _context.SaveChangesAsync();
            }
        }

        [HttpGet]
        public ActionResult<List<Exchange>> GetAll()
        {
            return _context.Exchanges.ToList();
        }

        [HttpGet("{id}", Name="GetExchange")]
        public ActionResult<Exchange> GetById(int id)
        {
            var item = _context.Exchanges.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}