using System;
using System.Collections.Generic;
namespace CryptoStats.Models
{
    public class Exchange
    {
        public int ExchangeId {get;set;}
        public string name {get;set;}

        public List<Stats> stats {get;set;}
    }
}