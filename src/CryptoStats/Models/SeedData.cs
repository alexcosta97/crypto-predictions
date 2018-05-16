using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using CryptoStats.GDAX;
using CryptoStats.GDAX.Services.Products.Models;
using CryptoStats.GDAX.Services.Products.Types;
using CryptoStats.GDAX.Shared.Types;

namespace CryptoStats.Models
{
    public static class SeedData
    {
        private static GDAXClient client = new GDAXClient();
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new CryptoContext(serviceProvider.GetRequiredService<DbContextOptions<CryptoContext>>()))
            {
                if(context.Exchanges.Any() && context.Stats.Any())
                {
                    return;
                }
                
                if(!context.Exchanges.Any())
                {
                    List<Exchange> exchanges = new List<Exchange>();
                    var products = client.ProductsService.GetAllProductsAsync().Result;
                    foreach(Product prod in products)
                    {
                        exchanges.Add(new Exchange(){Name = prod.Id.ToString()});
                    }
                    context.Exchanges.AddRangeAsync(exchanges);
                }

                if(!context.Stats.Any())
                {
                    foreach(Exchange e in context.Exchanges)
                    {
                        seedStat(e.ExchangeId, DateTime.Now.AddDays(-7), DateTime.Now, context);
                    }
                    
                }
            }
        }

        public static void seedStat(int exchangeID, DateTime startDate, DateTime endDate, CryptoContext context)
        {
            using(context)
            {
                foreach(Product p in client.ProductsService.GetAllProductsAsync().Result)
                {
                    if(p.Id.ToString().Equals(context.Exchanges.Find(exchangeID).Name))
                    {
                        TimeSpan t = endDate.Subtract(startDate);
                        int days = (int)t.TotalDays;
                            for(int i = 0; i < days; i++)
                                {
                                    var candles = client.ProductsService.GetHistoricRatesAsync(p.Id, startDate, endDate, CandleGranularity.Minutes1).Result;
                                    Stat adder = new Stat(){
                                        startDate = candles.First().Time,
                                        endDate = candles.Last().Time  
                                    };

                                    //calculate highestLatestDiff
                                    decimal high = 0.00M;
                                    foreach(Candle c in candles)
                                    {
                                        if(c.High > high)
                                        {
                                            high = (decimal)c.High;
                                        }
                                    }
                                    adder.HighestLatestDiff = (decimal)candles.Last().Close - high;

                                    //calculate avgDiff
                                    //Use difference between open and close of candles to calculate the average diff
                                    decimal candlesDiff = 0.00M;
                                    foreach(Candle c in  candles)
                                    {
                                        candlesDiff += (decimal)(c.Close - c.Open);
                                    }
                                    adder.avgDiff = candlesDiff / candles.Count();
                                    
                                    //calculate average growth and decline times
                                    //store candles in a temporary list to retrieve their times when not in growth/decline anymore
                                    //growth and decline times are stored in a list of timespans
                                    //average is calculated afterwards
                                    List<TimeSpan> growthTimes = new List<TimeSpan>();
                                    List<TimeSpan> declineTimes = new List<TimeSpan>();
                                    bool inGrowth = false;
                                    List<Candle> growthCandles = new List<Candle>();

                                    foreach(Candle c in candles)
                                    {
                                        if(c.Close > c.Open && inGrowth == false)
                                        {
                                            inGrowth = true;
                                            declineTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                            growthCandles.Clear();
                                            growthCandles.Add(c);
                                        }
                                        else if(c.Close > c.Open && inGrowth == true)
                                        {
                                            growthCandles.Add(c);
                                        }
                                        else if(c.Close < c.Open && inGrowth == true)
                                        {
                                            inGrowth = false;
                                            growthTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                            growthCandles.Clear();
                                            growthCandles.Add(c);
                                        }
                                        else if(c.Close < c.Open && inGrowth == false)
                                        {
                                            growthCandles.Add(c);
                                        }
                                    }
                                    if(inGrowth)
                                    {
                                        growthTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                    }
                                    else
                                    {
                                        declineTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                    }

                                    double totalTimes = 0;
                                    foreach(TimeSpan time in growthTimes)
                                    {
                                        totalTimes += t.TotalMilliseconds;
                                    }
                                    adder.avgGrowthTime = TimeSpan.FromMilliseconds(totalTimes/ growthTimes.Count());
                                    
                                    totalTimes = 0;
                                    foreach(TimeSpan time in declineTimes)
                                    {
                                        totalTimes += t.TotalMilliseconds;
                                    }
                                    adder.avgDeclineTime = TimeSpan.FromMilliseconds(totalTimes / declineTimes.Count());
                                    context.Stats.Add(adder);
                                    context.SaveChangesAsync();
                                    startDate.AddDays(1);
                                }

                                startDate.AddDays(-days);
                                for(int i = 0; i < days; i++)
                                {
                                    var candles = client.ProductsService.GetHistoricRatesAsync(p.Id, startDate, endDate, CandleGranularity.Minutes1).Result;
                                    Stat adder = new Stat(){
                                        startDate = candles.First().Time,
                                        endDate = candles.Last().Time  
                                    };

                                    //calculate highestLatestDiff
                                    decimal high = 0.00M;
                                    foreach(Candle c in candles)
                                    {
                                        if(c.High > high)
                                        {
                                            high = (decimal)c.High;
                                        }
                                    }
                                    adder.HighestLatestDiff = (decimal)candles.Last().Close - high;

                                    //calculate avgDiff
                                    //Use difference between open and close of candles to calculate the average diff
                                    decimal candlesDiff = 0.00M;
                                    foreach(Candle c in  candles)
                                    {
                                        candlesDiff += (decimal)(c.Close - c.Open);
                                    }
                                    adder.avgDiff = candlesDiff / candles.Count();
                                    
                                    //calculate average growth and decline times
                                    //store candles in a temporary list to retrieve their times when not in growth/decline anymore
                                    //growth and decline times are stored in a list of timespans
                                    //average is calculated afterwards
                                    List<TimeSpan> growthTimes = new List<TimeSpan>();
                                    List<TimeSpan> declineTimes = new List<TimeSpan>();
                                    bool inGrowth = false;
                                    List<Candle> growthCandles = new List<Candle>();

                                    foreach(Candle c in candles)
                                    {
                                        if(c.Close > c.Open && inGrowth == false)
                                        {
                                            inGrowth = true;
                                            declineTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                            growthCandles.Clear();
                                            growthCandles.Add(c);
                                        }
                                        else if(c.Close > c.Open && inGrowth == true)
                                        {
                                            growthCandles.Add(c);
                                        }
                                        else if(c.Close < c.Open && inGrowth == true)
                                        {
                                            inGrowth = false;
                                            growthTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                            growthCandles.Clear();
                                            growthCandles.Add(c);
                                        }
                                        else if(c.Close < c.Open && inGrowth == false)
                                        {
                                            growthCandles.Add(c);
                                        }
                                    }
                                    if(inGrowth)
                                    {
                                        growthTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                    }
                                    else
                                    {
                                        declineTimes.Add(growthCandles.Last().Time.Subtract(growthCandles.First().Time));
                                    }

                                    double totalTimes = 0;
                                    foreach(TimeSpan time in growthTimes)
                                    {
                                        totalTimes += t.TotalMilliseconds;
                                    }
                                    adder.avgGrowthTime = TimeSpan.FromMilliseconds(totalTimes/ growthTimes.Count());
                                    
                                    totalTimes = 0;
                                    foreach(TimeSpan time in declineTimes)
                                    {
                                        totalTimes += t.TotalMilliseconds;
                                    }
                                    adder.avgDeclineTime = TimeSpan.FromMilliseconds(totalTimes / declineTimes.Count());
                                    context.Stats.Add(adder);
                                    context.SaveChangesAsync();
                                    endDate.AddDays(-1);
                                }
                    }
                }
            }
        }
    }
}