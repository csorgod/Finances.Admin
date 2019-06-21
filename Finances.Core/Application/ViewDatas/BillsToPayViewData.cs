using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewDatas
{
    public class BillsToPayViewData : BaseViewData
    {
        public List<BillsToPayViewModel> BillsToPay { get; set; }

        public BillsToPayViewData(UserData userLogged, List<BillsToPayViewModel> billsToPay) : base(userLogged)
        {
            UserLogged = userLogged;
            BillsToPay = billsToPay;
        }
    }
}
