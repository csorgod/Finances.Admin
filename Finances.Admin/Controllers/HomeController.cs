using Finances.Common.Data;
using Finances.Core.Application.ViewDatas;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Finances.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IOptions<AppSettings> appSettings) : base(appSettings) { }

        public IActionResult Index()
        {
            string[] breadcrumb = { "Dashboard" };
            ViewData["breadcrumb"] = breadcrumb;

            if (TempData["error"] != null)
                ViewData["error"] = TempData["error"];

            if (TempData["success"] != null)
                ViewData["success"] = TempData["success"];

            return View("Index", new BaseViewData(UserLogged));
        }

        public async Task<IActionResult> Favoreds()
        {
            string[] breadcrumb = { "Favorecidos" };
            ViewData["breadcrumb"] = breadcrumb;

            if (TempData["success"] != null)
                ViewData["success"] = ViewData["success"];

            var favoreds = new JsonDefaultResponse<List<FavoredViewModel>>();

            try
            {
                favoreds = await Http.Get<List<FavoredViewModel>>("Favored/ByUserId");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Index();
            }

            if (favoreds.Payload == null)
            {
                favoreds.Payload = new List<FavoredViewModel>();
                TempData["favoreds"] = favoreds.Message;
            }

            return View(new FavoredViewData(UserLogged, favoreds.Payload));
        }

        public async Task<IActionResult> BillsToPay()
        {
            string[] breadcrumb = { "Contas a pagar" };

            ViewData["breadcrumb"] = breadcrumb;

            return View(new BillsToPayViewData(UserLogged, new List<BillsToPayViewModel>()));
        }

        public async Task<IActionResult> PaidBills()
        {
            string[] breadcrumb = { "Contas pagas" };

            ViewData["breadcrumb"] = breadcrumb;

            return View(new PaidBillsViewData(UserLogged, new List<PaidBillsViewModel>()));
        }

        public async Task<IActionResult> Incomings()
        {
            string[] breadcrumb = { "rendas" };

            ViewData["breadcrumb"] = breadcrumb;

            JsonDefaultResponse<List<IncomingViewModel>> incomings = new JsonDefaultResponse<List<IncomingViewModel>>();
            try
            {
                incomings = await Http.Get<List<IncomingViewModel>>("incomings");
            }
            catch
            {

            }

            if (incomings.Payload == null)
            {
                incomings.Payload = new List<IncomingViewModel>();
                ViewData["incomings"] = incomings.Message;
            }

            return View(new IncomingViewData(UserLogged, incomings.Payload));
        }

        public async Task<IActionResult> Statistics()
        {
            string[] breadcrumb = { "Estatisticas" };

            ViewData["breadcrumb"] = breadcrumb;

            return View();
        }

        public async Task<IActionResult> Users()
        {
            string[] breadcrumb = { "Usuários" };

            ViewData["breadcrumb"] = breadcrumb;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var response = HttpContext.Response;

            ErrorViewModel errorModel = default;

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                errorModel = new ErrorViewModel
                {
                    ErrorCode = 404,
                    Message = "Página não encontrada",
                    Description = "A pagina que você estava buscando não foi encontrada. O que deseja fazer?"
                };
            else if(exceptionHandlerPathFeature?.Error is Exception)
                errorModel = new ErrorViewModel
                {
                    ErrorCode = 500,
                    Message = "Erro interno do servidor",
                    Description = "Houve um erro no servidor ao tentar tratar sua solicitação. O que deseja fazer?"
                };

            return View(errorModel);
        }

        public async Task<IActionResult> ReportAProblem()
        {
            return null;
        }
    }
}
