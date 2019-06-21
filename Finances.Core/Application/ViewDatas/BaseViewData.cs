using Finances.Common.Data;

namespace Finances.Core.Application.ViewDatas
{
    public class BaseViewData
    {
        public UserData UserLogged { get; set; }
        public bool HasLateralMenu { get; set; } = true;

        public BaseViewData(UserData userLogged)
        {
            UserLogged = userLogged;
        }
    }
}
