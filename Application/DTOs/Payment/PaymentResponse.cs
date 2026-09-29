using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Payment
{
    public class PaymentResponse
    {
        public long PaymentId { get; set; }

        public long RegistrationId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? TransactionId { get; set; }

        public string? PaymentUrl { get; set; }

        // Razorpay
        public string? OrderId { get; set; }

        public string? KeyId { get; set; }

        public bool? Success { get; set; }

        public string? Message { get; set; }
    }
}