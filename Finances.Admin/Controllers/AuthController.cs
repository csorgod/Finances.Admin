using Finances.Common.Data;
using Finances.Core.Application.Services;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using static Finances.Common.Data.Enum;

namespace Finances.Admin.Controllers
{
    public class AuthController : BaseController
    {
        private readonly AuthService Service;

        public AuthController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            Service = new AuthService(appSettings);
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]SignIn model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            model.LoginMode = LoginMode.Admin;

            try
            {
                var userAuth = await Service.Login(model);

                if (userAuth.Payload == null)
                {
                    RegisterMessage("Usuário e/ou senha incorretos", MessageType.ErrorMessage);
                    return RedirectToAction(nameof(Index));
                }

                Storage.Store("UserJwt", userAuth.Payload.JwtToken);
                Storage.Store("UserLogged", JsonConvert.SerializeObject(userAuth.Payload.User));

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                RegisterMessage("Houve uma falha no login. Por favor, tente novamente mais tarde", MessageType.ErrorMessage);
                return Index();
            }
        }

        public async Task<IActionResult> ForgotPassword()
        {
            return View("ForgotPassword");
        }

        public async Task<IActionResult> SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]SignUp model)
        {
            if (!ModelState.IsValid)
                return View("SignUp", model);

            var response = await Service.Register(model);

            if (response.Success)
                return await Login(new SignIn { Username = model.Username, Password = model.Password });

            RegisterMessage("", MessageType.ErrorMessage);

            return await SignUp();
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                var resp = await Service.Logout(new SignOut { Username = UserLogged?.Username });
            }
            catch (Exception ex)
            {
                RegisterMessage("Houve uma falha no logoff, mas conseguimos finalizar sua sessão.", MessageType.ErrorMessage);
                return Index();
            }
            
            if (Storage.Keys().Count > 0)
                Storage.Clear();

            return Index();
        }
    }
}