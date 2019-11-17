using Finances.Common.Data;
using Finances.Core.Application.Services;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Http;
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
            if (HttpContext.Session.GetString("Username") != null)
                ViewData["Username"] = HttpContext.Session.GetString("Username");

            return View(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]SignIn model)
        {
            try
            {
                var userAuth = await Service.Login(model);

                if (userAuth.Success)
                {   
                    Storage.Store("UserJwt", userAuth.Payload.JwtToken);
                    Storage.Store("UserLogged", JsonConvert.SerializeObject(userAuth.Payload.User));

                    if(model.RememberUser == OnOrOff.On)
                        HttpContext.Session.SetString("Username", userAuth.Payload.User.Username);

                    return RedirectToAction("Index", "Home");
                }
                else if(userAuth.Payload == null)
                {
                    RegisterMessage("Usuário e/ou senha incorretos", MessageType.ErrorMessage);
                    return RedirectToAction(nameof(Index));
                }

                RegisterMessage("Houve um erro com o servidor. Por favor tente novamente mais tarde", MessageType.ErrorMessage);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                RegisterMessage("Houve uma falha no login. Por favor, tente novamente mais tarde", MessageType.ErrorMessage);
                return Index();
            }
        }

        public async Task<IActionResult> ForgotPassword()
        {
            return View(nameof(ForgotPassword));
        }

        public async Task<IActionResult> SignUp()
        {
            return View(nameof(SignUp));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]SignUp model)
        {
            if (!ModelState.IsValid)
                return View(nameof(SignUp), model);

            var response = await Service.Register(model);

            if (response.Success)
                return await Login(new SignIn { Username = model.Username, Password = model.Password });

            RegisterMessage("Houve um erro ao criar sua conta.", MessageType.ErrorMessage);

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