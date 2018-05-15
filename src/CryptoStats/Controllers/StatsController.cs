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
        public ActionResult<List<Stat>> GetByStartDate([FromBody] DateTime startDate)
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

        [HttpGet]
        public ActionResult<Stat> GetByDates([FromBody] DateTime startDate, [FromBody] DateTime endDate)
        {
            var item = _context.Stats.Where(s => s.startDate == startDate && s.endDate == endDate).First();
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }

        //Find HighestLatestDiff by using a different Get URL
        //api/stats/highestlatest/id or dates as parameters
        [HttpGet("/highestlatest/{id}", Name="GetHighestLatestID")]
        public ActionResult<double> GetHighestLatestByID(int id)
        {
            var item = _context.Stats.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item.HighestLatestDiff;
        }

        [HttpGet("/highestlatest")]
        public ActionResult<double> GetHighestLatestByEndDate([FromBody] DateTime endDate)
        {
            var item = _context.Stats.Where(s => s.endDate == endDate).Last();
            if(item == null)
            {
                return NotFound();
            }
            return item.HighestLatestDiff;
        }

        [HttpGet("/highestlatest")]
        public ActionResult<double> GetHighestLatestByDates([FromBody] DateTime startDate, [FromBody] DateTime endDate)
        {
            var item = _context.Stats.Where(s => s.startDate == startDate && s.endDate == endDate).First();
            if(item == null)
            {
                return NotFound();
            }
            return item.HighestLatestDiff;
        }

        //Get avgDiff values from stats using different GET URL
        //Example api/stats/avgdiff/{id} or dates

        [HttpGet("/avgdiff/{id}", Name="GetAvgDiffByID")]
        public ActionResult<double> GetAvgDiffByID(int id)
        {
            var item = _context.Stats.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item.avgDiff;
        }

        //To get AvgDiff from startDate, all the avgDiff from the
        //start date onwards are put on a list and the overall avg is
        //calculated
        [HttpGet("/avgdiff")]
        public ActionResult<double> GetAvgDiffByStartDate([FromBody] DateTime startDate)
        {
            var items = _context.Stats.Where(s => s.startDate == startDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            double avgs = 0.00;
            foreach(Stat s in items)
            {
                avgs += s.avgDiff;
            }
            double avgDiff = avgs / items.Count();
            return avgDiff;
        }

        //To get the avgDiff from endDate, the same procedure as for from startDate is applied: avg of avgs
        [HttpGet("/avgdiff")]
        public ActionResult<double> GetAvgDiffByEndDate([FromBody] DateTime endDate)
        {
            var items = _context.Stats.Where(s => s.endDate == endDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            double avgs = 0.00;
            foreach(Stat s in items)
            {
                avgs += s.avgDiff;
            }
            double avgDiff = avgs / items.Count();
            return avgDiff;
        }
    }
}