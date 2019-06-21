using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewModels
{
    public class ProfileViewModel
    {
        #region Person

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string TaxNumber { get; set; }
        public char Gender { get; set; }

        #endregion

        #region User

        public string UserName { get; set; }
        public string Password { get; set; }

        #endregion

        #region UserImage

        public string UserImage { get; set; }

        #endregion

        #region Contact

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        #endregion
    }
}
