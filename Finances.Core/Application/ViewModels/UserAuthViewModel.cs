using Finances.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewModels
{
    public class UserAuthViewModel : BaseViewModel
    {
        public UserAuthPayload Payload { get; set; }
    }

    public class UserAuthPayload
    {
        public string JwtToken { get; set; }
        public UserData User { get; set; }
    }
}
