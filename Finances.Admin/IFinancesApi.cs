using Finances.Core.Application.ViewModels;
using RestEase;
using System.Threading.Tasks;

[Header("User-Agent", "RestEase")]
public interface IFinancesApi
{
    [Post("auth/login")]
    Task<UserAuthViewModel> Login([Body] LoginInfoViewModel loginInfo);
}
