using static Finances.Common.Data.Enum;

namespace Finances.Core.Application.ViewModels
{
    public class LoginInfoViewModel
    {
        public string username { get; set; }

        public string password { get; set; }

        public LoginMode LoginMode { get; set; }
    }
}
