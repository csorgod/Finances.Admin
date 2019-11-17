﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewModels
{
    public class SignUp
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }
    }
}
