using Application.DTOs.Payment;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Payments.Razorpay
{
    public class RazorpayPaymentGateway : IPaymentGateway
    {
        private readonly string _keyId;
        private readonly string _keySecret;

        public RazorpayPaymentGateway(
            IConfiguration configuration)
        {
            _keyId = configuration["Razorpay:KeyId"]
                ?? throw new InvalidOperationException(
                    "Razorpay KeyId is not configured.");

            _keySecret = configuration["Razorpay:KeySecret"]
                ?? throw new InvalidOperationException(
                    "Razorpay KeySecret is not configured.");
        }

        public async Task<PaymentResponse> CreatePaymentAsync(CreatePaymentRequest request) 
        {
            try
            {
                var client = new RazorpayClient(_keyId, _keySecret);

                var amountInPaise =
                    Convert.ToInt64(request.Amount * 100);

                var options = new Dictionary<string, object>
        {
            { "amount", amountInPaise },
            { "currency", "INR" },
            { "receipt", $"REG_{request.RegistrationId}" }
        };

                Order order = client.Order.Create(options);

                return new PaymentResponse
                {
                    Success = true,

                    RegistrationId = request.RegistrationId,
                    Amount = request.Amount,

                    PaymentMethod = "Razorpay",
                    Status = "Created",

                    // IMPORTANT
                    OrderId = order["id"]?.ToString(),

                    // Frontend Razorpay Checkout ke liye
                    KeyId = _keyId,

                    TransactionId = null,

                    Message = "Razorpay order created successfully."
                };
            }
            catch (Exception ex)
            {
                return new PaymentResponse
                {
                    Success = false,
                    RegistrationId = request.RegistrationId,
                    Amount = request.Amount,
                    PaymentMethod = "Razorpay",
                    Status = "Failed",
                    Message = ex.Message
                };
            }
        }

        public async Task<PaymentResponse> VerifyPaymentAsync(
     PaymentVerificationRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.GatewayOrderId) ||
                    string.IsNullOrWhiteSpace(request.GatewayPaymentId) ||
                    string.IsNullOrWhiteSpace(request.Signature))
                {
                    return new PaymentResponse
                    {
                        Success = false,
                        PaymentId = request.PaymentId,
                        PaymentMethod = "Razorpay",
                        Status = "Failed",
                        Message = "Payment verification details are missing."
                    };
                }

                // Razorpay:
                // order_id|payment_id

                var payload =
                    $"{request.GatewayOrderId}|{request.GatewayPaymentId}";

                using var hmac =
                    new HMACSHA256(
                        Encoding.UTF8.GetBytes(_keySecret));

                var hash =
                    hmac.ComputeHash(
                        Encoding.UTF8.GetBytes(payload));

                var generatedSignature =
                    Convert.ToHexString(hash)
                        .ToLowerInvariant();

                var razorpaySignature =
                    request.Signature.Trim().ToLowerInvariant();

                if (generatedSignature != razorpaySignature)
                {
                    return new PaymentResponse
                    {
                        Success = false,
                        PaymentId = request.PaymentId,
                        TransactionId = request.GatewayPaymentId,
                        PaymentMethod = "Razorpay",
                        Status = "Failed",
                        Message = "Invalid Razorpay payment signature."
                    };
                }

                return new PaymentResponse
                {
                    Success = true,
                    PaymentId = request.PaymentId,
                    TransactionId = request.GatewayPaymentId,
                    PaymentMethod = "Razorpay",
                    Status = "Success",
                    Message = "Payment verified successfully."
                };
            }
            catch (Exception ex)
            {
                return new PaymentResponse
                {
                    Success = false,
                    PaymentId = request.PaymentId,
                    TransactionId = request.GatewayPaymentId,
                    PaymentMethod = "Razorpay",
                    Status = "Failed",
                    Message = ex.Message
                };
            }
        }


    }
}