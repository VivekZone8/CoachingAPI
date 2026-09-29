using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Payment
{
    public class PaymentWebhookRequest
    {
        public string Event { get; set; } = string.Empty;

        public string? TransactionId { get; set; }

        public string? OrderId { get; set; }

        public decimal Amount { get; set; }

        public string? Status { get; set; }

        public string? Signature { get; set; }
    }
}
