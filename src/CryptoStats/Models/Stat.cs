using System;

namespace CryptoStats.Models
{
    public class Stat
    {
        public int StatId{get;set;}
        public DateTime startDate {get;set;}
        public DateTime endDate{get;set;}
        public double HighestLatestDiff{get;set;}
        public double avgDiff {get;set;}
        public long avgGrowthTime {get;set;}
        public long avgDeclineTime {get;set;}

        //Setting variables for foreign key
        public int ExchangeId{get;set;}
        public Exchange Exchange {get;set;}
    }
}