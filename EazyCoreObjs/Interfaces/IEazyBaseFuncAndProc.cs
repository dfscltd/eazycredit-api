using EazyCoreObjs.Models;
using EazyCoreObjs.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EazyCoreObjs.Interfaces
{
    public interface IEazyBaseFuncAndProc
    {
        Task<int> GenerateAccountingEntries(string screenCode, string transID, string transCode);
        Task<int> TransHeaderApprove(string transID, bool approved, string branchAddedApproved, string workstationApproved, string workstationIPApproved);
        Task<bool> TransApprovalLogApprove(string transID, string screenCode, string addedBy, string approvedBy, bool approved, string approveRejectReason, string branchAddedApproved, string workstationApproved, string workstationIPApproved, long sequence);
        Task<string> GenerateNumberSequence(int numberID);
        Task<decimal> GetFnBookBal(string accountNo);
        Task<decimal> GetFnAvailableBal(string accountNo);
        Task<List<VwAcctMasterStatement>> GetAcctMasterStatement(DateTime startDate, DateTime endDate, string accountno, bool printAll);
        Task<List<VwAcctMasterStatementMobileApp>> GetAcctMasterStatementMobileApp(DateTime startDate, DateTime endDate, string accountno, bool printAll);
        Task<int> TransLinesCopy(VwTransLinesCopy transLines);
        Task<int> TransApproval(ViewTransApproval transApproval);
        Task<VwAcctMasterBalance> GetAcctMasterBalance(string accountNo);
        Task<List<VwAccMaster>> GetAcctMasterSelectByCustID(string cusID);
        Task<VwCusMasterSelectCorrespondenceDetails> GetCusMasterSeelectCorrespondenceDetails(string cusID);
        Task<VwCusMaster> GetCusMasterSelectOneAsync(string cusID);
        Task<string> GenerateAccountNumber(string cusID, string branchCode, string prodCode);
        Task<string> ProdMasterAccount(string prodCode, string branchCode, string code);
        Task<int> GenerateRepaymentSchedule(VwRepaymentScheduleParam scheduleParam);
        Task<string> CreditTransSave(CreditTransSave creditTrans);
        Task<List<VwCreditMasterStatement>> CreditMasterStatement(string creditID, DateTime startDate, DateTime endDate);
        Task<string> RetailFundsTrfLocalSave(ViewRetailFundsTrfLocalSave fundTrans);
        Task<int> TransHeadersSave(VwTransHeaderSave transHeader);
        Task<int> MessagesLogSave(VwMessagesLogSave message);
        Task<int> SendMessageAlerts(string transID);
        Task<string> GenerateCreditAccountNumber(string operativeAccountNo, string prodCode, string branchCode, bool overdraft);
        Task<decimal> GetFnCreditBal(string accountNo);
    }
}
