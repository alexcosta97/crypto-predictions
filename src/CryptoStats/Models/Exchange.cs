using System;
using System.Collections.Generic;

namespace CryptoStats.Models
{
    public class Exchange
    {
        public int ExchangeId {get;set;}
        public string name {get;set;}

        //Set List to establish foreign key
        public List<Stat> Stats {get;set;}
        
    }

}