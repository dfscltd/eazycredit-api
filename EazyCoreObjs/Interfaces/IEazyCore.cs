using EazyCoreObjs.OtherViewModels;
using EazyCoreObjs.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EazyCoreObjs.Interfaces
{
    public interface IEazyCore
    {
        Task<VwCusMaster> GetCusMasterByBvn(string bvn);
        Task<VwCusMaster> GetCusMasterByCusID(string cusID);
        Task<List<VwEntTels>> GetCusMasterPhoneNos(string cusID);
        Task<List<VwEntAddress>> GetCusMasterAddress(string cusID);
        Task<List<VwEntEmails>> GetCusMasterEmails(string cusID);
        Task<VwAccSMSNos> GetAccountSmsNo(string accountNo);
        Task<VwAccEmailAddr> GetAccountEmail(string accountNo);
        Task<VwAccMaster> GetCustomerAccountByAccountNo(string accountNo);
        Task<List<VwAccMaster>> GetCustomerAccountByName(string name);
        Task<List<VwAccMaster>> GetCustomerAccountByCusID(string cusID);
        Task<List<VwAccMaster>> GetAllAccountsOpenFromOutcess();
        Task<List<VwAccMaster>> GetCustomerAccountByAgentCode(string agentCode);
        Task<VwAcctMasterBalance> GetCustomerAccountBalance(string accountNo);
        Task<List<VwAcctMasterStatement>> GetCustomerAccountStatement(string accountNo, DateTime startDate, DateTime endDate);
        Task<List<VwAcctMasterStatementMobileApp>> GetCustomerAccountStatementMobileApp(string accountNo, DateTime startDate, DateTime endDate);
        //Response AccountTransfersLocal(ViewAccountTransfers req);
        Task<Response> AccountTransfersLocal(ViewAccountTransfers req);
        //Task<Response> AccountTransfersOtherBank(ViewInterBankTransfers req);
        //Task<Response> NIBBSNameEnquiry(ViewNameEnquirySingleRequest request);
        //Task<Response> NIPTSQTransEnquiry(ViewTransQuerySingleRequest request);
        Task<Response> AccountTransferSelf(ViewAccountTransfers req);
        Task<Response> FundTransferReversal(ViewTransferReversal req);
        Task<Response> LocalTransferReQuery(ViewTransferReversal req);
        Task<decimal> GetAccountAvailableBal(string accountNo);
        Task<decimal> GetAccountBookBal(string accountNo);
        Task<string> SendMessage(ViewSendMessage request);
        Task<Response> ProfileCustomer(ViewCusMaster record);
        Task<Response> NewAccountOpening(ViewNewAccountOpening record);
        Task<Response> AmountBlockRequest(ViewBlockAccountBal req);
        Task<Response> AmountUnBlockRequest(ViewBlockAccountBal req);
        Task<Response> AccountBlockRequest(ViewBlockAccountPostingStatus req);
        Task<Response> AccountUnBlockRequest(ViewBlockAccountPostingStatus req);
        Task<Response> PrmCardTypesLookup();
        Task<Response> AcctCardTransRequest(ViewAcctCardTransSave req);
        Task<Response> GetCardMaster(string accountNo);
        Task<Response> AcctCardHotListRequest(string req);
        Task<Response> ImageUpload(ViewAcctMandateSave req);
        Task<Response> DocumentUpload(ViewAcctMandateSave req);
        Task<Response> IdentityUpload(ViewIdentityUploadRequest req);
        Task<List<VwOrgBranches>> GetBankBranches();
        Task<List<VwOrgBranchesCordinate>> GetBankBranchesCordinate();
        Task<Response> DisputeTypesLookup();
        Task<Response> DisputeLogSave(ViewDisputeLogSave req);
        Task<Response> DisputeLogByRefNoAndAccount(string refNo, string accountNo);
        Task<Response> DisputeLogByRefNo(string refNo);
        Task<Response> DisputeLogDateRange(DateTime startDate, DateTime endDate);
        Task<List<VwProdMaster>> GetProdMaster();
        Task<Response> BankCodesLookup();
        Task<Response> SendMessageAlerts(string transID);
        Task<Response> InterbankListBankCodes();
        Task<bool> IsExistCusMaster(string cusID);
        Task<bool> IsExistAccount(string accountNo);
        Task<string> GetBeneficiaryAccountName(string sessionId, string accountNo, string transType);
        Task<string> SenderAccountName(string sessionId, string accountNo, string transType, long seqNo);
        Task<string> SenderAccountNumber(string sessionId, string accountNo, string transType, long seqNo);
        Task<Response> GetTransactionLine(string transID, string prodCode);
        Task<VwCreditMaster> GetCreditDetailByLoanId(string loanId);
        Task<VwAccMaster> GetAccountByLedgerCodeandBranch(string ledgerCode, string branchCode);
        Task<Response> Loanbooking(ViewLoanBooking record);
        Task<List<ViewChargeBaseCode>> GetChargeBaseCodeLookup();
    }
}
