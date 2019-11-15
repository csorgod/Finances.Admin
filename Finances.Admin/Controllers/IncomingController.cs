using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Finances.Admin.Controllers
{
    public class IncomingController : BaseController
    {
        public IncomingController(IOptions<AppSettings> appSettings) : base(appSettings) { }

        public async Task<IActionResult> New()
        {
            string[] breadcrumb = { "rendas", "Nova" };
            ViewData["breadcrumb"] = breadcrumb;

            if (TempData["error"] != null)
                ViewData["error"] = TempData["error"];

            if (TempData["success"] != null)
                ViewData["success"] = TempData["success"];

            return View();
        }

        public async Task<IActionResult> Create(IncomingViewModel newIncoming)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Houve um erro ao cadastrar a renda. Por favor, verifique os campos digitados e tente novamente";
                return RedirectToAction("New");
            }

            var resp = await Http.Post<IncomingViewModel>("incomings/create", newIncoming);
            if (resp.Success)
            {
                TempData["success"] = resp.Message;
                return RedirectToAction("Incomings", "Home");
            }
            TempData["error"] = resp.Error;
            return RedirectToAction("New");
        }
    }
}