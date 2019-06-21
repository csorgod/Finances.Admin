using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using System.Collections.Generic;

namespace Finances.Core.Application.ViewDatas
{
    public class FavoredViewData : BaseViewData
    {
        public List<FavoredViewModel> Favoreds;
        public FavoredViewData(UserData userLogged, List<FavoredViewModel> favoreds) : base(userLogged)
        {
            UserLogged = userLogged;
            Favoreds = favoreds;
        }
    }
}
