using Finances.Common.Data;
using Finances.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewDatas
{
    public class ProfileViewData : BaseViewData
    {
        public ProfileViewModel Profile;
        public ProfileViewData(UserData userLogged, ProfileViewModel profile) : base(userLogged)
        {
            UserLogged = userLogged;
            Profile = profile;
        }
    }
}
