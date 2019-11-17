using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Common.Data
{
    public class Enum
    {
        public enum Status
        {
            Active = 0,
            Inactive = 1,
            Blocked = 2
        }

        public enum LoginMode
        {
            None = 0,
            Admin = 1,
            App = 2
        }

        public enum IncomeType
        {
            Fix = 0,
            variable = 1,
            detached = 2
        }

        public enum MessageType
        {
            SuccessMessage = 0,
            ErrorMessage = 1
        }

        public enum OnOrOff
        {
            Off = 0,
            On = 1
        }
    }
}
