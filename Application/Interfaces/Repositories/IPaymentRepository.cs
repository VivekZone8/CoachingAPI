using Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<decimal> GetRegistrationFeeAsync(
            long registrationId);

        Task<long> CreatePaymentAsync(
       long registrationId,
       decimal amount,
       string paymentMethod,
       string gatewayOrderId,string feeType,string? ReferenceType, long? ReferenceId);

        Task<string?> GetStudentStatusAsync(long studentId);

        Task<PaymentResponse> VerifyPaymentAsync(
    long paymentId,
    string gatewayPaymentId,
    string gatewayOrderId);
    }
}
