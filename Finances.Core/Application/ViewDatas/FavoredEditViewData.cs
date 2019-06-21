using Finances.Common.Data;
using Finances.Core.Application.ViewModels;

namespace Finances.Core.Application.ViewDatas
{
    public class FavoredEditViewData : BaseViewData
    {
        public FavoredToEditViewModel Favored;
        public FavoredEditViewData(UserData userLogged, FavoredToEditViewModel favored) : base(userLogged)
        {
            UserLogged = userLogged;
            Favored = favored;
        }
    }
}
