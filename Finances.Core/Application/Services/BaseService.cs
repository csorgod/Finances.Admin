using Finances.Common.Data;
using Finances.Common.Helpers;
using Microsoft.Extensions.Options;

namespace Finances.Core.Application.Services
{
    public class BaseService
    {
        protected HttpClient Http;
        public IOptions<AppSettings> AppSettings;

        public BaseService(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings;
            Http = new HttpClient(AppSettings);
        }
    }
}