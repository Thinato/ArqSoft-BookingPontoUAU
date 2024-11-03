using Domain.Rooms.Enums;

namespace Domain.Rooms.ValueObjects;
public class Price
{
    protected Price() { }

    public Price(decimal value, AcceptedCurrencies currency)
    {
        Value = value;
        Currency = currency;
    }

    public decimal Value { get; }
    public AcceptedCurrencies Currency { get; }
}
