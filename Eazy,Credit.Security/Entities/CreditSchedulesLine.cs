

namespace Eazy.Credit.Security.Entities
{
    public class CreditSchedulesLine
    {
        public long Sequence { get; set; }
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public DateTime ValueDate { get; set; }
        public string LineType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ChargeBaseCode { get; set; }

        public virtual CreditSchedulesHeader Trans { get; set; }
    }
}
