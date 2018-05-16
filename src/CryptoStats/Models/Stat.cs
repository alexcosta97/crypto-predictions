using System;

namespace CryptoStats.Models
{
    public class Stat
    {
        public int StatId{get;set;}
        public DateTime startDate {get;set;}
        public DateTime endDate{get;set;}
        public decimal HighestLatestDiff{get;set;}
        public decimal avgDiff {get;set;}
        public TimeSpan avgGrowthTime {get;set;}
        public TimeSpan avgDeclineTime {get;set;}

        //Setting variables for foreign key
        public int ExchangeId{get;set;}
        public Exchange Exchange {get;set;}
    }
}