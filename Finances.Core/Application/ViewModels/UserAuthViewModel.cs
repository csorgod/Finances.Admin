using Finances.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewModels
{
    public class Payload
    {
        public string JwtToken { get; set; }
        public UserData User { get; set; }
    }

    public class UserAuthViewModel
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public Payload Payload { get; set; }
    }
}
