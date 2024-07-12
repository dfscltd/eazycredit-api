using Eazy.Credit.Security.Dtos;


namespace Eazy.Credit.Security.Contracts.Persistence
{
    public interface ICreditServiceFuncAndProc
    {
        Task<int> GenerateRepaymentSchedule(GenerateRepaymentScheduleDto repaymentSchedule);
        Task<List<CreditScheduleSummaryDto>> CreditSchedulesLinesSelectSummary(string creditID, string status);
        Task<string> GenerateNumberSequence(int numberID);
    }
}
