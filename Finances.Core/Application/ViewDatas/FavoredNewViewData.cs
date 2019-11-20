using System;
using System.Collections.Generic;
using System.Text;
using Finances.Common.Data;
using Finances.Core.Application.ViewModels;

namespace Finances.Core.Application.ViewDatas
{
    public class FavoredNewViewData : BaseViewData
    {
        public FavoredViewModel Favored;

        public FavoredNewViewData(UserData userLogged, FavoredViewModel favored) : base(userLogged)
        {
            UserLogged = userLogged;
            Favored = favored;
        }
    }
}
