﻿using Finances.Common.Data;
using Finances.Core.Application.Services;
using Finances.Core.Application.ViewDatas;
using Finances.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using static Finances.Common.Data.Enum;

namespace Finances.Admin.Controllers
{
    public class FavoredController : BaseController
    {
        private readonly FavoredService Service;

        public FavoredController(IOptions<AppSettings> appSettings) : base(appSettings)
        {
            Service = new FavoredService(appSettings);
        }

        public async Task<IActionResult> New(FavoredViewModel newFavored = default)
        {
            CreateBreadCrumb("Favorecidos", "Novo");
            
            return View(new FavoredNewViewData(UserLogged, newFavored));
        }
        
        public async Task<IActionResult> Create(FavoredViewModel newFavored)
        {
            try
            {
                newFavored.BelongToUserId = Guid.Parse(UserLogged.Id);
                newFavored.CreatedAt = DateTime.Now;

                var resp = await Service.CreateFavored(newFavored);

                if (resp.Success)
                {
                    RegisterMessage(resp.Message, MessageType.SuccessMessage);
                    return RedirectToAction("Favoreds", "Home");
                }
                
                RegisterMessage(resp.Message, MessageType.ErrorMessage);
                return RedirectToAction(nameof(New), newFavored);

            } catch
            {
                RegisterMessage("Algo deu errado ao tentar cadastrar o favorecido.", MessageType.ErrorMessage);
                return RedirectToAction(nameof(New), newFavored);
            }           
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
                    ViewData["error"] = "Houve um erro ao encontrar o Favorecido: " + resp.Error;
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

                TempData["error"] = fav.Error;
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
                ViewData["error"] = "Houve um erro ao excluir o Favorecido: " + resp.Error;
                return RedirectToAction("Favoreds", "Home");
            }
        }
    }
}