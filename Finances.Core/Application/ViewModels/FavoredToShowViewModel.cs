using System;
using System.Collections.Generic;
using System.Text;

namespace Finances.Core.Application.ViewModels
{
    public class FavoredToShowViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public AccountViewModel Account { get; set; }
    }
}
