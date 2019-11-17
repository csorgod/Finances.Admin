using static Finances.Common.Data.Enum;

namespace Finances.Core.Application.ViewModels
{
    public class SignIn
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginMode LoginMode { get; set; }
    }
}
