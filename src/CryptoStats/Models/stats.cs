using System;
namespace CryptoStats.Models
{
    public class Stats
    {
        public int Statsid {get;set;}
        public DateTime startDate{get;set;}
        public DateTime endDate{get;set;}

        public double HighestLatestDiff{get;set;}
        public double AvgDiff{get;set;}
        public long AvgGrowthTime {get;set;}
        public long AvgDeclineTime {get;set;}

        //Variables defining the forign key relationship
        public int ExchangeID{get;set;}
        public Exchange exchange{get;set;}
    }
}