using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Persistence.Services;


namespace Eazy.Credit.Security.Contracts.Persistence
{
    public interface ICreditServices
    {
        Task<ViewAPIResponse<List<CreditScheduleSummaryDto>>> LoanBooking(LoanApplicationRequestDto request);
        Task<ViewAPIResponse<ViewLookupResponse>> Lookup();
        Task<ViewAPIResponse<List<CreditScheduleSummaryDto>>> GetCreditSchedulesLines(string loanId);
        Task<ViewAPIResponse<List<CreditScheduleSummaryDto>>> LoanBookingWithDepencies(CreateCreditMaintHistWithDependenciesDto request);
        Task<ViewAPIResponse<List<AccountSearchDto>>> AccountEnquiry(string searchToken, SearchFlag searchFlag);
        Task<ViewAPIResponse<string>> CreditGuarantorMaint(CreateCreditGuarantorsDto creditGuarantorsMaint);
        Task<ViewAPIResponse<string>> CreditGuarantorMaint(List<CreateCreditGuarantorsDto> creditGuarantorsMaint);
        Task<ViewAPIResponse<string>> CreditSecurityMaint(CreateCreditSecuritiesDto creditSecuritiesMaint);
        Task<ViewAPIResponse<string>> CreditSecurityMaint(List<CreateCreditSecuritiesDto> creditSecuritiesMaint);
        Task<ViewAPIResponse<string>> CreditUpfrontChargesMaint(CreateCreditChargeDto creditChargesMaint);
        Task<ViewAPIResponse<string>> CreditUpfrontChargesMaint(List<CreateCreditChargeDto> creditChargesMaint);
        Task<ViewAPIResponse<string>> EditCreditSecurity(CreateCreditSecuritiesDto request);
        Task<ViewAPIResponse<string>> EditCreditGuarantor(CreateCreditGuarantorsDto request);
        Task<ViewAPIResponse<string>> EditCreditCharge(CreateCreditChargeDto request);
        Task<ViewAPIResponse<string>> EditCreditSecurity(List<CreateCreditSecuritiesDto> request);
        Task<ViewAPIResponse<string>> EditCreditGuarantor(List<CreateCreditGuarantorsDto> request);
        Task<ViewAPIResponse<string>> EditCreditCharge(List<CreateCreditChargeDto> request);
        Task<ViewAPIResponse<string>> RemoveCreditSecurity(long seqNo);
        Task<ViewAPIResponse<string>> RemoveCreditSecurityByCreditId(string creditId);
        Task<ViewAPIResponse<string>> RemoveCreditGuarantor(long seqNo);
        Task<ViewAPIResponse<string>> RemoveCreditGuarantorByCreditId(string creditId);
        Task<ViewAPIResponse<string>> RemoveCreditCharge(long sequence);
        Task<ViewAPIResponse<string>> RemoveCreditChargeByCreditId(string creditId);
        Task<ViewAPIResponse<List<CreditMaintResultDto>>> GetAllLoansByUserAsync(string userId);
        Task<ViewAPIResponse<List<CreditMaintResultDto>>> GetAllLoansWithDepenciesByUserAsync(string userId);
        Task<ViewAPIResponse<CreditMaintResultDto>> GetLoansByCreditIdAsync(string creditId);
        Task<ViewAPIResponse<CreditMaintResultDto>> GetLoanWithDepenciesByCreditIdAsync(string creditId);
        Task<ViewAPIResponse<List<CreditGuarantorsResultDto>>> GetCreditGuarantorsByCreditId(string creditId);
        Task<ViewAPIResponse<List<CreditChargeResultDto>>> GetCreditChargesByCreditId(string creditId);
        Task<ViewAPIResponse<List<CreditSecuritiesResultDto>>> GetCreditSecuritiesByCreditId(string creditId);
        Task<ViewAPIResponse<CreditScheduleParametersResultDto>> GetCreditScheduleHeaderByCreditId(string creditId);

        Task<ViewAPIResponse<string>> RemoveCreditCharge(List<long> request);
        Task<ViewAPIResponse<string>> RemoveCreditGuarantor(List<long> request);
        Task<ViewAPIResponse<string>> RemoveCreditSecurity(List<long> request);
        Task<ViewAPIResponse<string>> EditCreditMaintHist(CreditUpdateRequestDto request);
        Task<ViewAPIResponse<string>> RemoveCreditMaintHistByCreditId(string creditId);

        Task<ViewAPIResponse<ViewGuarantorLookupResponse>> GuarantorLookup();
        Task<ViewAPIResponse<ViewSecurityLookupResponse>> SecurityLookup();
        Task<ViewAPIResponse<ViewChargesLookupResponse>> UpfrontFeeLookup();
    }
}
