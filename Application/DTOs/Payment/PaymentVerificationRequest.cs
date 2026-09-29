using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Payment
{
    public class PaymentVerificationRequest
    {
        public long PaymentId { get; set; }

        public string? TransactionId { get; set; }

        public string? GatewayPaymentId { get; set; }

        public string? GatewayOrderId { get; set; }

        public string? Signature { get; set; }
    }
}
