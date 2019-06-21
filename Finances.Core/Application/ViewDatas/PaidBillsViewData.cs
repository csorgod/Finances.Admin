using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using System.Collections.Generic;

namespace Finances.Core.Application.ViewDatas
{
    public class PaidBillsViewData : BaseViewData
    {
        public List<PaidBillsViewModel> PaidBills { get; set; }

        public PaidBillsViewData(UserData userLogged, List<PaidBillsViewModel> paidBills) : base(userLogged)
        {
            UserLogged = userLogged;
            PaidBills = paidBills;
        }
    }
}
