using System.Runtime.Serialization;

namespace CryptoStats.GDAX.Services.Orders.Types
{
    public enum OrderSide
    {
        [EnumMember(Value = "buy")]
        Buy,
        [EnumMember(Value = "sell")]
        Sell
    }
}
