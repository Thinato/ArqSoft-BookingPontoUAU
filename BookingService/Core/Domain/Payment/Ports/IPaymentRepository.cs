using Domain.Payments.Entities;

namespace Domain.Payments.Ports;
public interface IPaymentRepository
{
    Task<Payment> GetPaymentByIdAsync(int id);
    Task<Payment> CreatePaymentAsync(Payment payment);
    Task<Payment> UpdatePaymentAsync(Payment payment);
    Task<IEnumerable<Payment>> GetPaymentsAsync();
    Task<IEnumerable<Payment>> GetPaymentsByRoomIdAsync(int roomId);
}
