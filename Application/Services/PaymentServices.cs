using Application.DTOs.Payment;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepository _repository;
        private readonly IPaymentGateway _paymentGateway;

        public PaymentServices(
            IPaymentRepository repository,
            IPaymentGateway paymentGateway)
        {
            _repository = repository;
            _paymentGateway = paymentGateway;
        }

        public async Task<PaymentResponse> CreatePaymentAsync(
        CreatePaymentRequest request)
        {
            var status =await _repository.GetStudentStatusAsync(request.RegistrationId);

            if (status == null)
            {
                return new PaymentResponse
                {
                    RegistrationId = request.RegistrationId,
                    Success = false,
                    Status = "Failed",
                    Message = "Student registration not found."
                };
            }

            if (status == "Registered")
            {
                return new PaymentResponse
                {
                    RegistrationId = request.RegistrationId,
                    Success = false,
                    Status = "AlreadyRegistered",
                    Message = "Student is already registered. Payment order cannot be created."
                };
            }

            // Amount validation
            if (request.Amount <= 0)
            {
                return new PaymentResponse
                {
                    RegistrationId = request.RegistrationId,
                    Success = false,
                    Status = "Failed",
                    Message = "Invalid payment amount."
                };
            }

            // Abhi Razorpay order create hoga
            var result =
                await _paymentGateway.CreatePaymentAsync(request);

            if (result.Success == true &&
                !string.IsNullOrEmpty(result.OrderId))
            {
                var paymentId =
                    await _repository.CreatePaymentAsync(
                        request.RegistrationId,
                        request.Amount,
                        request.PaymentMethod.ToString(),
                        result.OrderId,
                        request.FeeType,
                        request.ReferenceType,
                        request.ReferenceId);

                result.PaymentId = paymentId;
            }

            return result;
        }

        public async Task<PaymentResponse> VerifyPaymentAsync(PaymentVerificationRequest request)
        {
            // 1. Razorpay signature verify karo
            var result =
                await _paymentGateway.VerifyPaymentAsync(request);

            // 2. Verification fail hai to DB update mat karo
            if (result.Success != true)
            {
                return result;
            }

            // 3. Signature valid hai → database update
            var dbResult =
                await _repository.VerifyPaymentAsync(
                    request.PaymentId,
                    request.GatewayPaymentId!,
                    request.GatewayOrderId!);

            // 4. DB ka result return karo
            return new PaymentResponse
            {
                PaymentId = request.PaymentId,
                TransactionId = request.GatewayPaymentId,
                PaymentMethod = "Razorpay",
                Success = dbResult.Success,
                Status = dbResult.Success == true
                    ? "Success"
                    : "Failed",
                Message = dbResult.Message
            };
        }
    }
}