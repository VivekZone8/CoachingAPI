using Application.DTOs.Payment;
using Application.Interfaces.Repositories;
using Dapper;
using Infrastructure.Data;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PaymentRepository(
            IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<decimal> GetRegistrationFeeAsync(
            long registrationId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            var amount =
                await connection.QueryFirstOrDefaultAsync<int?>(
                    "sp_GetRegistrationFee",
                    new
                    {
                        RegistrationId = registrationId
                    },
                    commandType: CommandType.StoredProcedure);

            return amount ?? 0;
        }

        public async Task<long> CreatePaymentAsync(
       long registrationId,
       decimal amount,
       string paymentMethod,
       string gatewayOrderId, string feeType, string? ReferenceType, long? ReferenceId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            var paymentId =
                await connection.QuerySingleAsync<long>(
                    "sp_CreatePayment",
                    new
                    {
                        StudentId = registrationId,
                        Amount = amount,
                        PaymentMethod = paymentMethod,
                        GatewayOrderId = gatewayOrderId,
                        FeeType= feeType
                    },
                    commandType: CommandType.StoredProcedure);

            return paymentId;
        }

        public async Task<string?> GetStudentStatusAsync(long studentId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<string>(
                "sp_GetStudentStatus",
                new
                {
                    StudentId = studentId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<PaymentResponse> VerifyPaymentAsync(
    long paymentId,
    string gatewayPaymentId,
    string gatewayOrderId)
        {
            using var connection =
                _connectionFactory.CreateConnection();

            return await connection.QuerySingleAsync<PaymentResponse>(
                "sp_VerifyPayment",
                new
                {
                    PaymentId = paymentId,
                    GatewayPaymentId = gatewayPaymentId,
                    GatewayOrderId = gatewayOrderId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
