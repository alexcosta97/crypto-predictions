using System.Collections.Generic;

namespace CryptoStats.GDAX.Shared.Utilities.Queries
{
    public interface IQueryBuilder
    {
        string BuildQuery(params KeyValuePair<string, string>[] queryParameters);
    }
}
