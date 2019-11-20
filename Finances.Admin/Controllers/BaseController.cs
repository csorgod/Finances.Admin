using Finances.Common.Data;
using Finances.Common.Helpers;
using Hanssens.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static Finances.Common.Data.Enum;

namespace Finances.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected HttpClient Http;
        protected LocalStorage Storage;
        protected UserData UserLogged;
        public IOptions<AppSettings> AppSettings;
        
        public BaseController(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings;
        }               

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Http = new HttpClient(AppSettings);
            Storage = LocalStorageSingleton.Instance;
            if (Storage.Keys().Count > 0)
                UserLogged = JsonConvert.DeserializeObject<UserData>(Storage.Get<string>("UserLogged"));

            AttachMessages();

            base.OnActionExecuting(context);
        }

        protected void AttachMessages()
        {
            ViewData["error"] = HttpContext.Session.GetString("error");
            ViewData["success"] = HttpContext.Session.GetString("success");

            HttpContext.Session.Remove("error");
            HttpContext.Session.Remove("success");
        }

        protected void RegisterMessage(string message, MessageType messageType)
        {
            if (messageType == MessageType.ErrorMessage)
                HttpContext.Session.SetString("error", message);
            else
                HttpContext.Session.SetString("success", message);
        }

        protected void CreateBreadCrumb(params string[] args)
        {
            ViewData["breadcrumb"] = args;
        }
    }
}