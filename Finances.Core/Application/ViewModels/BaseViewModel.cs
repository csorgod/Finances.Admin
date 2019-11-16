using System;

namespace Finances.Core.Application.ViewModels
{
    public class BaseViewModel
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
