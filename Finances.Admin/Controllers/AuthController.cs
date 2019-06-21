using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static Finances.Common.Data.Enum;

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

            model.LoginMode = LoginMode.Admin;

            var userAuth = new JsonDefaultResponse<UserAuthViewModel>();

            try
            {
                userAuth = await Http.Post<UserAuthViewModel>("auth/login", model);
            }
            catch
            {
                TempData["error"] = "Houve uma falha no login. Por favor, tente novamente mais tarde";
                return RedirectToAction("Index", "Auth");
            }

            if (userAuth.Payload == null)
            {
                TempData["error"] = "Usuário e/ou senha incorretos";
                return RedirectToAction("Index", "Auth");
            }
            Storage.Store("UserJwt", userAuth.Payload.JwtToken);

            Storage.Store("UserLogged", JsonConvert.SerializeObject(userAuth.Payload.User));

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