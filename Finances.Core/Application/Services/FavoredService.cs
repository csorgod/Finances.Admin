using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using Microsoft.Extensions.Options;

namespace Finances.Core.Application.Services
{
    public class FavoredService : BaseService
    {
        public FavoredService(IOptions<AppSettings> appSettings) : base(appSettings) { }

        public async Task<JsonDefaultResponse<FavoredViewModel>> CreateFavored(FavoredViewModel newFavored)
            => await Http.Post<FavoredViewModel>("favored", newFavored);
    }
}
