using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Payment
{
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public string? TransactionId { get; set; }

        public string? GatewayOrderId { get; set; }

        public string? GatewayPaymentId { get; set; }

        public string? PaymentUrl { get; set; }
    }
}
