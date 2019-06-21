using Finances.Common.Data;
using Finances.Common.Helpers;
using Hanssens.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Finances.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected HttpClient Http;
        protected LocalStorage Storage;
        protected UserData UserLogged;
        protected string Error;
        protected string Success;
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

            base.OnActionExecuting(context);
        }
    }
}