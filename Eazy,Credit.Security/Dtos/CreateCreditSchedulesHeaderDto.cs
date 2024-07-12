using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateCreditSchedulesHeaderDto
    {
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string ScheduleType { get; set; }
        public DateTime InterestRepayNextDate { get; set; }
        public DateTime PrincipalRepayNextDate { get; set; }
        public string InterestRepayFreq { get; set; }
        public string PrincipalRepayFreq { get; set; }
        public int InterestRepayNumber { get; set; }
        public int PrincipalRepayNumber { get; set; }
        public string InterestArrearsTreatment { get; set; }
        public string PrincipalArrearsTreatment { get; set; }
        public string ChargesArrearsTreatment { get; set; }
        public bool ObserveLimit { get; set; }
        public string AddedBy { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public short BaseYear { get; set; }
        public bool StaggeredRepaymeent { get; set; }
        public bool UseExistingAnnuity { get; set; }
        public short PrincipalMorat { get; set; }
        public string PrincipalMoratFreq { get; set; }
        public short InterestMorat { get; set; }
        public string InterestMoratFreq { get; set; }
        public bool AmortizeMoratoriumInterest { get; set; }
        public decimal MoratoriumInterest { get; set; }
        public DateTime? MoratoriumIntAppDate { get; set; }
        public DateTime? EffectiveDate { get; set; }

    }
}
