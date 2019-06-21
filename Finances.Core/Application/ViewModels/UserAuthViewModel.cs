using Finances.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewModels
{
    public class UserAuthViewModel
    {
        public string JwtToken { get; set; }
        public UserData User { get; set; }
    }
}
