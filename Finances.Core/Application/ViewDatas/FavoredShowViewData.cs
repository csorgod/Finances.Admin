using Finances.Common.Data;
using Finances.Core.Application.ViewModels;

namespace Finances.Core.Application.ViewDatas
{
    public class FavoredShowViewData : BaseViewData
    {
        public FavoredToShowViewModel Favored;
        public FavoredShowViewData(UserData userLogged, FavoredToShowViewModel favored) : base(userLogged)
        {
            UserLogged = userLogged;
            Favored = favored;
        }
    }
}
