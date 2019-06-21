using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finances.Common.Data;
using Finances.Core.Application.ViewDatas;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Finances.Admin.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IOptions<AppSettings> appSettings) : base(appSettings) { }

        public async Task<IActionResult> Profile()
        {
            string[] breadcrumb = { "Usuário", "Perfil" };
            ViewData["breadcrumb"] = breadcrumb;

            if (ViewData["success"] != null)
                ViewData["success"] = ViewData["success"];

            JsonDefaultResponse<ProfileViewModel> profile = new JsonDefaultResponse<ProfileViewModel>();
            try
            {
                profile = await Http.Get<ProfileViewModel>("user/GetProfileByUserId");
            }
            catch (Exception ex)
            {

            }

            if (profile.Payload == null)
            {
                profile.Payload = new ProfileViewModel();
                ViewData["favoreds"] = profile.Message;
            }

            return View(new ProfileViewData(UserLogged, profile.Payload));
        }
    }
}