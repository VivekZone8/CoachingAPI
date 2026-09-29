using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Payment
{
    public class CreatePaymentRequest
    {
        public long RegistrationId { get; set; }

        public decimal Amount { get; set; }

        public int PaymentMethod { get; set; }

        public string FeeType { get; set; } = string.Empty;

        public string? ReferenceType { get; set; }

        public long? ReferenceId { get; set; }
    }
}
