using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateCreditSecuritiesDto
    {
        public long SeqNo { get; set; }
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string SecurityType { get; set; }
        public string SecurityRefNo { get; set; }
        public decimal SecurityValue { get; set; }
        public string CurrCode { get; set; }
        public string ValuerType { get; set; }
        public string AddedBy { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public string Description { get; set; }
        public decimal ForcedSaleValue { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string SecurityAddress { get; set; }
        public string SecurityLocationCode { get; set; }
        public string SecurityTitleCode { get; set; }

    }
}
