using Domain.Payments.Enums;

namespace Domain.Payments.Entities;
public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime? PayedAt { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; }
    public AcceptedCurrencies Currency { get; set; }
}
