using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using CryptoStats.Models;

namespace CryptoStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        private readonly CryptoContext _context;

        public ExchangesController(CryptoContext context)
        {
            _context = context;
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