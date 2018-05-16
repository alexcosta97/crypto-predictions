using System;
using CryptoStats.GDAX.Network.HttpClient;
using CryptoStats.GDAX.Network.HttpRequest;
using CryptoStats.GDAX.Services.Products;
using CryptoStats.GDAX.Shared.Utilities.Clock;
using CryptoStats.GDAX.Shared.Utilities.Queries;

namespace CryptoStats.GDAX
{
    public class GDAXClient
    {
        public GDAXClient(bool sandBox = false) : this( new HttpClient(), sandBox){}

        public GDAXClient(IHttpClient httpClient, bool sandBox = false)
        {
            var clock = new Clock();
            var httpRequestMessageService = new HttpRequestMessageService(clock, sandBox);
            var queryBuilder = new QueryBuilder();
            
            ProductsService = new ProductsService(httpClient, httpRequestMessageService, queryBuilder);
        }

        public ProductsService ProductsService{get;}
    }
}