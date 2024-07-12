namespace Eazy.Credit.Security.Dtos
{
    public class CreditScheduleParametersDto
    {
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public virtual DateTime PrincipalRepaymentStartDate { get; set; }
        public virtual DateTime InterestRepaymentStartDate { get; set; }
        public virtual string PrincipalRepaymentFreq { get; set; }
        public virtual string InterestRepaymentFreq { get; set; }
        public virtual short PrincipalMorat { get; set; }
        public virtual string PrincipalMoratFreq { get; set; }
        public virtual short InterestMorat { get; set; }
        public virtual string InterestMoratFreq { get; set; }
    }

}
