using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Finances.Core.Application.Services
{
    public class AuthService : BaseService
    {
        private readonly string baseURI = "auth/";

        public AuthService(IOptions<AppSettings> appSettings) : base(appSettings) { }

        public async Task<JsonDefaultResponse<UserAuth>> Login(SignIn loginInfo)
        {
            return await Http.Post<UserAuth>($"{baseURI}login", loginInfo);
        }

        public async Task<JsonDefaultResponse<UserAuth>> Logout(SignOut loginInfo)
        {
            return await Http.Post<UserAuth>($"{baseURI}logout", loginInfo);
        }

        public async Task<JsonDefaultResponse<UserAuth>> Register(SignUp signUpInfo)
        {
            return await Http.Post<UserAuth>($"{baseURI}CreateAccount", signUpInfo);
        }
    }
}
