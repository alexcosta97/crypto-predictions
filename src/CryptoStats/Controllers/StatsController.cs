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
        public ActionResult<List<Stat>> GetByStartDate([FromQuery] DateTime startDate)
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
        public ActionResult<List<Stat>> GetByEndDate([FromQuery] DateTime endDate)
        {
            var items = _context.Stats.Where(s => s.endDate == endDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            return items;
        }

        [HttpGet]
        public ActionResult<Stat> GetByDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
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
        public ActionResult<decimal> GetHighestLatestByID(int id)
        {
            var item = _context.Stats.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item.HighestLatestDiff;
        }

        [HttpGet("/highestlatest")]
        public ActionResult<decimal> GetHighestLatestByEndDate([FromQuery] DateTime endDate)
        {
            var item = _context.Stats.Where(s => s.endDate == endDate).Last();
            if(item == null)
            {
                return NotFound();
            }
            return item.HighestLatestDiff;
        }

        [HttpGet("/highestlatest")]
        public ActionResult<decimal> GetHighestLatestByDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
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
        public ActionResult<decimal> GetAvgDiffByID(int id)
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
        public ActionResult<decimal> GetAvgDiffByStartDate([FromQuery] DateTime startDate)
        {
            var items = _context.Stats.Where(s => s.startDate == startDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            decimal avgs = 0.00M;
            foreach(Stat s in items)
            {
                avgs += s.avgDiff;
            }
            decimal avgDiff = avgs / items.Count();
            return avgDiff;
        }

        //To get the avgDiff from endDate, the same procedure as for from startDate is applied: avg of avgs
        [HttpGet("/avgdiff")]
        public ActionResult<decimal> GetAvgDiffByEndDate([FromQuery] DateTime endDate)
        {
            var items = _context.Stats.Where(s => s.endDate == endDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            decimal avgs = 0.00M;
            foreach(Stat s in items)
            {
                avgs += s.avgDiff;
            }
            decimal avgDiff = avgs / items.Count();
            return avgDiff;
        }

        //The same procedure is applied for an id search is applied when looking for the avgDiff with a start and end dates
        [HttpGet("/avgdiff")]
        public ActionResult<decimal> GetAvgDiffByDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var item = _context.Stats.Where(s => s.startDate == startDate && s.endDate == endDate).First();
            if(item == null)
            {
                return NotFound();
            }
            return item.avgDiff;
        }

        //Get avgGrowth values from stats using different GET URL
        //Example api/stats/avggrowth/{id} or dates as parameters
        [HttpGet("/avggrowth/{id}", Name="GetAvgGrowthByID")]
        public ActionResult<TimeSpan> GetAvgGrowthByID(int id)
        {
            var item = _context.Stats.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item.avgGrowthTime;
        }

        //To get the avgGrowthTime for a start or end date, the same procedure as for the avgDiff is applied, meaning doing an avg of avgs
        [HttpGet("/avggrowth")]
        public ActionResult<TimeSpan> GetAvgGrowthByStartDate([FromQuery] DateTime startDate)
        {
            var items = _context.Stats.Where(s => s.startDate == startDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            double times = 0;
            foreach(Stat s in items)
            {
                times += s.avgGrowthTime.TotalMilliseconds;
            }
            return TimeSpan.FromMilliseconds(times / items.Count());
        }

        [HttpGet("/avggrowth")]
        public ActionResult<TimeSpan> GetAvgGrowthByEndDate([FromQuery] DateTime endDate)
        {
            var items = _context.Stats.Where(s => s.endDate == endDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            double times = 0;
            foreach(Stat s in items)
            {
                times += s.avgGrowthTime.TotalMilliseconds;
            }
            return TimeSpan.FromMilliseconds(times / items.Count());
        }

        [HttpGet("/avggrowth")]
        public ActionResult<TimeSpan> GetAvgGrowthByDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var item = _context.Stats.Where(s => s.startDate == startDate && s.endDate == endDate).First();
            if(item == null)
            {
                return NotFound();
            }
            return item.avgGrowthTime;
        }

        //To get the avgDecline values, same procedures apply as for avgGrowth
        //URLs are built the same way as for avgDecline, but with avgdecline instead of avggrowth
        [HttpGet("/avgdecline/{id}", Name="GetAvgDeclineByID")]
        public ActionResult<TimeSpan> GetAvgDeclineByID(int id)
        {
            var item = _context.Stats.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return item.avgDeclineTime;
        }
        
        [HttpGet("/avgdecline")]
        public ActionResult<TimeSpan> GetAvgDeclineByStartDate([FromQuery] DateTime startDate)
        {
            var items = _context.Stats.Where(s => s.startDate == startDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            double times = 0;
            foreach(Stat s in items)
            {
                times += s.avgDeclineTime.TotalMilliseconds;
            }
            return TimeSpan.FromMilliseconds(times / items.Count());
        }

        [HttpGet("/avgdecline")]
        public ActionResult<TimeSpan> GetAvgDeclineByEndDate([FromQuery] DateTime endDate)
        {
            var items = _context.Stats.Where(s => s.endDate == endDate).ToList();
            if(items == null)
            {
                return NotFound();
            }
            double times = 0;
            foreach(Stat s in items)
            {
                times += s.avgDeclineTime.TotalMilliseconds;
            }
            return TimeSpan.FromMilliseconds(times / items.Count());
        }

        [HttpGet("/avgdecline")]
        public ActionResult<TimeSpan> GetAvgDeclineByDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var item = _context.Stats.Where(s => s.startDate == startDate && s.endDate == endDate).First();
            if(item == null)
            {
                return NotFound();
            }
            return item.avgDeclineTime;
        }
    }
}