using System;

namespace Finances.Core.Application.ViewModels
{
    public class FavoredToEditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public AccountViewModel Account { get; set; }
    }
}
