using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwRepaymentScheduleParam
    {
        public string TransID { get; set; }
        public string CreditID { get; set; }
        public string ScheduleType { get; set; }
        public DateTime InterestRepayNextDate { get; set; }
        public DateTime PrincipalRepayNextDate { get; set; }
        public string InterestRepayFreq { get; set; }
        public string PrincipalRepayFreq { get; set; }
        public int InterestRepayNumber { get; set; }
        public int PrincipalRepayNumber { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public int Base { get; set; }
        public bool Simulation { get; set; }
        public bool UseExistingAnnuityAmount { get; set; }
        public bool StaggeredRepayment { get; set; }

    }
}
