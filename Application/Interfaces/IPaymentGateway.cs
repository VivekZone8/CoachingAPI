using Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentResponse> CreatePaymentAsync(
            CreatePaymentRequest request);

        Task<PaymentResponse> VerifyPaymentAsync(
            PaymentVerificationRequest request);
    }
}
