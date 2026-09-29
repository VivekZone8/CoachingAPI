using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Payment
{
    public class PaymentStatusResponse
    {
        public long PaymentId { get; set; }

        public long RegistrationId { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = string.Empty;

        public string? TransactionId { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}
