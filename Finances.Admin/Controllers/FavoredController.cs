using Finances.Common.Data;
using Finances.Core.Application.ViewDatas;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Finances.Admin.Controllers
{
    public class FavoredController : BaseController
    {
        public FavoredController(IOptions<AppSettings> appSettings) : base(appSettings) { }

        public async Task<IActionResult> New()
        {
            string[] breadcrumb = { "Favorecidos", "Novo" };
            ViewData["breadcrumb"] = breadcrumb;

            if (TempData["error"] != null)
                ViewData["error"] = TempData["error"];

            if (TempData["success"] != null)
                ViewData["success"] = TempData["success"];

            return View(new BaseViewData(UserLogged));
        }
        
        public async Task<IActionResult> Create(FavoredViewModel newFavored)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Houve um erro ao cadastrar o favorecido. Por favor, verifique os campos digitados e tente novamente";
                return RedirectToAction("New");
            }

            newFavored.CreatedAt = DateTime.Now;

            var resp = await Http.Post<FavoredViewModel>("favoreds/create", newFavored);

            if (resp.Success)
            {
                TempData["success"] = resp.Message;
                return RedirectToAction("Favoreds", "Home");
            }
            TempData["error"] = resp.Message;
            return RedirectToAction("New");
        }

        public async Task<IActionResult> Show(Guid id)
        {
            string[] breadcrumb = { "Favorecidos", "Detalhes" };

            ViewData["breadcrumb"] = breadcrumb;

            return View(new BaseViewData(UserLogged));
        }
        
        public async Task<IActionResult> Edit(Guid id)
        {
            string[] breadcrumb = { "Favorecidos", "Editar" };

            ViewData["breadcrumb"] = breadcrumb;

            try
            {
                var resp = await Http.Get<FavoredToEditViewModel>("Favored/" + id);

                if (!resp.Success)
                {
                    ViewData["error"] = "Houve um erro ao encontrar o Favorecido: " + resp.Message;
                    return RedirectToAction("Favoreds", "Home");
                }

                return View(new FavoredEditViewData(UserLogged, resp.Payload));
            }
            catch
            {
                ViewData["error"] = "Houve um erro ao encontrar o Favorecido. Por favor, tente novamente mais tarde.";
                return RedirectToAction("Favoreds", "Home");
            }
        }

        public async Task<IActionResult> Save(FavoredToEditViewModel favored)
        {   
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Os campos enviados estão incorretos. Por favor, verifique-os e tente novamente. " + ModelState.Values;
                return RedirectToAction("Edit", favored.Id);
            }
            
            try
            {
                var fav = await Http.Post<FavoredToEditViewModel>("Favored/Edit", favored);
                
                if(fav.Success)
                {
                    TempData["success"] = fav.Message;
                    return RedirectToAction("Favoreds", "Home");
                }

                TempData["error"] = fav.Message;
                return RedirectToAction("Edit", favored.Id);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Edit", favored.Id);
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                TempData["error"] = "Houve um erro ao excluir o favorecido. Por favor, verifique o favorecido selecionado e tente novamente";
                return RedirectToAction("Favoreds", "Home");
            }

            var resp = await Http.Delete<FavoredViewModel>("Favored/Delete/" + id);

            if (resp.Success)
            {
                ViewData["success"] = "Favorecido excluído com sucesso";
                return RedirectToAction("Favoreds", "Home");
            }
            else
            {
                ViewData["error"] = "Houve um erro ao excluir o Favorecido: " + resp.Message;
                return RedirectToAction("Favoreds", "Home");
            }
        }
    }
}