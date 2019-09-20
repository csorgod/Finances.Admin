using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Finances.Common.Data.Enum;
using RestEase;

namespace Finances.Admin.Controllers
{
    public class AuthController : BaseController
    {
        
        public AuthController(IOptions<AppSettings> appSettings) : base(appSettings) { }

        public IActionResult Index()
        {
            if (TempData["error"] != null)
                ViewData["error"] = TempData["error"];

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginInfoViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            IFinancesApi api = RestClient.For<IFinancesApi>("http://localhost:44358");
            UserAuthViewModel resp = await api.Login(model);
            
            Storage.Store("UserJwt", resp.Payload.JwtToken);
            Storage.Store("UserLogged", JsonConvert.SerializeObject(resp.Payload.User));

            // model.LoginMode = LoginMode.Admin;

            // var userAuth = new JsonDefaultResponse<UserAuthViewModel>();

            // try
            // {
            //     var loginRequest = new StringContent(model.ToString(), Encoding.UTF8, "application/json");
            //     // var loginRequest = JsonConvert.SerializeObject(model);
            //     userAuth = await Http.Post<UserAuthViewModel>("auth/login", loginRequest);
            // }
            // catch
            // {
            //     System.Console.WriteLine("\nFALIOU");
            //     TempData["error"] = "Houve uma falha no login. Por favor, tente novamente mais tarde";
            //     return RedirectToAction("Index", "Auth");
            // }

            // if (userAuth.Payload == null)
            // {
            //     TempData["error"] = "Usuário e/ou senha incorretos";
            //     return RedirectToAction("Index", "Auth");
            // }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            if (Storage.Keys().Count > 0)
                Storage.Clear();
            return View("Index");
        }
    }
}