using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using System.Collections.Generic;

namespace Finances.Core.Application.ViewDatas
{
    public class IncomingViewData : BaseViewData
    {
        public List<IncomingViewModel> Incomings;
        public IncomingViewData(UserData userLogged, List<IncomingViewModel> incomings) : base(userLogged)
        {
            UserLogged = userLogged;
            Incomings = incomings;
        }
    }
}
