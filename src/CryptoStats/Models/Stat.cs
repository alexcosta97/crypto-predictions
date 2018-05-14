using System;
using System.Collections.Generic;

namespace CryptoStats.Models
{
    public class Stat
    {
        public int StatId{get;set;}
        public DateTime startDate{get;set;}
        public DateTime endDate {get;set;}
        public double highestLatestDiff{get;set;}
        public double avgDiff {get;set;}
        public long avgGrowthTime{get;set;}
        public long avgDeclineTime{get;set;}

        //Variables to set foreign key
        public int ExchangeId{get;set;}
        public Exchange Exchange {get;set;}
    }
}