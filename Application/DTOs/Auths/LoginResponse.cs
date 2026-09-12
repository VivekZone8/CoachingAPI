using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Auths
{
    //public class LoginResponse
    //{
    //    public string AccessToken { get; set; } = string.Empty;
    //    public DateTime ExpiresAt { get; set; }
    //}

    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? AccessToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public long? SessionLogId { get; set; }
    }
    public class LogoutRequest
    {
        public long SessionLogId { get; set; }
    }
}
