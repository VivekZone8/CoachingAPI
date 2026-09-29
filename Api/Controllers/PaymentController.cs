using Application.DTOs.Payment;
using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentService;

        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePaymentRequest request)
        {
            var result =
                await _paymentService.CreatePaymentAsync(request);

            return Ok(result);
        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify(PaymentVerificationRequest request)
        {
            var result =
                await _paymentService.VerifyPaymentAsync(request);

            return Ok(result);
        }
    }
}
