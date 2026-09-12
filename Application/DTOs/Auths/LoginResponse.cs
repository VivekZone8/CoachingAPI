using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Auths
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
