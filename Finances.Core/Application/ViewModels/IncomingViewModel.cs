using System;
using static Finances.Common.Data.Enum;

namespace Finances.Core.Application.ViewModels
{
    public class IncomingViewModel
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public string UserId { get; set; }
        public IncomeType IncomeType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime ReceiveAt { get; set; }
        public bool Recurrent { get; set; }
    }
}
