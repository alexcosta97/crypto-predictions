using System.Runtime.Serialization;

namespace CryptoStats.GDAX.Services.Orders.Types
{
    public enum OrderType
    {
        [EnumMember(Value = "limit")]
        Limit,
        [EnumMember(Value = "market")]
        Market,
        [EnumMember(Value = "stop")]
        Stop
    }
}
