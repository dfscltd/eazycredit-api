using EazyCoreObjs.Data;
using EazyCoreObjs.Interfaces;
using EazyCoreObjs.Models;
using EazyCoreObjs.OtherViewModels;
using EazyCoreObjs.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EazyCoreObjs.Repos
{
    public class EazyBaseRepos : IEazyCore
    {
        private readonly IConfiguration configuration;
        DbContextOptionsBuilder<EazyCoreContext> eazybank = new DbContextOptionsBuilder<EazyCoreContext>();

        private readonly IConfiguration imageConfiguration;
        DbContextOptionsBuilder<ImageContext> imageContext = new DbContextOptionsBuilder<ImageContext>();

        //private readonly INibbsRepos nibbs;
        private readonly IEazyBaseFuncAndProc funcAndProc;
        private readonly IOptions<ViewEazyCoreOptions> eazCoreOptions;

        public EazyBaseRepos(IConfiguration configuration, IConfiguration imageConfiguration, IEazyBaseFuncAndProc funcAndProc, IOptions<ViewEazyCoreOptions> eazCoreOptions)
        {
            this.configuration = configuration;
            this.imageConfiguration = imageConfiguration;
            //this.nibbs = nibbs;
            this.funcAndProc = funcAndProc;
            this.eazCoreOptions = eazCoreOptions;
        }

        public async Task<VwCusMaster> GetCusMasterByBvn(string bvn)
        {
            var result = new VwCusMaster();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwCusMaster.Where(x => x.BvnNo == bvn).SingleOrDefaultAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<VwCusMaster> GetCusMasterByCusID(string cusID)
        {
            var result = new VwCusMaster();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwCusMaster.Where(x => x.CusID == cusID).SingleOrDefaultAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<bool> IsExistCusMaster(string cusID)
        {
            bool result = false;
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwCusMaster.AnyAsync(x => x.CusID == cusID);
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwEntTels>> GetCusMasterPhoneNos(string cusID)
        {
            var result = new List<VwEntTels>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwEntTels.Where(x => x.EntityID == cusID).ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwEntAddress>> GetCusMasterAddress(string cusID)
        {
            var result = new List<VwEntAddress>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwEntAddress.Where(x => x.EntityID == cusID).ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwEntEmails>> GetCusMasterEmails(string cusID)
        {
            var result = new List<VwEntEmails>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwEntEmails.Where(x => x.EntityID == cusID).ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<VwAccSMSNos> GetAccountSmsNo(string accountNo)
        {
            var result = new VwAccSMSNos();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccSMSNos.Where(x => x.AccountNo == accountNo).Take(1).SingleOrDefaultAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<VwAccEmailAddr> GetAccountEmail(string accountNo)
        {
            var result = new VwAccEmailAddr();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccEmailAddr.Where(x => x.AccountNo == accountNo).Take(1).SingleOrDefaultAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<VwAccMaster> GetCustomerAccountByAccountNo(string accountNo) 
        {
            var result = new VwAccMaster();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccMaster.Where(x => x.AccountNo == accountNo || x.NetAccountNo == accountNo).SingleOrDefaultAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<VwAccMaster>> GetCustomerAccountByName(string name)
        {
            var result = new List<VwAccMaster>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccMaster.Where(x => name.Contains(x.AccountDesc) && x.AccountType == "4" && x.ProdCatCode == "1" || x.ProdCatCode == "2").OrderBy(x => x.AccountDesc).ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<VwAccMaster>> GetCustomerAccountByAgentCode(string agentCode)
        {
            var result = new List<VwAccMaster>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccMaster.Where(x => x.OldAccountNo.Equals(agentCode) && x.PostingDate > Convert.ToDateTime("2021-06-30")).ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwAccMaster>> GetAllAccountsOpenFromOutcess()
        {
            var result = new List<VwAccMaster>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccMaster.Where(x => x.AddedBy.Equals("System") && x.PostingDate > Convert.ToDateTime("2021-06-30")).ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwAccMaster>> GetCustomerAccountByCusID(string cusID)
        {
            var result = new List<VwAccMaster>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccMaster.Where(x => x.CusID == cusID).ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<VwAcctMasterBalance> GetCustomerAccountBalance(string accountNo)
        {
            var result = new VwAcctMasterBalance();
            try
            {

                
               result = await funcAndProc.GetAcctMasterBalance(accountNo);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwAcctMasterStatement>> GetCustomerAccountStatement(string accountNo, DateTime startDate, DateTime endDate)
        {
            var result = new List<VwAcctMasterStatement>();
            try
            {


                result = await funcAndProc.GetAcctMasterStatement(startDate, endDate, accountNo, false);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwAcctMasterStatementMobileApp>> GetCustomerAccountStatementMobileApp(string accountNo, DateTime startDate, DateTime endDate)
        {
            var result = new List<VwAcctMasterStatementMobileApp>();
            try
            {


                result = await funcAndProc.GetAcctMasterStatementMobileApp(startDate, endDate, accountNo, false);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Response> AccountTransfersLocal(ViewAccountTransfers req)
        {
            var transLines = new List<TransLine>();
            var postingDate = new PostingDates();
            var acctMaster = new AcctMaster();
            var prmRulesText = new PrmRulesText();
            var cusMaster = new CusMaster();
            var ledgerChart = new LedgerChart();
            var acctMasterCr = new AcctMaster();
            var transID = "";
            var contraAccountNo = "";
            decimal availableBal = 0;
            var prodMaster = new ProdMaster();
            var prodMasterCr = new ProdMaster();

            decimal commission = 0;

            int count = 0;

            try
            {

                if (string.IsNullOrEmpty(req.FromAccountNo))
                {
                    return new Response
                    {
                        ResponseCode = "400",
                        ResponseResult = "Bad Request: FromAccountNo is require"
                    };
                }

                if (string.IsNullOrEmpty(req.DestinationAccountNo))
                {
                    return new Response
                    {
                        ResponseCode = "400",
                        ResponseResult = "Bad Request: DestinationAccountNo is require"
                    };
                }

                //if (string.IsNullOrEmpty(req.TransferAmount.ToString()))
                //{
                //    return new Response
                //    {
                //        ResponseCode = "400",
                //        ResponseResult = "Bad Request: TransferAmount is require"
                //    };
                //}


                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    postingDate = await db.PostingDates.Where(x => x.PostingOn == true).SingleOrDefaultAsync();
                }

                if (postingDate == null)
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Request decline. Try again"
                    };
                }

                if(await IsTransRefExist(req.TransRef))
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Transaction duplication: Request decline"
                    };
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.FromAccountNo) || x.OldAccountNo.Equals(req.FromAccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
                }

                if (acctMaster == null)
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = $"{"The FromAccountNo - "}{req.FromAccountNo}{" is invalid"}"
                    };
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    cusMaster = await db.CusMaster.Where(x => x.CusId.Equals(acctMaster.CusId)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
                }

                if (acctMaster.PostStatus != "1")
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Account has PND"
                    };
                }

                if (acctMaster.AccountStatus != "1")
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Account is Inactive or Dormant"
                    };
                }

                var prodCatList = new List<string>() { "1", "2","Z" };

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    //availableBal = db.GetFnAvailableBal(req.FromAccountNo);
                    prodMaster = await db.ProdMaster.Where(x => prodCatList.Contains(x.ProdCatCode) && x.ProdCode == acctMaster.ProdCode).SingleOrDefaultAsync();
                }

                if (prodMaster == null) // not a retail account
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Invalid Account Number"
                    };
                }

                //if ((acctMaster.ProdCatCode == "3") || (acctMaster.ProdCatCode == "4") || (acctMaster.ProdCatCode == "5") || (acctMaster.ProdCatCode == "6") || (acctMaster.ProdCatCode == "7") || (acctMaster.ProdCatCode == "8")) // not a retail account
                //{
                //    return new Response
                //    {
                //        ResponseCode = "500",
                //        ResponseResult = "Invalid Account Number"
                //    };
                //}

 
                availableBal = await funcAndProc.GetFnAvailableBal(req.FromAccountNo);

                if (availableBal < (req.TransferAmount + commission) && (acctMaster.ProdCode != "ZZZ"))
                    return new Response { ResponseCode = "500", ResponseResult = "Insufficient balance" };  //insufficient balance


                if (string.IsNullOrEmpty(cusMaster.BvnNo) && acctMaster.AccountType.Equals("4"))
                    return new Response { ResponseCode = "500", ResponseResult = "Incomplete documentation - No BVN" }; //incomplete documentation - no BVN

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    acctMasterCr = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.DestinationAccountNo) || x.OldAccountNo.Equals(req.DestinationAccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
                }

                //var prodCatList = new List<string>() { "1", "2" };

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    //availableBal = db.GetFnAvailableBal(req.FromAccountNo);
                    prodMasterCr = await db.ProdMaster.Where(x => prodCatList.Contains(x.ProdCatCode) && x.ProdCode == acctMasterCr.ProdCode).SingleOrDefaultAsync();
                }

                if (prodMasterCr == null) // not a retail account
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Invalid Destination Account Number"
                    };
                }

                //if ((acctMasterCr.ProdCatCode == "6") || (acctMasterCr.AccountType == "0") ) // not a retail account
                //{
                //    return new Response
                //    {
                //        ResponseCode = "500",
                //        ResponseResult = "Invalid Destination Account Number"
                //    };
                //};

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    prmRulesText = await db.PrmRulesText.Where(x => x.Parameter.Equals(105)).SingleOrDefaultAsync();
                }

                if (string.IsNullOrEmpty(prmRulesText.Value))
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Incomplete setup for inter-bank transaction"
                    };
                }


                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    ledgerChart = await db.LedgerChart.Where(x => x.LedgerChartCode.Equals(prmRulesText.Value)).SingleOrDefaultAsync();
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    contraAccountNo = await db.AcctMaster.Where(x => x.AccountNo.Equals(prmRulesText.Value)).Select(x => x.AccountNo).SingleOrDefaultAsync();
                }

                if (ledgerChart == null && string.IsNullOrEmpty(contraAccountNo))
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Invalid GL setup for inter-bank transaction"
                    };
                }

                var accountNo = "";

                if (string.IsNullOrEmpty(contraAccountNo))
                {
                    accountNo = acctMaster.BranchCode + prmRulesText.Value;
                }
                else
                    accountNo = contraAccountNo;


                transID = await funcAndProc.GenerateNumberSequence(350); //specify the desire sequence number for transaction id generation

                //transaction header
                var transheaders = new TransHeader
                {
                    TransID = transID,
                    TransDesc = req.Remarks.Length > 90 ? req.Remarks.Substring(0,90) : req.Remarks,
                    CurrCode = acctMaster.CurrCode,
                    ExchRate = 1,
                    ModuleCode = "91",
                    ScreenCode = "00351",
                    BranchAdded = acctMaster.BranchCode,
                    PostingDateAdded = postingDate.PostingDate,
                    GLPostingDateAdded = postingDate.PostingDate,
                    ValueDate = postingDate.PostingDate,
                    EditMode = false,
                    TransStatus = "3",
                    Reversed = false,
                    Posted = false,
                    PostedGL = false,
                    PostedIB = false,
                    PostedPosAccts = false,
                    AddedBy = eazCoreOptions.Value.UserID,//"System",
                    DateAdded = DateTime.Now,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    WorkstationAdded = req.WorkstationAdded,
                    WorkstationIPAdded = "192.1.1.1",
                    BranchAddedApproved = acctMaster.BranchCode,
                    PostingDateApproved = postingDate.PostingDate,
                    AddedApprovedBy = eazCoreOptions.Value.UserID,//"System",
                    DateAddedApproved = DateTime.Now,
                    TimeAddedApproved = DateTime.Now.TimeOfDay,
                    WorkstationApproved = "",
                    WorkstationIPApproved = "",
                    ReversalReason = ""
                };

                //debit from originating account
                var transLinesDR = new TransLine
                {
                    SeqNo = 0,
                    AccountNo = req.FromAccountNo,
                    TransCode = "001",
                    UserNarrative = req.Remarks.Length > 90 ? req.Remarks.Substring(0, 90) : req.Remarks,
                    Debit = req.TransferAmount,
                    Credit = 0,
                    ContraAccountNo = acctMasterCr.AccountNo,
                    RefAccountNo = acctMasterCr.AccountNo,
                    OtherRefNo = req.TransRef,
                    TransID = transID,
                    DebitBaseCurr = req.TransferAmount,
                    CreditBaseCurr = 0,
                    DebitAcctCurr = req.TransferAmount,
                    CreditAcctCurr = 0,
                    AcctCurrCode = acctMaster.CurrCode,
                    AcctExchRate = 1,
                    ValueDate = postingDate.PostingDate,
                    AllocRuleCode = "999",
                    MISCode = "ZZZ"
                };

                transLines.Add(transLinesDR);

                //debit from originating account - transfer charges
                //var transLinesDRCom = new TransLine
                //{
                //    SeqNo = 0,
                //    AccountNo = req.FromAccountNo,
                //    TransCode = "001",
                //    UserNarrative = "Transfer Charges/" + req.Remarks,
                //    Debit = commission,
                //    Credit = 0,
                //    ContraAccountNo = req.DestinationAccountNo,
                //    RefAccountNo = req.DestinationAccountNo,
                //    OtherRefNo = req.TransRef,
                //    TransID = transID,
                //    DebitBaseCurr = commission,
                //    CreditBaseCurr = 0,
                //    DebitAcctCurr = commission,
                //    CreditAcctCurr = 0,
                //    AcctCurrCode = acctMaster.CurrCode,
                //    AcctExchRate = 1,
                //    ValueDate = postingDate.Date,
                //    AllocRuleCode = "999",
                //    MISCode = "ZZZ"
                //};

                //transLines.Add(transLinesDRCom);

                //credit to the benefinciary account, transfer main amount
                var transLinesCR = new TransLine
                {
                    SeqNo = 0,
                    AccountNo = acctMasterCr.AccountNo,
                    TransCode = "002",
                    UserNarrative = req.Remarks.Length > 90 ? req.Remarks.Substring(0, 90) : req.Remarks,
                    Debit = 0,
                    Credit = req.TransferAmount,
                    ContraAccountNo = req.FromAccountNo,
                    RefAccountNo = req.FromAccountNo,
                    OtherRefNo = req.TransRef,
                    TransID = transID,
                    DebitBaseCurr = 0,
                    CreditBaseCurr = req.TransferAmount,
                    DebitAcctCurr = 0,
                    CreditAcctCurr = req.TransferAmount,
                    AcctCurrCode = acctMaster.CurrCode,
                    AcctExchRate = 1,
                    ValueDate = postingDate.PostingDate,
                    AllocRuleCode = "999",
                    MISCode = "ZZZ"
                };

                transLines.Add(transLinesCR);

                //credit to the commission account - transfer commission
                //var transLinesCRCom = new TransLine
                //{
                //    SeqNo = 0,
                //    AccountNo = req.DestinationAccountNo,
                //    TransCode = "002",
                //    UserNarrative = "Transfer Charges/" + req.Remarks,
                //    Debit = 0,
                //    Credit = commission,
                //    ContraAccountNo = req.FromAccountNo,
                //    RefAccountNo = req.FromAccountNo,
                //    OtherRefNo = req.TransRef,
                //    TransID = transID,
                //    DebitBaseCurr = 0,
                //    CreditBaseCurr = commission,
                //    DebitAcctCurr = 0,
                //    CreditAcctCurr = commission,
                //    AcctCurrCode = acctMaster.CurrCode,
                //    AcctExchRate = 1,
                //    ValueDate = postingDate.Date,
                //    AllocRuleCode = "999",
                //    MISCode = "ZZZ"
                //};

                //transLines.Add(transLinesCRCom);

                //Pool PFI record
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync(transheaders);
                    await db.AddRangeAsync(transLines);
                    count = await db.SaveChangesAsync();

                    if(count > 0)
                        await funcAndProc.SendMessageAlerts(transID);
                }


                return new Response { ResponseCode = "200", ResponseResult = req.TransRef };

            }
            catch (Exception ex)
            {
                WriteLog(ex.InnerException.ToString());
                return new Response { ResponseCode = "500", ResponseResult = ex.Message };
            }
        }
        //public async Task<Response> AccountTransfersOtherBank(ViewInterBankTransfers req)
        //{
        //    var transLines = new List<TransLine>();
        //    var postingDate = new PostingDates();
        //    var acctMaster = new VwAccMaster(); 


        //    var prmRulesText = new PrmRulesText();
        //    //var NipPayableAcct = new PrmRulesText();
        //    var NipTransferChargeAcct = new PrmRulesText();
        //    var VatonNipTransferChargeAcct = new PrmRulesText();
        //    var dueToNibbsAcct = new PrmRulesText();


        //    var cusMaster = new CusMaster();
        //    var ledgerChart = new LedgerChart();
        //    var ledgerChartNIPCharge = new LedgerChart();
        //    var ledgerChartNIPVAT = new LedgerChart();
        //    var prodMaster = new ProdMaster();

        //    var transID = string.Empty;
        //    decimal availableBal = 0;

        //    var accountNo = string.Empty;
        //    var nipChargeGl = string.Empty;
        //    var vatChargeGl = string.Empty;
        //    var dueToNibbsGl = string.Empty;


        //    decimal commission = 0.0M;
        //    decimal vatOnCommission = 0.0M;
        //    double vatRate = 0.075;
        //    decimal dailyCummulativeAmount = 0.0M;
        //    decimal nIPFTOutwardDirect = 0.0M;

        //    int count = 0;

        //    string beneficiaryBankName = string.Empty;

        //    try
        //    {
        //        if (req.TransferAmount <= 5000)
        //        {
        //            commission = 10;
        //            vatOnCommission = commission * Convert.ToDecimal(vatRate);
        //        }

        //        if (req.TransferAmount > 5000 && req.TransferAmount <= 50000)
        //        {
        //            commission = 26;
        //            vatOnCommission = commission * Convert.ToDecimal(vatRate);
        //        }

        //        if (req.TransferAmount > 50000)
        //        {
        //            commission = 50;
        //            vatOnCommission = commission * Convert.ToDecimal(vatRate);
        //        }

        //        if (string.IsNullOrEmpty(req.FromAccountNo))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "400",
        //                ResponseResult = "Bad Request: FromAccountNo is require"
        //            };
        //        }

        //        if (string.IsNullOrEmpty(req.FromAccountNo))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "400",
        //                ResponseResult = "Bad Request: FromAccountNo is require"
        //            };
        //        }

        //        if (string.IsNullOrEmpty(req.TransferAmount.ToString()))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "400",
        //                ResponseResult = "Bad Request: TransferAmount is require"
        //            };
        //        }

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            postingDate = await db.PostingDates.Where(x => x.Record == 1).SingleOrDefaultAsync();
        //        }

        //        if (postingDate == null)
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "500",
        //                ResponseResult = "Request decline. Try again"
        //            };
        //        }

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            acctMaster = await db.VwAccMaster.Where(x => x.AccountNo.Equals(req.FromAccountNo) || x.OldAccountNo.Equals(req.FromAccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
        //        }

        //        if (string.IsNullOrEmpty(req.FromAccountNo))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "400",
        //                ResponseResult = "Bad Request: FromAccountNo is invalid"
        //            };
        //        }

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            nIPFTOutwardDirect = await db.NIPFTOutwardDirectCredit.Where(x => x.OriginatorAccountNumber == acctMaster.AccountNo && x.PostingDate == postingDate.PostingDate && x.Reversed == false).SumAsync(x => x.Amount);
        //        }

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            dailyCummulativeAmount = await db.PrmRulesNumbers.Where(x => x.Parameter == 164).Select(x => x.Value).SingleOrDefaultAsync();
        //        }

        //        if ((req.TransferAmount + nIPFTOutwardDirect) > dailyCummulativeAmount)
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = $"{"Daily cummulative other bank transfer limit of "}{string.Format("{0:C}", dailyCummulativeAmount)}{" exceeded"}"
        //            };
        //        }

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            acctMaster = await db.VwAccMaster.Where(x => x.AccountNo.Equals(req.FromAccountNo) || x.OldAccountNo.Equals(req.FromAccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
        //        }

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            cusMaster = await db.CusMaster.Where(x => x.CusId.Equals(acctMaster.CusID)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
        //        }

        //        if (acctMaster.PostStatus != "1")
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "500",
        //                ResponseResult = "Account has PND"
        //            };
        //        }

        //        if (acctMaster.AccountStatus != "1")
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "500",
        //                ResponseResult = "Account is Inactive or Dormant"
        //            };
        //        }

        //        //if ((acctMaster.ProdCatCode == "3") || (acctMaster.ProdCatCode == "4") || (acctMaster.ProdCatCode == "5") || (acctMaster.ProdCatCode == "6") || (acctMaster.ProdCatCode == "7") || (acctMaster.ProdCatCode == "8")) // not a retail account
        //        //{
        //        //return new Response
        //        //{
        //        //    ResponseCode = "500",
        //        //    ResponseResult = "Invalid Account Number"
        //        //};
        //        //}

        //        var prodCatList = new List<string>() { "1", "2" };

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            //availableBal = db.GetFnAvailableBal(req.FromAccountNo);
        //            prodMaster = await db.ProdMaster.Where(x => prodCatList.Contains(x.ProdCatCode) && x.ProdCode == acctMaster.ProdCode).SingleOrDefaultAsync();
        //        }

        //        if (prodMaster == null) // not a retail account
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "500",
        //                ResponseResult = "Invalid Account Number"
        //            };
        //        }

        //        availableBal = await funcAndProc.GetFnAvailableBal(acctMaster.AccountNo);

        //        if (availableBal < (req.TransferAmount + commission + vatOnCommission) && (acctMaster.ProdCatCode != "Z"))
        //            return new Response { ResponseCode = "500", ResponseResult = "Insufficient balance" };  //insufficient balance


        //        if (string.IsNullOrEmpty(cusMaster.BvnNo) && acctMaster.AccountType.Equals("4"))
        //            return new Response { ResponseCode = "500", ResponseResult = "Incomplete documentation - No BVN" }; //incomplete documentation - no BVN

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            prmRulesText = await db.PrmRulesText.Where(x => x.Parameter.Equals(915)).SingleOrDefaultAsync();
        //            NipTransferChargeAcct = await db.PrmRulesText.Where(x => x.Parameter.Equals(172)).SingleOrDefaultAsync();
        //            VatonNipTransferChargeAcct = await db.PrmRulesText.Where(x => x.Parameter.Equals(173)).SingleOrDefaultAsync();
        //            dueToNibbsAcct = await db.PrmRulesText.Where(x => x.Parameter.Equals(174)).SingleOrDefaultAsync();
        //        }

        //        if (string.IsNullOrEmpty(prmRulesText.Value))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "500",
        //                ResponseResult = "Incomplete setup for inter-bank transaction - NIP Payable GL not set"
        //            };
        //        }
        //        if (string.IsNullOrEmpty(NipTransferChargeAcct.Value))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = "Incomplete setup for inter-bank transaction - NIP transfer charge GL not set"
                        
        //            };
        //        }

        //        if (string.IsNullOrEmpty(VatonNipTransferChargeAcct.Value))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = "Incomplete setup for inter-bank transaction - VAT on NIP transfer charge GL not set" 
        //            };
        //        }

        //        if (await IsTransRefExist(req.TransRef))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "500",
        //                ResponseResult = "duplicate transaction"
        //            };
        //        }


        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            //ledgerChart = db.LedgerChart.Where(x => x.LedgerChartCode.Equals(prmRulesText.Value)).SingleOrDefault();
        //            accountNo = await db.AcctMaster.Where(x => x.AccountNo.Equals(prmRulesText.Value)).Select(x => x.AccountNo).SingleOrDefaultAsync();
        //            nipChargeGl = await db.AcctMaster.Where(x => x.AccountNo.Equals(NipTransferChargeAcct.Value)).Select(x => x.AccountNo).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
        //            vatChargeGl = await db.AcctMaster.Where(x => x.AccountNo.Equals(VatonNipTransferChargeAcct.Value)).Select(x => x.AccountNo).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation

        //        }

        //        if (string.IsNullOrEmpty(accountNo))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = "Invalid NIP Payable GL setup for inter-bank transaction "
        //            };
        //        }

        //        if (string.IsNullOrEmpty(nipChargeGl))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = "Invalid NIP transfer charge GL setup for inter-bank transaction "
        //            };
        //        }

        //        if (string.IsNullOrEmpty(vatChargeGl))
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = "Invalid NIP transfer VAT charge GL setup for inter-bank transaction "
        //            };
        //        }

        //        var nibssTrfSessionID = nibbs.SessionID().Result; // make a call to NIP 

        //        var trfSessionID = nibssTrfSessionID.Data.ToString();// JsonConvert.DeserializeObject<string>(nibssTrfSessionID.Data.ToString());

        //        transID = await funcAndProc.GenerateNumberSequence(2061); //specify the desire sequence number for transaction id generation

        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            beneficiaryBankName = await db.NIPFinancialInstitutions.Where(x => x.InstitutionCode.Equals(req.DestinationBankCode)).Select(x => x.InstitutionName).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
        //        }

        //        string remarks = req.Remarks.Length > 99 ? req.Remarks : req.Remarks + req.FromAccountName;

        //        //transaction header
        //        var transheaders = new TransHeader
        //        {
        //            TransID = transID,
        //            TransDesc = req.Remarks.Length > 90 ? req.Remarks.Substring(0, 90) : req.Remarks,
        //            CurrCode = acctMaster.CurrCode,
        //            ExchRate = 1,
        //            ModuleCode = "91",
        //            ScreenCode = "00351",
        //            BranchAdded = acctMaster.BranchCode,
        //            PostingDateAdded = postingDate.PostingDate,
        //            GLPostingDateAdded = postingDate.PostingDate,
        //            ValueDate = postingDate.PostingDate,
        //            EditMode = false,
        //            TransStatus = "3",
        //            Reversed = false,
        //            Posted = false,
        //            PostedGL = false,
        //            PostedIB = false,
        //            PostedPosAccts = false,
        //            AddedBy = eazCoreOptions.Value.UserID,//"System",
        //            DateAdded = DateTime.Now,
        //            TimeAdded = DateTime.Now.TimeOfDay,
        //            WorkstationAdded = "NIP",
        //            WorkstationIPAdded = "192.1.1.1",
        //            BranchAddedApproved = acctMaster.BranchCode,
        //            PostingDateApproved = postingDate.PostingDate,
        //            AddedApprovedBy = eazCoreOptions.Value.UserID,//"System",
        //            DateAddedApproved = DateTime.Now,
        //            TimeAddedApproved = DateTime.Now.TimeOfDay,
        //            WorkstationApproved = "",
        //            WorkstationIPApproved = "",
        //            ReversalReason = ""
        //        };

        //        //debit from originating account
        //        var transLinesDR = new TransLine
        //        {
        //            SeqNo = 0,
        //            AccountNo = acctMaster.AccountNo,
        //            TransCode = "867",
        //            UserNarrative = remarks.Length > 90 ? "NIP CR/" + remarks.Substring(0, 90) : "NIP CR/" + remarks,
        //            Debit = req.TransferAmount + commission + vatOnCommission,
        //            Credit = 0,
        //            ContraAccountNo = req.DestinationAccountNo,
        //            RefAccountNo = req.DestinationAccountNo,
        //            OtherRefNo = trfSessionID,
        //            TransID = transID,
        //            DebitBaseCurr = req.TransferAmount + commission + vatOnCommission,
        //            CreditBaseCurr = 0,
        //            DebitAcctCurr = req.TransferAmount + commission + vatOnCommission,
        //            CreditAcctCurr = 0,
        //            AcctCurrCode = acctMaster.CurrCode,
        //            AcctExchRate = 1,
        //            ValueDate = postingDate.PostingDate,
        //            AllocRuleCode = "999",
        //            MISCode = "ZZZ"
        //        };

        //        transLines.Add(transLinesDR);

        //        //debit from originating account - transfer charges
        //        //var transLinesDRCom = new TransLine
        //        //{
        //        //    SeqNo = 0,
        //        //    AccountNo = acctMaster.AccountNo,
        //        //    TransCode = "867",
        //        //    UserNarrative = "Transfer Charges/" + req.Remarks,
        //        //    Debit = commission,
        //        //    Credit = 0,
        //        //    ContraAccountNo = req.DestinationAccountNo,
        //        //    RefAccountNo = req.DestinationAccountNo,
        //        //    OtherRefNo = trfSessionID,
        //        //    TransID = transID,
        //        //    DebitBaseCurr = commission,
        //        //    CreditBaseCurr = 0,
        //        //    DebitAcctCurr = commission,
        //        //    CreditAcctCurr = 0,
        //        //    AcctCurrCode = acctMaster.CurrCode,
        //        //    AcctExchRate = 1,
        //        //    ValueDate = postingDate.PostingDate,
        //        //    AllocRuleCode = "999",
        //        //    MISCode = "ZZZ"
        //        //};

        //        //transLines.Add(transLinesDRCom);

        //        //debit from originating account -transfer charges
        //        //var transLinesDRVAT = new TransLine
        //        //{
        //        //    SeqNo = 0,
        //        //    AccountNo = acctMaster.AccountNo,
        //        //    TransCode = "867",
        //        //    UserNarrative = "VAT on NIP Charges/" + req.Remarks,
        //        //    Debit = vatOnCommission,
        //        //    Credit = 0,
        //        //    ContraAccountNo = req.DestinationAccountNo,
        //        //    RefAccountNo = vatChargeGl,
        //        //    OtherRefNo = trfSessionID,
        //        //    TransID = transID,
        //        //    DebitBaseCurr = vatOnCommission,
        //        //    CreditBaseCurr = 0,
        //        //    DebitAcctCurr = vatOnCommission,
        //        //    CreditAcctCurr = 0,
        //        //    AcctCurrCode = acctMaster.CurrCode,
        //        //    AcctExchRate = 1,
        //        //    ValueDate = postingDate.PostingDate,
        //        //    AllocRuleCode = "999",
        //        //    MISCode = "ZZZ"
        //        //};

        //        //transLines.Add(transLinesDRVAT);


        //        //credit to the benefinciary account, transfer main amount
        //        var transLinesCR = new TransLine
        //        {
        //            SeqNo = 0,
        //            AccountNo = accountNo, //req.DestinationAccountNo,
        //            TransCode = "868",
        //            UserNarrative = remarks.Length > 90 ? "TRF FRM/" + remarks.Substring(0, 90) : "TRF FRM/" + remarks,
        //            Debit = 0,
        //            Credit = req.TransferAmount,
        //            ContraAccountNo = acctMaster.AccountNo,
        //            RefAccountNo = acctMaster.AccountNo,
        //            OtherRefNo = trfSessionID,
        //            TransID = transID,
        //            DebitBaseCurr = 0,
        //            CreditBaseCurr = req.TransferAmount,
        //            DebitAcctCurr = 0,
        //            CreditAcctCurr = req.TransferAmount,
        //            AcctCurrCode = acctMaster.CurrCode,
        //            AcctExchRate = 1,
        //            ValueDate = postingDate.PostingDate,
        //            AllocRuleCode = "999",
        //            MISCode = "ZZZ"
        //        };

        //        transLines.Add(transLinesCR);

        //        //credit to the commission account - transfer commission
        //        var transLinesCRCom = new TransLine
        //        {
        //            SeqNo = 0,
        //            AccountNo = nipChargeGl, //req.DestinationAccountNo,
        //            TransCode = "868",
        //            UserNarrative = remarks.Length > 70 ? "NIP Charge on TRF FRM/" + remarks.Substring(0, 70) : "NIP Charge on TRF FRM/" + remarks,
        //            Debit = 0,
        //            Credit = commission,
        //            ContraAccountNo = acctMaster.AccountNo,
        //            RefAccountNo = acctMaster.AccountNo,
        //            OtherRefNo = trfSessionID,
        //            TransID = transID,
        //            DebitBaseCurr = 0,
        //            CreditBaseCurr = commission,
        //            DebitAcctCurr = 0,
        //            CreditAcctCurr = commission,
        //            AcctCurrCode = acctMaster.CurrCode,
        //            AcctExchRate = 1,
        //            ValueDate = postingDate.PostingDate,
        //            AllocRuleCode = "999",
        //            MISCode = "ZZZ"
        //        };

        //        transLines.Add(transLinesCRCom);

        //        //credit to the VAT account - VAT on transfer commission
        //        var transLinesCRVAT = new TransLine
        //        {
        //            SeqNo = 0,
        //            AccountNo = vatChargeGl,
        //            TransCode = "868",
        //            UserNarrative = remarks.Length > 70 ? "VAT on NIP Charge" + remarks.Substring(0, 70) : "VAT on NIP Charge" + remarks,
        //            Debit = 0,
        //            Credit = vatOnCommission,
        //            ContraAccountNo = acctMaster.AccountNo,
        //            RefAccountNo = acctMaster.AccountNo,
        //            OtherRefNo = trfSessionID,
        //            TransID = transID,
        //            DebitBaseCurr = 0,
        //            CreditBaseCurr = vatOnCommission,
        //            DebitAcctCurr = 0,
        //            CreditAcctCurr = vatOnCommission,
        //            AcctCurrCode = acctMaster.CurrCode,
        //            AcctExchRate = 1,
        //            ValueDate = postingDate.PostingDate,
        //            AllocRuleCode = "999",
        //            MISCode = "ZZZ"
        //        };
        //        transLines.Add(transLinesCRVAT);


        //        //if (!string.IsNullOrEmpty(dueToNibbsAcct.Value))
        //        //{
        //        //    using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        //    {
        //        //        dueToNibbsGl = db.AcctMaster.Where(x => x.AccountNo.Equals(dueToNibbsAcct.Value)).Select(x => x.AccountNo).SingleOrDefault();

        //        //    }

        //        //    if(!string.IsNullOrEmpty(dueToNibbsGl))
        //        //    {
        //        //        //debit to the commission account - transfer commission; the portion that is due to nibbs
        //        //        var transLinesDRChargeGL = new TransLine
        //        //        {
        //        //            SeqNo = 0,
        //        //            AccountNo = nipChargeGl, //req.DestinationAccountNo,
        //        //            TransCode = "867",
        //        //            UserNarrative = "due to nibbs/" + req.Remarks,
        //        //            Debit = commission/2,
        //        //            Credit = 0,
        //        //            ContraAccountNo = dueToNibbsGl,
        //        //            RefAccountNo = dueToNibbsGl,
        //        //            OtherRefNo = trfSessionID,
        //        //            TransID = transID,
        //        //            DebitBaseCurr = commission/2,
        //        //            CreditBaseCurr = 0,
        //        //            DebitAcctCurr = commission/2,
        //        //            CreditAcctCurr = 0,
        //        //            AcctCurrCode = acctMaster.CurrCode,
        //        //            AcctExchRate = 1,
        //        //            ValueDate = postingDate.PostingDate,
        //        //            AllocRuleCode = "999",
        //        //            MISCode = "ZZZ"
        //        //        };

        //        //        transLines.Add(transLinesDRChargeGL);

        //        //        var transLinesCRChargeGL = new TransLine
        //        //        {
        //        //            SeqNo = 0,
        //        //            AccountNo = dueToNibbsGl, //req.DestinationAccountNo,
        //        //            TransCode = "868",
        //        //            UserNarrative = "due to nibbs/" + req.Remarks,
        //        //            Debit = 0,
        //        //            Credit = commission/2,
        //        //            ContraAccountNo = nipChargeGl,
        //        //            RefAccountNo = nipChargeGl,
        //        //            OtherRefNo = trfSessionID,
        //        //            TransID = transID,
        //        //            DebitBaseCurr = 0,
        //        //            CreditBaseCurr = commission/2,
        //        //            DebitAcctCurr = 0,
        //        //            CreditAcctCurr = commission/2,
        //        //            AcctCurrCode = acctMaster.CurrCode,
        //        //            AcctExchRate = 1,
        //        //            ValueDate = postingDate.PostingDate,
        //        //            AllocRuleCode = "999",
        //        //            MISCode = "ZZZ"
        //        //        };

        //        //        transLines.Add(transLinesCRChargeGL);

        //        //    }
        //        //}


        //        //Pool PFI record
        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            await db.AddAsync(transheaders);
        //            await db.AddRangeAsync(transLines);
        //            count = await db.SaveChangesAsync();

        //            await funcAndProc.SendMessageAlerts(transID);
        //        }                

        //        var nibssTrf = new ViewFTSingleCreditRequest
        //        {
        //            FromAccountNo = acctMaster.AccountNo,
        //            FromAccountName = req.FromAccountName,
        //            FromAccountBvn = req.FromAccountBvn,

        //            BeneficiaryAccountNo = req.DestinationAccountNo,
        //            BeneficiaryAccountName = req.DestinationAccountName,
        //            BeneficiaryBvn = req.DestinationAccountNoBvn,
        //            BeneficiaryKYCLevel = req.DestinationAccountNoKYCLevel,

        //            DestinationBankCode = req.DestinationBankCode,
        //            TransferAmount = req.TransferAmount,
        //            CurrencyCode = req.CurrencyCode,
        //            Remarks = string.IsNullOrEmpty(req.Remarks) ? req.FromAccountName : remarks.Length > 99 ? remarks.Substring(0, 99) : remarks,
        //            TransRef = req.TransRef,
        //            ChannelCode = req.ChannelCode,
        //            NESessionID = req.NESessionID,
        //            SessionID = trfSessionID

        //        };

        //        var nibssTrfResponse = await nibbs.FTSingleCreditRequest(nibssTrf); // make a call to NIP 

        //        if(nibssTrfResponse.ResponseCode.Equals("00"))
        //        {
        //            var transferRequest = new NIPFTOutwardDirectCreditReq
        //            {
        //                SeqNo = 0,
        //                SessionID = trfSessionID,
        //                NameEnquiryRef = req.NESessionID,
        //                DestinationInstitutionCode = req.DestinationBankCode,
        //                ChannelCode = req.ChannelCode,
        //                BeneficiaryAccountName = req.DestinationAccountName,
        //                BeneficiaryAccountNumber = req.DestinationAccountNo,//responseDC.BeneficiaryAccountNumber,
        //                BeneficiaryBankVerificationNumber = req.DestinationAccountNoBvn,
        //                BeneficiaryKYCLevel = req.DestinationAccountNoKYCLevel,
        //                OriginatorAccountName = req.FromAccountName,
        //                OriginatorAccountNumber = req.FromAccountNo,
        //                OriginatorBankVerificationNumber = string.IsNullOrEmpty(req.FromAccountBvn) ? "" : req.FromAccountBvn,
        //                OriginatorKYCLevel = "",
        //                TransactionLocation = acctMaster.BranchName,
        //                Narration = req.Remarks,
        //                PaymentReference = req.TransRef,
        //                Amount = req.TransferAmount,
        //                ContraAccount = accountNo,
        //                TransferChargeAccount = string.IsNullOrEmpty(nipChargeGl) ? "" : nipChargeGl,
        //                VATonChargeAccount = string.IsNullOrEmpty(vatChargeGl) ? "" : vatChargeGl,
        //                TransferChargeAmount = commission,
        //                VATonChargeAmount = vatOnCommission,
        //                PostingDate = postingDate.PostingDate,
        //                DateAdded = DateTime.Now,
        //                Reversed = false,
        //                TSQStatus = "POSTED",
        //                RetryCount = 0
        //            };

        //            using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //            {
        //                await db.NIPFTOutwardDirectCredit.AddAsync(transferRequest);
        //                await db.SaveChangesAsync();
        //            }

        //            var transReceipt = new List<TransactionReceipt>();
        //            var date = DateTime.Now;
        //            var postingDateString = Convert.ToDateTime($"{new DateTime(date.Year, date.Month, date.Day).Date.Add(DateTime.Now.TimeOfDay):MM/dd/yyyy HH:mm:ss}"); //postingDate.ToString();


        //            var drReceipt = new TransactionReceipt
        //            {
        //                SeqNo = 0,
        //                AccountNumber = acctMaster.AccountNo,
        //                AccountName = acctMaster.AccountDesc,
        //                RefAccountNumber = req.DestinationAccountNo,
        //                RefAccountName = req.DestinationAccountName,
        //                TransactionAmount = req.TransferAmount,
        //                TransactionCurrency = req.CurrencyCode,
        //                TransactionNarration = req.Remarks,
        //                TransactionReference = trfSessionID,
        //                TransIndicator = "D",
        //                BeneficiaryBank = beneficiaryBankName,
        //                TransactionDate = postingDateString
        //                //Convert.ToDateTime($"{new DateTime().Date.Add(DateTime.Now.TimeOfDay):MM/dd/yyyy HH:mm:ss}")
        //                //Convert.ToDateTime(postingDateString)
        //                //Convert.ToDateTime(postingDate.ToLocalTime())
        //            };

        //            transReceipt.Add(drReceipt);

        //            var crReceipt = new TransactionReceipt
        //            {
        //                SeqNo = 0,
        //                AccountNumber = req.DestinationAccountNo,
        //                AccountName = req.DestinationAccountName,
        //                RefAccountNumber = acctMaster.AccountNo,
        //                RefAccountName = req.FromAccountName,
        //                TransactionAmount = req.TransferAmount,
        //                TransactionCurrency = req.CurrencyCode,
        //                TransactionNarration = req.Remarks,
        //                TransactionReference = trfSessionID,//req.TransRef,
        //                TransIndicator = "C",
        //                BeneficiaryBank = beneficiaryBankName,
        //                TransactionDate = postingDateString
        //                //Convert.ToDateTime($"{new DateTime().Date.Add(DateTime.Now.TimeOfDay):MM/dd/yyyy HH:mm:ss}")
        //                //DateTime.ParseExact(d, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
        //                //Convert.ToDateTime($"{ postingDate:d/M/yyyy} {DateTime.Now.TimeOfDay:HH:mm:ss}")

        //            };

        //            transReceipt.Add(crReceipt);

        //            //save receipt
        //            using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //            {
        //                await db.AddRangeAsync(transReceipt);
        //                await db.SaveChangesAsync();
        //            }


        //            return new Response
        //            {
        //                ResponseCode = "00",
        //                ResponseResult = $"{trfSessionID}"
        //            };
        //        }
        //        else
        //        {
        //            var transferRequest = new NIPFTOutwardDirectCreditReq
        //            {
        //                SeqNo = 0,
        //                SessionID = trfSessionID,
        //                NameEnquiryRef = req.NESessionID,
        //                DestinationInstitutionCode = req.DestinationBankCode,
        //                ChannelCode = req.ChannelCode,
        //                BeneficiaryAccountName = req.DestinationAccountName,
        //                BeneficiaryAccountNumber = req.DestinationAccountNo,//responseDC.BeneficiaryAccountNumber,
        //                BeneficiaryBankVerificationNumber = req.DestinationAccountNoBvn,
        //                BeneficiaryKYCLevel = req.DestinationAccountNoKYCLevel,
        //                OriginatorAccountName = req.FromAccountName,
        //                OriginatorAccountNumber = req.FromAccountNo,
        //                OriginatorBankVerificationNumber = string.IsNullOrEmpty(req.FromAccountBvn) ? "" : req.FromAccountBvn,
        //                OriginatorKYCLevel = "",
        //                TransactionLocation = acctMaster.BranchName,
        //                Narration = req.Remarks,
        //                PaymentReference = req.TransRef,
        //                Amount = req.TransferAmount,
        //                ContraAccount = accountNo,
        //                TransferChargeAccount = string.IsNullOrEmpty(nipChargeGl) ? "" : nipChargeGl,
        //                VATonChargeAccount = string.IsNullOrEmpty(vatChargeGl) ? "" : vatChargeGl,
        //                TransferChargeAmount = commission,
        //                VATonChargeAmount = vatOnCommission,
        //                PostingDate = postingDate.PostingDate,
        //                DateAdded = DateTime.Now,
        //                Reversed = false,
        //                TSQStatus = "WAITING",
        //                RetryCount = 0
        //            };

        //            using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //            {
        //                await db.NIPFTOutwardDirectCredit.AddAsync(transferRequest);
        //                await db.SaveChangesAsync();
        //            }

        //            var transReceipt = new List<TransactionReceipt>();
        //            var date = DateTime.Now;
        //            var postingDateString = Convert.ToDateTime($"{new DateTime(date.Year, date.Month, date.Day).Date.Add(DateTime.Now.TimeOfDay):MM/dd/yyyy HH:mm:ss}"); //postingDate.ToString();


        //            var drReceipt = new TransactionReceipt
        //            {
        //                SeqNo = 0,
        //                AccountNumber = acctMaster.AccountNo,
        //                AccountName = acctMaster.AccountDesc,
        //                RefAccountNumber = req.DestinationAccountNo,
        //                RefAccountName = req.DestinationAccountName,
        //                TransactionAmount = req.TransferAmount,
        //                TransactionCurrency = req.CurrencyCode,
        //                TransactionNarration = req.Remarks,
        //                TransactionReference = trfSessionID,
        //                TransIndicator = "D",
        //                BeneficiaryBank = beneficiaryBankName,
        //                TransactionDate = postingDateString
        //                //Convert.ToDateTime($"{new DateTime().Date.Add(DateTime.Now.TimeOfDay):MM/dd/yyyy HH:mm:ss}")
        //                //Convert.ToDateTime(postingDateString)
        //                //Convert.ToDateTime(postingDate.ToLocalTime())
        //            };

        //            transReceipt.Add(drReceipt);

        //            var crReceipt = new TransactionReceipt
        //            {
        //                SeqNo = 0,
        //                AccountNumber = req.DestinationAccountNo,
        //                AccountName = req.DestinationAccountName,
        //                RefAccountNumber = acctMaster.AccountNo,
        //                RefAccountName = req.FromAccountName,
        //                TransactionAmount = req.TransferAmount,
        //                TransactionCurrency = req.CurrencyCode,
        //                TransactionNarration = req.Remarks,
        //                TransactionReference = trfSessionID,//req.TransRef,
        //                TransIndicator = "C",
        //                BeneficiaryBank = beneficiaryBankName,
        //                TransactionDate = postingDateString
        //                //Convert.ToDateTime($"{new DateTime().Date.Add(DateTime.Now.TimeOfDay):MM/dd/yyyy HH:mm:ss}")
        //                //DateTime.ParseExact(d, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
        //                //Convert.ToDateTime($"{ postingDate:d/M/yyyy} {DateTime.Now.TimeOfDay:HH:mm:ss}")

        //            };

        //            transReceipt.Add(crReceipt);

        //            //save receipt
        //            using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //            {
        //                await db.AddRangeAsync(transReceipt);
        //                await db.SaveChangesAsync();
        //            }

        //            return new Response
        //            {
        //                ResponseCode = "00",
        //                ResponseResult = $"{trfSessionID}"
        //            };

        //        }
             
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response { ResponseCode = "500", ResponseResult = ex.Message };
        //    }
        //}
        //public async Task<Response> NIBBSNameEnquiry(ViewNameEnquirySingleRequest request)
        //{
        //    var data = new ViewNESingleRequest
        //    {
        //        AccountNo = request.AccountNo,
        //        DestinationCode = request.DestinationCode,
        //        ChannelCode = request.ChannelCode
        //    };
        //    try
        //    {
        //        var response = await nibbs.NESingleRequest(data);

        //        if (response.ResponseCode == "00")
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "00",
        //                ResponseResult = response.Data
        //            };
        //        }
        //        else
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = response.Error
        //            };
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResponseCode = "01",
        //            ResponseResult = ex.Message
        //        };
        //    }
        //}
        //public async Task<Response> NIPTSQTransEnquiry(ViewTransQuerySingleRequest request)
        //{
        //    var data = new ViewTSQuerySingleRequest
        //    {
        //        SessionID = request.SessionID,
        //        ChannelCode = request.ChannelCode
        //    };
        //    try
        //    {
        //        var response = await nibbs.TSQuerySingleRequest(data);

        //        var trfSession = JsonConvert.DeserializeObject<ViewTSQuerySingleResponse>(response.Data.ToString());


        //        if (trfSession.ResponseCode == "00")
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "00",
        //                ResponseResult = trfSession
        //            };
        //        }
        //        else
        //        {
        //            return new Response
        //            {
        //                ResponseCode = "01",
        //                ResponseResult = response.Error
        //            };
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response
        //        {
        //            ResponseCode = "01",
        //            ResponseResult = ex.Message
        //        };
        //    }




        //}
        public async Task<Response> AccountTransferSelf(ViewAccountTransfers req)
        {
            var transLines = new List<TransLine>();
            var postingDate = DateTime.Now.Date;
            var acctMaster = new VwAccMaster();
            var prmRulesText = new PrmRulesText();
            var cusMaster = new CusMaster();
            var ledgerChart = new LedgerChart();
            var transID = "";
            var contraAccountNo = "";
            decimal availableBal = 0;

            decimal commission = 0;

            int count = 0;

            try
            {

                if (string.IsNullOrEmpty(req.FromAccountNo))
                {
                    return new Response
                    {
                        ResponseCode = "400",
                        ResponseResult = "Bad Request: FromAccountNo is require"
                    };
                }

                if (string.IsNullOrEmpty(req.FromAccountNo))
                {
                    return new Response
                    {
                        ResponseCode = "400",
                        ResponseResult = "Bad Request: FromAccountNo is require"
                    };
                }

                if (string.IsNullOrEmpty(req.TransferAmount.ToString()))
                {
                    return new Response
                    {
                        ResponseCode = "400",
                        ResponseResult = "Bad Request: TransferAmount is require"
                    };
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    postingDate = await db.PostingDates.Where(x => x.PostingOn == true).Select(x => x.PostingDate).SingleOrDefaultAsync();
                }

                if (postingDate == null)
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Request decline. Try again"
                    };
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    acctMaster = await db.VwAccMaster.Where(x => x.AccountNo.Equals(req.FromAccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    cusMaster = await db.CusMaster.Where(x => x.CusId.Equals(acctMaster.CusID)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
                }

                if (acctMaster.PostStatus != "1")
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Account has PND"
                    };
                }

                if (acctMaster.AccountStatus != "1")
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Account is Inactive or Dormant"
                    };
                }

                if ((acctMaster.ProdCatCode == "3") || (acctMaster.ProdCatCode == "4") || (acctMaster.ProdCatCode == "5") || (acctMaster.ProdCatCode == "6") || (acctMaster.ProdCatCode == "7") || (acctMaster.ProdCatCode == "8")) // not a retail account
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Invalid Account Number"
                    };
                }


                availableBal = await funcAndProc.GetFnAvailableBal(req.FromAccountNo);

                if (availableBal < (req.TransferAmount + commission) && (acctMaster.ProdCatCode != "Z"))
                    return new Response { ResponseCode = "500", ResponseResult = "Insufficient balance" };  //insufficient balance


                if (string.IsNullOrEmpty(cusMaster.BvnNo) && acctMaster.AccountType.Equals("4"))
                    return new Response { ResponseCode = "500", ResponseResult = "Incomplete documentation - No BVN" }; //incomplete documentation - no BVN

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    prmRulesText = await db.PrmRulesText.Where(x => x.Parameter.Equals(105)).SingleOrDefaultAsync();
                }

                if (string.IsNullOrEmpty(prmRulesText.Value))
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Incomplete setup for inter-bank transaction"
                    };
                }


                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    ledgerChart = await db.LedgerChart.Where(x => x.LedgerChartCode.Equals(prmRulesText.Value)).SingleOrDefaultAsync();
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    contraAccountNo = await db.AcctMaster.Where(x => x.AccountNo.Equals(prmRulesText.Value)).Select(x => x.AccountNo).SingleOrDefaultAsync();
                }

                if (ledgerChart == null && string.IsNullOrEmpty(contraAccountNo))
                {
                    return new Response
                    {
                        ResponseCode = "500",
                        ResponseResult = "Invalid GL setup for inter-bank transaction"
                    };
                }

                var accountNo = "";

                if (string.IsNullOrEmpty(contraAccountNo))
                {
                    accountNo = acctMaster.BranchCode + prmRulesText.Value;
                }
                else
                    accountNo = contraAccountNo;


                transID = await funcAndProc.GenerateNumberSequence(350); //specify the desire sequence number for transaction id generation

                //transaction header
                var transheaders = new TransHeader
                {
                    TransID = transID,
                    TransDesc = req.Remarks.Length > 90 ? req.Remarks.Substring(0, 90) : req.Remarks,
                    CurrCode = acctMaster.CurrCode,
                    ExchRate = 1,
                    ModuleCode = "91",
                    ScreenCode = "00351",
                    BranchAdded = acctMaster.BranchCode,
                    PostingDateAdded = postingDate.Date,
                    GLPostingDateAdded = postingDate.Date,
                    ValueDate = postingDate.Date,
                    EditMode = false,
                    TransStatus = "3",
                    Reversed = false,
                    Posted = false,
                    PostedGL = false,
                    PostedIB = false,
                    PostedPosAccts = false,
                    AddedBy = eazCoreOptions.Value.UserID,//"System",
                    DateAdded = DateTime.Now,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    WorkstationAdded = req.WorkstationAdded,
                    WorkstationIPAdded = "192.1.1.1",
                    BranchAddedApproved = acctMaster.BranchCode,
                    PostingDateApproved = postingDate.Date,
                    AddedApprovedBy = eazCoreOptions.Value.UserID,//"System",
                    DateAddedApproved = DateTime.Now,
                    TimeAddedApproved = DateTime.Now.TimeOfDay,
                    WorkstationApproved = "",
                    WorkstationIPApproved = "",
                    ReversalReason = ""
                };

                //debit from originating account
                var transLinesDR = new TransLine
                {
                    SeqNo = 0,
                    AccountNo = req.FromAccountNo,
                    TransCode = "001",
                    UserNarrative = req.Remarks.Length > 90 ? req.Remarks.Substring(0, 90) : req.Remarks,
                    Debit = req.TransferAmount,
                    Credit = 0,
                    ContraAccountNo = req.DestinationAccountNo,
                    RefAccountNo = req.DestinationAccountNo,
                    OtherRefNo = req.TransRef,
                    TransID = transID,
                    DebitBaseCurr = req.TransferAmount,
                    CreditBaseCurr = 0,
                    DebitAcctCurr = req.TransferAmount,
                    CreditAcctCurr = 0,
                    AcctCurrCode = acctMaster.CurrCode,
                    AcctExchRate = 1,
                    ValueDate = postingDate.Date,
                    AllocRuleCode = "999",
                    MISCode = "ZZZ"
                };

                transLines.Add(transLinesDR);

                ////debit from originating account - transfer charges
                //var transLinesDRCom = new TransLine
                //{
                //    SeqNo = 0,
                //    AccountNo = req.FromAccountNo,
                //    TransCode = "001",
                //    UserNarrative = "Transfer Charges/" + req.Remarks,
                //    Debit = commission,
                //    Credit = 0,
                //    ContraAccountNo = req.DestinationAccountNo,
                //    RefAccountNo = req.DestinationAccountNo,
                //    OtherRefNo = req.TransRef,
                //    TransID = transID,
                //    DebitBaseCurr = commission,
                //    CreditBaseCurr = 0,
                //    DebitAcctCurr = commission,
                //    CreditAcctCurr = 0,
                //    AcctCurrCode = acctMaster.CurrCode,
                //    AcctExchRate = 1,
                //    ValueDate = postingDate.Date,
                //    AllocRuleCode = "999",
                //    MISCode = "ZZZ"
                //};

                //transLines.Add(transLinesDRCom);

                //credit to the benefinciary account, transfer main amount
                var transLinesCR = new TransLine
                {
                    SeqNo = 0,
                    AccountNo = req.DestinationAccountNo,
                    TransCode = "002",
                    UserNarrative = req.Remarks.Length > 90 ? req.Remarks.Substring(0, 90) : req.Remarks,
                    Debit = 0,
                    Credit = req.TransferAmount,
                    ContraAccountNo = req.FromAccountNo,
                    RefAccountNo = req.FromAccountNo,
                    OtherRefNo = req.TransRef,
                    TransID = transID,
                    DebitBaseCurr = 0,
                    CreditBaseCurr = req.TransferAmount,
                    DebitAcctCurr = 0,
                    CreditAcctCurr = req.TransferAmount,
                    AcctCurrCode = acctMaster.CurrCode,
                    AcctExchRate = 1,
                    ValueDate = postingDate.Date,
                    AllocRuleCode = "999",
                    MISCode = "ZZZ"
                };

                transLines.Add(transLinesCR);

                ////credit to the commission account - transfer commission
                //var transLinesCRCom = new TransLine
                //{
                //    SeqNo = 0,
                //    AccountNo = req.DestinationAccountNo,
                //    TransCode = "002",
                //    UserNarrative = "Transfer Charges/" + req.Remarks,
                //    Debit = 0,
                //    Credit = commission,
                //    ContraAccountNo = req.FromAccountNo,
                //    RefAccountNo = req.FromAccountNo,
                //    OtherRefNo = req.TransRef,
                //    TransID = transID,
                //    DebitBaseCurr = 0,
                //    CreditBaseCurr = commission,
                //    DebitAcctCurr = 0,
                //    CreditAcctCurr = commission,
                //    AcctCurrCode = acctMaster.CurrCode,
                //    AcctExchRate = 1,
                //    ValueDate = postingDate.Date,
                //    AllocRuleCode = "999",
                //    MISCode = "ZZZ"
                //};

                //transLines.Add(transLinesCRCom);

                //Pool PFI record
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync(transheaders);
                    await db.AddRangeAsync(transLines);
                    count = await db.SaveChangesAsync();
                    if(count > 0)
                        await funcAndProc.SendMessageAlerts(transID);
                }
                return new Response { ResponseCode = "200", ResponseResult = transID };

            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "500", ResponseResult = ex.Message };
            }
        }
        public async Task<Response> FundTransferReversal(ViewTransferReversal req)
        {
            var transLine = new TransLine();
            var trans = new TransHeader();
            string transID = "";
            int result = 0;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                transLine =  await db.TransLine.Where(x => x.OtherRefNo == req.ReferenceNo).Take(1).SingleOrDefaultAsync();
            }

            if(transLine == null)
            {
                return new Response
                {
                    ResponseCode = "400",
                    ResponseResult = $"Invalid reference number - {req.ReferenceNo}"
                };
            }

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                trans = await db.TransHeader.Where(x => x.TransID == transLine.TransID).SingleOrDefaultAsync();
            }

            if (trans.Reversed)
            {
                return new Response
                {
                    ResponseCode = "400",
                    ResponseResult = $"Transaction already reversed"
                };
            }

            transID = await funcAndProc.GenerateNumberSequence(350);
            
            var transHeader = new VwTransHeaderSave
            {
                TransID = transID,
                TransDesc = "RSVL - " + transLine.UserNarrative.ToUpper(),
                CurrCode = transLine.AcctCurrCode,
                ExchRate = 1,
                ModuleCode = "00",
                ScreenCode = "00354", // transaction reversals
                AddedBy = eazCoreOptions.Value.UserID,//"System",
                BranchAdded = "000",
                ValueDate = transLine.ValueDate,
                TransStatus = "1",
                WorkstationAdded = "system",
                WorkstationIPAdded = "192.168.1.1"
            };

            result = await funcAndProc.TransHeadersSave(transHeader);


            if (result >= 1)
            {
                var reversal = new VwTransLinesCopy()
                {
                    TransIdFrom = transLine.TransID,
                    TransIdTo = transID,
                    CopyOption = "02",
                    ClearBatch = true,
                    Charge = 0,
                    ProdCode = "",
                    Narration = req.Narration
                };


                await funcAndProc.TransLinesCopy(reversal);

                var transApproval = new ViewTransApproval()
                {
                    TransID = transID,
                    ScreenCode = "00351",
                    AddedBy = eazCoreOptions.Value.UserID,//"System",
                    ApprovedBy = eazCoreOptions.Value.UserID,//"System",
                    Approved = true,
                    ApproveRejectionReason = "Ok",
                    BranchAddedApproved = "000",
                    WorkstationApproved = "",
                    WorkstationIPApproved = "192.168.1.1",
                    TransCode = "001",
                    Reason = "REVERSAL"
                };

                await funcAndProc.TransApproval(transApproval);
                await funcAndProc.SendMessageAlerts(transID);

                    //var transHeaderReversal = db.TransHeader.Where(x => x.TransID.Equals(transLine.TransID)).SingleOrDefault();

                    //transHeaderReversal.Reversed = true;

                    //var local = db.Set<TransHeader>()
                    //                    .Local
                    //                    .FirstOrDefault(entry => entry.TransID.Equals(transLine.TransID));

                    //if (local != null)
                    //{

                    //    db.Entry(local).State = EntityState.Detached;
                    //}

                    //db.TransHeader.Update(transHeaderReversal);//.Property(x => x.CardseqNo).IsModified = false;
                    //db.SaveChanges();



                var record = new Response
                {
                    ResponseResult = req.ReferenceNo,
                    ResponseCode = "00"
                };

                return record;

            }
            else
                return new Response
                {
                    ResponseResult = "",
                    ResponseCode = "01"
                };
        }
        public async Task<Response> LocalTransferReQuery(ViewTransferReversal req)
        {
            bool transLine = false;
            var trans = new TransHeader();
            string transID = "";
            int result = 0;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                transLine = await db.VwTransLines.AnyAsync(x => x.OtherRefNo == req.ReferenceNo && x.EditMode == false && x.TransStatus == "3" && x.Reversed == false);
            }

            if (transLine)
            {
                return new Response
                {
                    ResponseResult = req.ReferenceNo,
                    ResponseCode = "00"
                };
            }
            else
            {
                return new Response
                {
                    ResponseResult = req.ReferenceNo,
                    ResponseCode = "01"
                };
            }
        }
        public async Task<bool> IsExistAccount(string accountNo)
        {
            bool result = false;
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.AcctMaster.AnyAsync(x => x.AccountNo == accountNo || x.NetAccountNo == accountNo);
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<decimal> GetAccountAvailableBal(string accountNo)
        {
            decimal result = 0;
            try
            {

                result = await funcAndProc.GetFnAvailableBal(accountNo);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<decimal> GetAccountBookBal(string accountNo)
        {
            decimal result = 0;
            try
            {


                result = await funcAndProc.GetFnBookBal(accountNo);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<string> SendMessage(ViewSendMessage request)
        {
            var accountSMS = await GetAccountSmsNo(request.AccountNumber);
            var accountemail = await GetAccountEmail(request.AccountNumber);

            var record = new VwMessagesLogSave
            {
                Sequence = 0,
                MessageText = request.Message,
                AccountNo = request.AccountNumber,
                PhoneNo = accountSMS == null ? "" : accountSMS.SMSNo,
                EmailAddr = accountemail == null ? "" : accountemail.EmailAddr,
                MessageType = request.MessageType,
                TransID = request.TransID,
                TransLineNo = 0,
                TransAmount = request.TransAmount,
                AvailBalance = request.AvailBalance
            };

            try
            {

                await funcAndProc.MessagesLogSave(record);               

                return "00";
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<List<ViewPrmCurrencies>> GetCurrencies()
        {
            var result = new List<PrmCurrencies>();

            var response = new List<ViewPrmCurrencies>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.PrmCurrencies.ToListAsync();
                }

                foreach(var record in result)
                {
                    response.Add(new ViewPrmCurrencies
                    {
                        CurrCode = record.CurrCode,
                        CurrDesc = record.CurrDesc
                    });
                }

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Response> ProfileCustomer(ViewCusMaster record)
        {
            var postingDate = new DateTime().Date;

            var customerID = "";

            try
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    postingDate = db.PostingDates.Where(x => x.PostingOn == true).Select(x => x.PostingDate).SingleOrDefault();
                }


                customerID = await funcAndProc.GenerateNumberSequence(1001);
                var customer = new CusMaster
                {
                    CusId = customerID,
                    NetcusId = record.NetcusId,
                    CusName = record.CusName,
                    FirstName = record.FirstName.ToUpper(),
                    MiddleName = record.MiddleName.ToUpper(),
                    LastName = record.LastName.ToUpper(),
                    BranchCode = record.BranchAdded,
                    CusType = record.CusType,
                    Title = record.Title,
                    Gender = record.Gender,
                    MaritalStatus = record.MaritalStatus,
                    Religion = record.Religion,
                    CountryCode = record.CountryCode,
                    StateCode = record.StateCode,
                    LocalGovtCode = record.LocalGovtCode,
                    BirthDate = record.BirthDate,
                    BirthPlace = record.BirthPlace,
                    EmployStatus = record.EmployStatus,
                    OccCode = record.OccCode,
                    MotherMaidName = record.MotherMaidName,
                    IncomeBracketCode = record.IncomeBracketCode,
                    SectorCode = record.SectorCode,
                    BusinessType = record.BusinessType,
                    CertificateNo = record.CertificateNo,
                    CertificateDate = record.CertificateDate,
                    CertificateIssuer = record.CertificateIssuer,
                    AuthCapital = record.AuthCapital,
                    PaidUpCapital = record.PaidUpCapital,
                    Reserves = record.Reserves,
                    UnappProfitLoss = record.UnappProfitLoss,
                    FinYearEnd = record.FinYearEnd,
                    LastAuditDate = record.LastAuditDate,
                    BoardSize = record.BoardSize,
                    BranchAdded = record.BranchAdded,
                    CusStatus = record.CusStatus,
                    Insider = record.Insider,
                    InsiderType = record.InsiderType,
                    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy, //record.LastModifiedBy,
                    PostingDateAdded = postingDate.Date,
                    LastApprovedBy = eazCoreOptions.Value.UserID,//record.AddedBy,//record.AddedApprovedBy,
                    BranchLastApproved = record.BranchAdded,
                    DateAdded = DateTime.Now,
                    PostingDateLastModified = postingDate.Date,
                    DateLastModified = DateTime.Now,
                    DateLastApproved = DateTime.Now,
                    PostingDateLastApproved = postingDate.Date,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    TimeLastModified = DateTime.Now.TimeOfDay,
                    TimeLastApproved = DateTime.Now.TimeOfDay,
                    TinNo = record.TinNo,
                    BvnNo = record.BvnNo,
                    OwnershipCode = record.OwnershipCode,
                    CreditRiskCode = record.CreditRiskCode,//record.CreditRiskCode,
                    ComplianceRiskCode = record.ComplianceRiskCode,
                    Pep = record.Pep,
                    RatingAgencyID = record.RatingAgencyID,
                    Rating = record.Rating,
                    RatingOrigination = record.RatingOrigination,
                    FinInstTypeCode = record.FinInstTypeCode
                };

                var customerTelephone = new EntTels
                {
                    Sequence = 0,
                    EntityID = customerID,
                    EntityTypeCode = "01",
                    TelTypeCode = "01",
                    TelNo = record.EntTels.TelNo,
                    Extension = "",
                    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    DateAdded = DateTime.Now.Date,
                    DateLastModified = DateTime.Now.Date,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    TimeLastModified = DateTime.Now.TimeOfDay,
                    Remarks = record.FirstName
                };

                var customerEmail = new EntEmails
                {
                    Sequence = 0,
                    EntityID = customerID,
                    EntityTypeCode = "01",
                    EmailAddr = record.EmailAddress,
                    PrimaryAddr = true,
                    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    DateAdded = DateTime.Now.Date,
                    DateLastModified = DateTime.Now.Date,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    TimeLastModified = DateTime.Now.TimeOfDay,
                    Remarks = record.FirstName
                };

                var identification = new EntIdentities
                {
                    Sequence = 0,
                    EntityID = customerID,
                    EntityTypeCode = "01",
                    IDTypeCode = record.Identification.IDTypeCode,
                    IDNo = record.Identification.IDNo,
                    Issuer = record.Identification.Issuer,
                    ExpiryDate = string.IsNullOrEmpty(record.Identification.ExpiryDate.ToString()) ? null : record.Identification.ExpiryDate,
                    PrimaryID = true,
                    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    LastmodifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    DateAdded = DateTime.Now.Date,
                    LastDateModified = DateTime.Now.Date,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    LastTimeModified = DateTime.Now.TimeOfDay,
                    IssueDate = string.IsNullOrEmpty(record.Identification.IssueDate.ToString()) ? null : record.Identification.IssueDate
                };

                CusRelParties nextOfKin = record.CusRelParty == null ? null : new CusRelParties
                {
                    Sequence = 0,
                    CusID = customerID,
                    CusRelTypeCode = record.CusRelParty.CusRelTypeCode,
                    SurName = record.CusRelParty.SurName,
                    FirstName = record.CusRelParty.FirstName,
                    MiddleName = record.CusRelParty.MiddleName,
                    Title = record.CusRelParty.Title,
                    Gender = record.CusRelParty.Gender,
                    MaritalStatus = record.CusRelParty.MaritalStatus,
                    CountryCode = record.CusRelParty.CountryCode,
                    StateCode = record.CusRelParty.StateCode,
                    LocalGovtCode = record.CusRelParty.LocalGovtCode,
                    BirthDate = record.CusRelParty.BirthDate,
                    BirthPlace = record.CusRelParty.BirthPlace,
                    EmployStatus = record.CusRelParty.EmployStatus,
                    OccCode = record.CusRelParty.OccCode,
                    PostingDateAdded = record.PostingDateAdded,
                    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                    DateAdded = DateTime.Now.Date,
                    PostingDateLastModified = record.PostingDateAdded,
                    DateLastModified = DateTime.Now.Date,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    TimeLastModified = DateTime.Now.TimeOfDay,
                    BvnNo = record.CusRelParty.BvnNo,
                    PEP = record.CusRelParty.PEP
                };
                CusAddress address = record.CusAddress == null ?  null :
                    new CusAddress
                    {
                        Sequence = 0,
                        EntityID = customerID,
                        EntityTypeCode = "01",
                        AddrTypeCode = record.CusAddress.AddrTypeCode,
                        Street = record.CusAddress.Street,
                        CountryCode = record.CusAddress.CountryCode,
                        StateCode = record.CusAddress.StateCode,
                        LocalGovtCode = record.CusAddress.LocalGovtCode,
                        City = record.CusAddress.City,
                        PostalCode = record.CusAddress.PostalCode,
                        Dispatch = record.CusAddress.Dispatch,
                        AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                        LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                        DateAdded = DateTime.Now.Date,
                        DateLastModified = DateTime.Now.Date,
                        TimeAdded = DateTime.Now.TimeOfDay,
                        TimeLastModified = DateTime.Now.TimeOfDay
                    };


                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync(customer);
                    if (customerTelephone != null)
                        await db.AddAsync(customerTelephone);
                    if (customerEmail != null)
                        await db.AddAsync(customerEmail);
                    if (record.Identification.IssueDate != null && record.Identification.ExpiryDate != null)
                        await db.AddAsync (identification);
                    if (nextOfKin != null)
                        await db.AddAsync(nextOfKin);
                    if (address != null)
                        await db.AddAsync(address);
                    await db.SaveChangesAsync();
                }

                return new Response
                {
                    ResponseCode = "00",
                    ResponseResult = customerID
                };

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> ProdMasterAccount(string prodCode, string branchCode, string code)
        {

                var result = await funcAndProc.ProdMasterAccount(prodCode, branchCode, code);
                if (result != null)
                    return result;
                else
                    return "";

        }
        public async Task<string> LedgerChartCode(string prodCode)
        {
            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                db.Database.OpenConnection();
                var result = await db.ProdAcctsMapping.Where(x => x.ProdCode == prodCode && x.ProdAcctTypeCode == "01").Select(x => x.ProdGLAcct).SingleOrDefaultAsync();
                db.Database.CloseConnection();

                if (result != null)
                    return result;
                else
                    return "";
            }

        }
        public int AccountSequence(string cusID, string prodCode)
        {
            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                db.Database.OpenConnection();
                var result = db.AcctMaster.Where(x => x.CusId == cusID && x.ProdCode == prodCode).Count() + 1;
                db.Database.CloseConnection();
                return result;
            }

        }
        public async Task<List<VwProdMaster>> GetProdMaster()
        {
            var records = new List<VwProdMaster>();

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.VwProdMaster.Where(x => x.ProdCode != "ZZZ").ToListAsync();
            }
            return records;
        }
        public async Task<Response> ProdMasterLookup()
        {
            var lookups = new List<ViewProdMasterLookup>();
            var records = new List<ProdMaster>();
            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.ProdMaster.Where(X => X.ProdCatCode.Equals("1") || X.ProdCatCode.Equals("2")).ToListAsync();
            }

            foreach (var record in records)
            {
                lookups.Add(new ViewProdMasterLookup
                {
                    ProdCode = record.ProdCode,
                    ProdName = record.ProdName
                });
            }

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> LoanProdMasterLookup()
        {
            var lookups = new List<ViewProdMasterLookup>();
            var records = new List<ProdMaster>();
            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.ProdMaster.Where(X => X.ProdCatCode.Equals("6") && X.ArrearsProdCode != "ZZZ").ToListAsync();
            }

            foreach (var record in records)
            {
                lookups.Add(new ViewProdMasterLookup
                {
                    ProdCode = record.ProdCode,
                    ProdName = record.ProdName
                });
            }

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> NationalityLookup()
        {
            var lookups = new List<ViewParamCountryLookup>();
            var records = new List<ParamCountries>();
            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.ParamCountries.ToListAsync();
            }

            foreach (var record in records)
            {
                lookups.Add(new ViewParamCountryLookup
                {
                    CountryCode = record.CountryCode,
                    CountryName = record.CountryName
                });
            }

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> ParamTypesDetailLookup(int typeCode)
        {
            var lookups = new List<ViewParamTypesDetailLookup>();
            var records = new List<ParamTypesDetails>();
            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.ParamTypesDetails.Where(x => x.TypeCode == typeCode).ToListAsync();
            }

            foreach (var record in records)
            {
                lookups.Add(new ViewParamTypesDetailLookup
                {
                    Code = record.Code,
                    Description = record.Description
                });
            }

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> CurrencyLookup()
        {
            try
            {
                var currencies = new List<ViewCurrencyLookup>();
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    var response = await db.PrmCurrencies.ToListAsync();
                    foreach (var record in response)
                    {
                        currencies.Add(new ViewCurrencyLookup
                        {
                            CurrCode = record.CurrCode,
                            CurrDesc = record.CurrDesc
                        });
                    }

                    return new Response
                    {
                        ResponseCode = "00",
                        ResponseResult = currencies
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = "01",
                    ResponseResult = ex.Message
                };
            }

        }
        public async Task<Response> BranchLookup()
        {
            try
            {
                var branches = new List<ViewBranchLookup>();
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    var response = await db.OrgBranches.Where(x => x.BranchCode != "000").ToListAsync();
                    foreach (var record in response)
                    {
                        branches.Add(new ViewBranchLookup
                        {
                            BranchCode = record.BranchCode,
                            BranchName = record.BranchName
                        });
                    }

                    return new Response
                    {
                        ResponseCode = "00",
                        ResponseResult = branches
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = "01",
                    ResponseResult = ex.Message
                };
            }
        }
        public async Task<Response> MaturityTypeLookup()
        {
            var lookups = new List<ViewParamTypesDetailLookup>();
            var records = new List<ParamTypesDetails>();
            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.ParamTypesDetails.Where(x => x.TypeCode == 42).ToListAsync(); //&& x.Code.Equals("01") || x.Code.Equals("02")
            }

            foreach (var record in records)
            {
                if (record.Code.Equals("01") || record.Code.Equals("02"))
                {
                    lookups.Add(new ViewParamTypesDetailLookup
                    {
                        Code = record.Code,
                        Description = record.Description
                    });

                }
            }

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> DepositProductLookup()
        {
            var lookups = new List<ViewProdMasterLookup>();
            var records = new List<ProdMaster>();

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.ProdMaster.Where(X => X.ProdCatCode.Equals("3")).ToListAsync();
            }

            foreach (var record in records)
            {
                lookups.Add(new ViewProdMasterLookup
                {
                    ProdCode = record.ProdCode,
                    ProdName = record.ProdName
                });
            }

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> PrmCardTypesLookup()
        {
            var lookups = new List<ViewPrmCardTypesLookup>();
            var records = new List<PrmCardTypes>();

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                records = await db.PrmCardType.ToListAsync();
            }

            foreach (var record in records)
            {
                lookups.Add(new ViewPrmCardTypesLookup
                {
                    CardType = record.CardType,
                    CardDesc = record.CardDesc
                });
            }

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> NewAccountOpening(ViewNewAccountOpening record)
        {
            var postingDate = new DateTime().Date;

            //var customerID = "";

            var accountNo = "";

            if (!record.Arrears)
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    postingDate = db.PostingDates.Select(x => x.PostingDate).SingleOrDefault();
                }

                accountNo = await funcAndProc.GenerateAccountNumber(record.CusID, record.BranchCode, record.ProdCode);

                try
                {

                    using (var db = new EazyCoreContext(eazybank.Options, configuration))
                    {
                        var loanAccount = new AcctMaster 
                        {
                            AccountNo = accountNo,
                            ParentAccountNo = await ProdMasterAccount(record.ProdCode, record.BranchCode, "01"),
                            ParentAccountNoOverdrawn = "",
                            NetAccountNo = "",
                            OldAccountNo = record.AgentCode,
                            AccountDesc = record.CusName,
                            CusId = record.CusID,
                            BranchCode = record.BranchCode,
                            UnitCode = "ZZZ",
                            UnitPosCode = "ZZZ",
                            ProdCode = record.ProdCode,
                            LedgerChartCode = await LedgerChartCode(record.ProdCode),
                            CurrCode = "NGN",
                            Sequence = AccountSequence(record.CusID, record.ProdCode),
                            SequenceSo = 0,
                            AccountStatus = "1",
                            PostStatus = "1",
                            AccountType = "4",
                            AcctSecurityLevel = 1,
                            PaymentInstruct = "",
                            PaymentCombin = "",
                            Arrears = false,
                            MarketedBy = "ZZZ",
                            PostingDateAdded = postingDate.Date,
                            DebitIntBaseCodeMargin = 0,
                            CreditIntBaseCodeMargin = 0,
                            CotbaseCode = "000",
                            LastTransDateUser = null,
                            LastDebitTransDateUser = null,
                            LastCreditTransDateUser = null,
                            LastTransDateSystem = null,
                            LastDebitTransDateSystem = null,
                            LastCreditTransDateSystem = null,
                            BookBal = 0,
                            ClearBal = 0,
                            CurrBal = 0,
                            BlockedBal = 0,
                            DisbursedAmount = 0,
                            LastDayBookBal = 0,
                            LastDayClearBal = 0,
                            LastDayCurrBal = 0,
                            LastDayMonthDebits = 0,
                            LastDayMonthCredits = 0,
                            LastDayYearDebits = 0,
                            LastDayYearCredits = 0,
                            LastDayDisbursedAmount = 0,
                            LastMonthBookBal = 0,
                            LastMonthClearBal = 0,
                            LastMonthCurrBal = 0,
                            LastYearBookBal = 0,
                            LastYearClearBal = 0,
                            LastYearCurrBal = 0,
                            LastTwoYearsBookBal = 0,
                            LastTwoYearsClearBal = 0,
                            LastTwoYearsCurrBal = 0,
                            MonthDebits = 0,
                            MonthCredits = 0,
                            YearDebits = 0,
                            YearCredits = 0,
                            CreditClassCode = "001",
                            StopInterestApplication = false,
                            AddedBy = eazCoreOptions.Value.UserID,//"System",
                            AddedApprovedBy = eazCoreOptions.Value.UserID,//"System",
                            LastModifiedBy = eazCoreOptions.Value.UserID,//"System",
                            LastApprovedBy = eazCoreOptions.Value.UserID,//"",
                            DateAdded = DateTime.Now,
                            DateAddedApproved = DateTime.Now,
                            DateLastModified = DateTime.Now,
                            DateLastApproved = DateTime.Now,
                            TimeAdded = DateTime.Now.TimeOfDay,
                            TimeAddedApproved = DateTime.Now.TimeOfDay,
                            TimeLastModified = DateTime.Now.TimeOfDay,
                            TimeLastApproved = DateTime.Now.TimeOfDay,
                            ClosedBy = "",
                            ClosedApprovedBy = "",
                            DateClosed = null,
                            TimeClosed = null,
                            PostingDateClosed = null,
                            CalcField1 = 0,
                            CalcField2 = 0,
                            ArrearsPrincipalToDate = 0,
                            ArrearsInterestToDate = 0,
                            ArrearsSettlementToDate = 0,
                            UnpaidPrincipal = 0,
                            UnpaidInterest = 0,
                            YearOpeningBalance = 0,
                            EnableSms = true,
                            EnableEmail = true,
                            EnableInternet = true,
                            InternetPub = false,
                            PanNo = "",
                            CardIssueDate = null,
                            CardIssueCounter = 0,
                            CardExipryDate = null,
                            CardseqNo = 0,
                            CreditExpiryDate = null,
                            CreditClassDays = 0,
                            ArrearsPrincipalToday = 0,
                            ArrearsInterestToday = 0,
                            ArrearsSettlementToday = 0,
                            //CardIssueCounterss = 0,
                            CarryingCurrClearBal = 0,
                            Impairment = 0,
                            SegmentId = "ZZZ",
                            ArrearsLastRepaymentAmount = 0,
                            ArrearsLastRepaymentDate = postingDate.Date,
                            ArrearsNumber = 0,
                            MinBalMargin = 0
                        };

                        try
                        {
                            db.AcctMaster.Add(loanAccount);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            //logger.LogError("{@Ex} Error - Loan Account: ", ex.Message);
                        }

                    }

                    return new Response
                    {
                        ResponseCode = "00",
                        ResponseResult = accountNo
                    };
                }
                catch (DbUpdateException ex)
                {
                    throw ex;
                }
            }
            else
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    postingDate = db.PostingDates.Select(x => x.PostingDate).SingleOrDefault();
                }
                try
                {

                    using (var db = new EazyCoreContext(eazybank.Options, configuration))
                    {
                        var loanAccount = new AcctMaster
                        {
                            AccountNo = record.AccountNo + "A",
                            ParentAccountNo = await ProdMasterAccount(record.ProdCode, record.BranchCode, "01"),
                            ParentAccountNoOverdrawn = "",
                            NetAccountNo = "",
                            OldAccountNo = "",
                            AccountDesc = record.CusName,
                            CusId = record.CusID,
                            BranchCode = record.BranchCode,
                            UnitCode = "ZZZ",
                            UnitPosCode = "ZZZ",
                            ProdCode = record.ProdCode,
                            LedgerChartCode = await LedgerChartCode(record.ProdCode),
                            CurrCode = "NGN",
                            Sequence = AccountSequence(record.CusID, record.ProdCode),
                            SequenceSo = 0,
                            AccountStatus = "1",
                            PostStatus = "1",
                            AccountType = "4",
                            AcctSecurityLevel = 1,
                            PaymentInstruct = "",
                            PaymentCombin = "",
                            Arrears = false,
                            MarketedBy = "ZZZ",
                            PostingDateAdded = postingDate.Date,
                            DebitIntBaseCodeMargin = 0,
                            CreditIntBaseCodeMargin = 0,
                            CotbaseCode = "000",
                            LastTransDateUser = null,
                            LastDebitTransDateUser = null,
                            LastCreditTransDateUser = null,
                            LastTransDateSystem = null,
                            LastDebitTransDateSystem = null,
                            LastCreditTransDateSystem = null,
                            BookBal = 0,
                            ClearBal = 0,
                            CurrBal = 0,
                            BlockedBal = 0,
                            DisbursedAmount = 0,
                            LastDayBookBal = 0,
                            LastDayClearBal = 0,
                            LastDayCurrBal = 0,
                            LastDayMonthDebits = 0,
                            LastDayMonthCredits = 0,
                            LastDayYearDebits = 0,
                            LastDayYearCredits = 0,
                            LastDayDisbursedAmount = 0,
                            LastMonthBookBal = 0,
                            LastMonthClearBal = 0,
                            LastMonthCurrBal = 0,
                            LastYearBookBal = 0,
                            LastYearClearBal = 0,
                            LastYearCurrBal = 0,
                            LastTwoYearsBookBal = 0,
                            LastTwoYearsClearBal = 0,
                            LastTwoYearsCurrBal = 0,
                            MonthDebits = 0,
                            MonthCredits = 0,
                            YearDebits = 0,
                            YearCredits = 0,
                            CreditClassCode = "001",
                            StopInterestApplication = false,
                            AddedBy = eazCoreOptions.Value.UserID,//"System",
                            AddedApprovedBy = eazCoreOptions.Value.UserID,//"System",
                            LastModifiedBy = eazCoreOptions.Value.UserID,//"System",
                            LastApprovedBy = eazCoreOptions.Value.UserID,//"",
                            DateAdded = DateTime.Now,
                            DateAddedApproved = DateTime.Now,
                            DateLastModified = DateTime.Now,
                            DateLastApproved = DateTime.Now,
                            TimeAdded = DateTime.Now.TimeOfDay,
                            TimeAddedApproved = DateTime.Now.TimeOfDay,
                            TimeLastModified = DateTime.Now.TimeOfDay,
                            TimeLastApproved = DateTime.Now.TimeOfDay,
                            ClosedBy = "",
                            ClosedApprovedBy = "",
                            DateClosed = null,
                            TimeClosed = null,
                            PostingDateClosed = null,
                            CalcField1 = 0,
                            CalcField2 = 0,
                            ArrearsPrincipalToDate = 0,
                            ArrearsInterestToDate = 0,
                            ArrearsSettlementToDate = 0,
                            UnpaidPrincipal = 0,
                            UnpaidInterest = 0,
                            YearOpeningBalance = 0,
                            EnableSms = true,
                            EnableEmail = true,
                            EnableInternet = true,
                            InternetPub = false,
                            PanNo = "",
                            CardIssueDate = null,
                            CardIssueCounter = 0,
                            CardExipryDate = null,
                            CardseqNo = 0,
                            CreditExpiryDate = null,
                            CreditClassDays = 0,
                            ArrearsPrincipalToday = 0,
                            ArrearsInterestToday = 0,
                            ArrearsSettlementToday = 0,
                            //CardIssueCounterss = 0,
                            CarryingCurrClearBal = 0,
                            Impairment = 0,
                            SegmentId = "ZZZ",
                            ArrearsLastRepaymentAmount = 0,
                            ArrearsLastRepaymentDate = postingDate.Date,
                            ArrearsNumber = 0,
                            MinBalMargin = 0
                        };

                        try
                        {
                            db.Add(loanAccount);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                            //logger.LogError("{@Ex} Error - Loan Account: ", ex.Message);
                        }

                    }

                    return new Response
                    {
                        ResponseCode = "00",
                        ResponseResult = record.AccountNo + "A"
                    };

                }
                catch (DbUpdateException ex)
                {
                    throw ex;
                }
            }

        }
        public async Task<Response> AmountBlockRequest(ViewBlockAccountBal req)
        {
            var postingDate = new PostingDates();
            var transID = "";
            var acctMaster = new AcctMaster(); 

            int count = 0;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                postingDate = db.PostingDates.Where(x => x.PostingOn == true).SingleOrDefault();
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNumber)).SingleOrDefault(); //specify the desire sequence number for transaction id generation
            }


            var acctmaint = new AcctMaint
            {
                TransID = transID,
                MaintType = "61",
                PostingDate = postingDate.PostingDate,
                Narrative = "Block Account Balance",
                EditMode = false,
                TransStatus = "3",
                BranchAdded = acctMaster.BranchCode,
                AddedBy = eazCoreOptions.Value.UserID,//"System",
                DateAdded = DateTime.Now,
                TimeAdded = TimeSpan.FromMinutes(00),
                WorkstationAdded = req.WorkstationAdded,
                WorkstationIPAdded = req.WorkstationAdded,
                BranchApproved = acctMaster.BranchCode,
                PostingDateApproved = postingDate.PostingDate,
                AddedApprovedBy = "",
                DateAddedApproved = DateTime.Now,
                TimeAddedApproved = TimeSpan.FromMinutes(00),
                WorkstationApproved = "",
                WorkstationIPApproved = ""
            };

            var acctMaintDetails = new AcctMaintDetails
            {
                SeqNo = 0,
                TransID = transID,
                AccountNo = req.AccountNumber,
                Reason = req.Narration,
                OldTextValue = "",
                NewTextValue = "",
                OldNumValue = acctMaster.BlockedBal.Value,
                NewNumValue = req.Amount,
                OldBoolValue = false,
                NewBoolValue = false,
                Remarks = req.AccountName

            };

            try
            {
                //Pool PFI record
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    db.Add(acctmaint);
                    db.Add(acctMaintDetails);

                    //db.Database.OpenConnection();
                    count = db.SaveChanges();
                }
                //update AcctMaster with the block amount on the given account
                acctMaster.BlockedBal = req.Amount;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    var local = db.Set<AcctMaster>()
                             .Local
                             .FirstOrDefault(entry => entry.AccountNo.Equals(acctMaster.AccountNo));

                    if (local != null)
                    {

                        db.Entry(local).State = EntityState.Detached;
                    }

                    db.Update(acctMaster);
                    var result = db.SaveChanges();

                }
                return new Response { ResponseCode = "00",  ResponseResult = "Success" };
            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "01",  ResponseResult = ex.Message };
            }
        }
        public async Task<Response> AmountUnBlockRequest(ViewBlockAccountBal req)
        {
            var postingDate = new PostingDates();
            var transID = "";
            var acctMaster = new AcctMaster();

            int count = 0;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                postingDate = await db.PostingDates.Where(x => x.PostingOn == true).SingleOrDefaultAsync();
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNumber)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }


            var acctmaint = new AcctMaint
            {
                TransID = transID,
                MaintType = "61",
                PostingDate = postingDate.PostingDate,
                Narrative = "Block Account Balance",
                EditMode = false,
                TransStatus = "3",
                BranchAdded = acctMaster.BranchCode,
                AddedBy = eazCoreOptions.Value.UserID,//"System",
                DateAdded = DateTime.Now,
                TimeAdded = TimeSpan.FromMinutes(00),
                WorkstationAdded = req.WorkstationAdded,
                WorkstationIPAdded = req.WorkstationAdded,
                BranchApproved = acctMaster.BranchCode,
                PostingDateApproved = postingDate.PostingDate,
                AddedApprovedBy = "",
                DateAddedApproved = DateTime.Now,
                TimeAddedApproved = TimeSpan.FromMinutes(00),
                WorkstationApproved = "",
                WorkstationIPApproved = ""
            };

            var acctMaintDetails = new AcctMaintDetails
            {
                SeqNo = 0,
                TransID = transID,
                AccountNo = req.AccountNumber,
                Reason = req.Narration,
                OldTextValue = "",
                NewTextValue = "",
                OldNumValue = acctMaster.BlockedBal.Value,
                NewNumValue = req.Amount,
                OldBoolValue = false,
                NewBoolValue = false,
                Remarks = req.AccountName

            };

            try
            {
                //Pool PFI record
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync(acctmaint);
                    await db.AddAsync(acctMaintDetails);

                    //db.Database.OpenConnection();
                    count = await db.SaveChangesAsync();
                }
                //update AcctMaster with the block amount on the given account
                acctMaster.BlockedBal = req.Amount;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    var local = db.Set<AcctMaster>()
                             .Local
                             .FirstOrDefault(entry => entry.AccountNo.Equals(acctMaster.AccountNo));

                    if (local != null)
                    {

                        db.Entry(local).State = EntityState.Detached;
                    }

                    db.Update(acctMaster);
                    var result = await db.SaveChangesAsync();

                }
                return new Response { ResponseCode = "00",  ResponseResult = "Success" };
            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "01", ResponseResult = ex.Message };
            }
        }
        public async Task<Response> AccountBlockRequest(ViewBlockAccountPostingStatus req)
        {
            var postingDate = DateTime.Now.Date;
            var transID = "";
            var acctMaster = new AcctMaster();

            int count = 0;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                postingDate = await db.PostingDates.Where(x => x.PostingOn == true).Select(x => x.PostingDate).SingleOrDefaultAsync();
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNumber)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            var acctmaint = new AcctMaint
            {
                TransID = transID,
                MaintType = "03",
                PostingDate = postingDate,
                Narrative = "Change Posting Status",
                EditMode = false,
                TransStatus = "3",
                BranchAdded = acctMaster.BranchCode,
                AddedBy = eazCoreOptions.Value.UserID,//"System",
                DateAdded = DateTime.Now,
                TimeAdded = TimeSpan.FromMinutes(00),
                WorkstationAdded = req.WorkstationAdded,
                WorkstationIPAdded = req.WorkstationAdded,
                BranchApproved = acctMaster.BranchCode,
                PostingDateApproved = postingDate,
                AddedApprovedBy = "",
                DateAddedApproved = DateTime.Now,
                TimeAddedApproved = TimeSpan.FromMinutes(00),
                WorkstationApproved = "",
                WorkstationIPApproved = ""
            };

            var acctMaintDetails = new AcctMaintDetails
            {
                SeqNo = 0,
                TransID = transID,
                AccountNo = req.AccountNumber,
                Reason = req.Narration,
                OldTextValue = acctMaster.PostStatus,
                NewTextValue = "2",
                OldNumValue = 0,
                NewNumValue = 0,
                OldBoolValue = false,
                NewBoolValue = false,
                Remarks = acctMaster.AccountDesc

            };

            try
            {
                //Pool PFI record
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync(acctmaint);
                    await db.AddAsync(acctMaintDetails);

                    //db.Database.OpenConnection();
                    count = await db.SaveChangesAsync();
                }
                //update AcctMaster with the posting status on the given account
                acctMaster.PostStatus = "2";

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    var local = db.Set<AcctMaster>()
                             .Local
                             .FirstOrDefault(entry => entry.AccountNo.Equals(acctMaster.AccountNo));

                    if (local != null)
                    {

                        db.Entry(local).State = EntityState.Detached;
                    }

                    db.Update(acctMaster).Property(x => x.CardseqNo).IsModified = false; 
                    var result = await db.SaveChangesAsync();

                }
                return new Response { ResponseCode = "00", ResponseResult = "Success" };
            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "01", ResponseResult = ex.Message };
            }
        }
        public async Task<Response> AccountUnBlockRequest(ViewBlockAccountPostingStatus req)
        {
            var postingDate = DateTime.Now.Date;
            var transID = "";
            var acctMaster = new AcctMaster();

            int count = 0;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                postingDate = await db.PostingDates.Where(x => x.PostingOn == true).Select(x => x.PostingDate).SingleOrDefaultAsync();
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNumber)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }


            var acctmaint = new AcctMaint
            {
                TransID = transID,
                MaintType = "03",
                PostingDate = postingDate,
                Narrative = "Change Posting Status",
                EditMode = false,
                TransStatus = "3",
                BranchAdded = acctMaster.BranchCode,
                AddedBy = eazCoreOptions.Value.UserID,//"System",
                DateAdded = DateTime.Now,
                TimeAdded = TimeSpan.FromMinutes(00),
                WorkstationAdded = req.WorkstationAdded,
                WorkstationIPAdded = req.WorkstationAdded,
                BranchApproved = acctMaster.BranchCode,
                PostingDateApproved = postingDate,
                AddedApprovedBy = "",
                DateAddedApproved = DateTime.Now,
                TimeAddedApproved = TimeSpan.FromMinutes(00),
                WorkstationApproved = "",
                WorkstationIPApproved = ""
            };

            var acctMaintDetails = new AcctMaintDetails
            {
                SeqNo = 0,
                TransID = transID,
                AccountNo = req.AccountNumber,
                Reason = req.Narration,
                OldTextValue = acctMaster.PostStatus,
                NewTextValue = "1",
                OldNumValue = 0,
                NewNumValue = 0,
                OldBoolValue = false,
                NewBoolValue = false,
                Remarks = req.AccountName

            };

            try
            {
                //Pool PFI record
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync(acctmaint);
                    await db.AddAsync(acctMaintDetails);

                    //db.Database.OpenConnection();
                    count = await db.SaveChangesAsync();
                }
                //update AcctMaster with the posting sataus on the given account
                acctMaster.PostStatus = "1";

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    var local = db.Set<AcctMaster>()
                             .Local
                             .FirstOrDefault(entry => entry.AccountNo.Equals(acctMaster.AccountNo));

                    if (local != null)
                    {

                        db.Entry(local).State = EntityState.Detached;
                    }

                    db.Update(acctMaster).Property(x => x.CardseqNo).IsModified = false; 
                    var result = await db.SaveChangesAsync();

                }
                return new Response { ResponseCode = "00", ResponseResult = "Success" };
            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "01", ResponseResult = ex.Message };
            }
        }
        public async Task<Response> AcctCardTransRequest(ViewAcctCardTransSave req)
        {
            var postingDate = DateTime.Now.Date;
            var transID = "";
            var acctCardTrans = new AcctCardTrans();

            var acctMaster = new AcctMaster();

            int count = 0;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                postingDate = await db.PostingDates.Where(x => x.PostingOn == true).Select(x => x.PostingDate).SingleOrDefaultAsync();
            }

             transID = await funcAndProc.GenerateNumberSequence(1068); //specify the desire sequence number for transaction id generation

            //using (var db = new EazyCoreContext(eazybank.Options, configuration))
            //{
            //    acctCardTrans = db.AcctCardTran.Where(x => x.TransID.Equals(req.TransID)).SingleOrDefault(); //specify the desire sequence number for transaction id generation
            //}

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.CardTransDetails.AccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }


            var acctmaint = new AcctCardTrans
            {
                TransID = transID,
                PostingDate = postingDate,
                Narrative = "Card Request From User",
                EditMode = false,
                TransStatus = "3",
                BranchAdded = acctMaster.BranchCode,
                AddedBy = "System",
                DateAdded = DateTime.Now,
                TimeAdded = TimeSpan.FromMinutes(00),
                WorkstationAdded = "eChannel",
                WorkstationIPAdded = "eChannel",
                BranchApproved = acctMaster.BranchCode,
                PostingDateApproved = postingDate,
                AddedApprovedBy = "",
                DateAddedApproved = DateTime.Now,
                TimeAddedApprove = TimeSpan.FromMinutes(00),
                WorkstationApproved = "",
                WorkstationIPApproved = ""
            };

            var cardDetails = new AcctCardTransDetails
            {
                Sequence = 0,
                TransID = transID,
                AccountNo = req.CardTransDetails.AccountNo,
                CardType = req.CardTransDetails.CardType,
                CardSubType = req.CardTransDetails.CardSubType,
                CardNo = req.CardTransDetails.CardNo,
                SecurityNo = req.CardTransDetails.SecurityNo,
                IssueDate = req.CardTransDetails.IssueDate,
                ExpiryDate = req.CardTransDetails.ExpiryDate,
                BankCode = req.CardTransDetails.BankCode,
                SerialNo = req.CardTransDetails.SerialNo,
                ChargeAmount = req.CardTransDetails.ChargeAmount

            };

            try
            {
                //Pool PFI record
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync (acctCardTrans);
                    await db.AddAsync(cardDetails);

                    //db.Database.OpenConnection();
                    count = await db.SaveChangesAsync();
                }
                //update AcctMaster with the posting sataus on the given account
                //acctMaster.PostStatus = "1";

                //using (var db = new EazyCoreContext(eazybank.Options, configuration))
                //{
                //    var local = db.Set<AcctMaster>()
                //             .Local
                //             .FirstOrDefault(entry => entry.AccountNo.Equals(acctMaster.AccountNo));

                //    if (local != null)
                //    {

                //        db.Entry(local).State = EntityState.Detached;
                //    }

                //    db.Update(acctMaster);
                //    var result = db.SaveChanges();

                //}

                if(count > 0)
                {
                    var cardMaster = new AcctCardMaster
                    {
                        Sequence = 0,
                        TransID = transID,
                        AccountNo = req.CardTransDetails.AccountNo,
                        CardType = req.CardTransDetails.CardType,
                        CardSubType = req.CardTransDetails.CardSubType,
                        CardNo = req.CardTransDetails.CardNo,
                        SecurityNo = req.CardTransDetails.SecurityNo,
                        IssueDate = req.CardTransDetails.IssueDate,
                        ExpiryDate = req.CardTransDetails.ExpiryDate,
                        BankCode = req.CardTransDetails.BankCode,
                        SerialNo = req.CardTransDetails.SerialNo,
                        ChargeAmount = req.CardTransDetails.ChargeAmount,
                        CardStatus = "",
                        PostingDateAdded = postingDate,
                        AddedBy = "System",
                        AddedApprovedBy = "System",
                        LastModifiedBy = "System",
                        DateAdded = DateTime.Now.Date,
                        DateAddedApproved = DateTime.Now.Date,
                        DateLastModified = DateTime.Now.Date,
                        TimeAdded = DateTime.Now.TimeOfDay,
                        TimeAddedApproved = DateTime.Now.TimeOfDay,
                        TimeLastModified = DateTime.Now.TimeOfDay
                    };

                    using (var db = new EazyCoreContext(eazybank.Options, configuration))
                    {
                        await db.AddAsync(cardMaster);
                        //db.Database.OpenConnection();
                        await db.SaveChangesAsync();
                    }

                    return new Response { ResponseCode = "00", ResponseResult = "Success" };

                }

                return new Response { ResponseCode = "01", ResponseResult = "Failed" };

            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "01", ResponseResult = ex.Message };
            }
        }
        public async Task<Response> AcctCardHotListRequest(string req)
        {
            var postingDate = DateTime.Now.Date;
            var acctCardTrans = new AcctCardMaster();

            var acctMaster = new AcctMaster();

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                postingDate = await db.PostingDates.Where(x => x.PostingOn == true).Select(x => x.PostingDate).SingleOrDefaultAsync();
            }

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctCardTrans = await db.AcctCardMaster.Where(x => x.AccountNo.Equals(req)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            try
            {
                //Pool PFI record

                if(acctCardTrans != null)
                {
                    //update AcctMaster with the posting sataus on the given account
                    acctMaster.PanNo = "1";

                    using (var db = new EazyCoreContext(eazybank.Options, configuration))
                    {
                        var local = db.Set<AcctMaster>()
                                 .Local
                                 .FirstOrDefault(entry => entry.AccountNo.Equals(req));

                        if (local != null)
                        {

                            db.Entry(local).State = EntityState.Detached;
                        }

                        db.Update(acctMaster);
                        var result = await db.SaveChangesAsync();

                    }

                    return new Response { ResponseCode = "00", ResponseResult = "Successful" };

                }
                else
                    return new Response { ResponseCode = "01", ResponseResult = "Record not found" };

            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "01", ResponseResult = ex.Message };
            }
        }
        public async Task<Response> GetCardMaster(string accountNo)
        {
            var cardMaster = new List<VwAcctCardMaster>();

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                var result = await db.AcctCardMaster.Where(x => x.AccountNo.Equals(accountNo)).ToListAsync();

                if (result.Count >0 )
                {
                    foreach(var record in result)
                    {
                        cardMaster.Add(new VwAcctCardMaster
                        {
                            Sequence = record.Sequence,
                            TransID = record.TransID,
                            AccountNo = record.AccountNo,
                            AccountDesc = "",
                            ProdCode = "",
                            ProdName = "",
                            CardType = record.CardType,
                            CardDesc = "",
                            CardSubType = record.CardSubType,
                            CardSubTypeDesc = "",
                            CardNo = record.CardNo,
                            SecurityNo = record.SecurityNo,
                            IssueDate = record.IssueDate,
                            ExpiryDate = record.ExpiryDate,
                            BankCode = record.BankCode,
                            BankName = "",
                            SerialNo = record.SerialNo,
                            ChargeAmount = record.ChargeAmount,
                            CardStatus = record.CardStatus,
                            CardStatusDesc = "",
                            PostingDateAdded = record.PostingDateAdded
                    });
                    }
                    return new Response { ResponseCode = "00", ResponseResult = cardMaster };

                }
                else
                    return new Response { ResponseCode = "0", ResponseResult = "No record found" };

            }

        }
        public async Task<List<VwOrgBranches>> GetBankBranches()
        {
            var result = new List<VwOrgBranches>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwOrgBranches.ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<VwOrgBranchesCordinate>> GetBankBranchesCordinate()
        {
            var result = new List<VwOrgBranchesCordinate>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwOrgBranchesCordinate.ToListAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Response> DisputeTypesLookup()
        {
            try
            {
                var disputeTypes = new List<ViewPrmDisputeTypes>();
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    var response = await db.PrmDisputeTypes.ToListAsync();
                    foreach (var record in response)
                    {
                        disputeTypes.Add(new ViewPrmDisputeTypes
                        {
                            DisputeCode = record.DisputeCode,
                            DisputeDesc = record.DisputeDesc
                        });
                    }

                    return new Response
                    {
                        ResponseCode = "00",
                        ResponseResult = disputeTypes
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = "01",
                    ResponseResult = ex.Message
                };
            }

        }
        public async Task<Response> DisputeLogSave(ViewDisputeLogSave req)
        {
            var disputeLog = new DisputeLog();
            var postingDate = DateTime.Now.Date;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                disputeLog = await db.DisputeLogs.Where(x => x.ReferenceID.Equals(req.ReferenceID) && x.AccountNo.Equals(req.AccountNo)).Take(1).SingleOrDefaultAsync();
            }

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                postingDate = await db.PostingDates.Where(x => x.PostingOn == true).Select(x => x.PostingDate).SingleOrDefaultAsync();
            }

            var log = new DisputeLog
            {
                SeqNo = 0,
                AccountNo = req.AccountNo,
                DisputeCode = req.DisputeCode,
                Comment = req.Comment,
                LogDate = DateTime.Now.Date,
                LogDateTime = DateTime.Now,
                PostingDate = postingDate,
                ReferenceID = req.ReferenceID,
                Resolved = false
            };

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                await db.AddAsync(log);
                await db.SaveChangesAsync();
            }

            return new Response { ResponseCode = "00", ResponseResult = "Success" };
         
        }
        public async Task<Response> DisputeLogByRefNoAndAccount(string refNo, string accountNo)
        {
            //var disputeLog = new DisputeLog();
            var log = new VwDisputeLog();


            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                log = await db.VwDisputeLog.Where(x => x.ReferenceID.Equals(refNo) && x.AccountNo.Equals(accountNo))
                    .Select(x => new VwDisputeLog
                    { 
                        SeqNo = x.SeqNo,
                        AccountNo = x.AccountNo,
                        AccountDesc = x.AccountDesc,
                        DisputeCode = x.DisputeCode,
                        DisputeDesc = x.DisputeDesc,
                        Comment = x.Comment,
                        LogDate = x.LogDate,
                        LogDateTime = x.LogDateTime,
                        PostingDate = x.PostingDate,
                        ReferenceID = x.ReferenceID
                    }).SingleOrDefaultAsync();
            }

            
            return new Response { ResponseCode = "00", ResponseResult = log };

        }
        public async Task<Response> DisputeLogByRefNo(string refNo)
        {
            //var disputeLog = new DisputeLog();
            var log = new VwDisputeLog();


            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                log = await db.VwDisputeLog.Where(x => x.ReferenceID.Equals(refNo))
                    .Select(x => new VwDisputeLog
                    {
                        SeqNo = x.SeqNo,
                        AccountNo = x.AccountNo,
                        AccountDesc = x.AccountDesc,
                        DisputeCode = x.DisputeCode,
                        DisputeDesc = x.DisputeDesc,
                        Comment = x.Comment,
                        LogDate = x.LogDate,
                        LogDateTime = x.LogDateTime,
                        PostingDate = x.PostingDate,
                        ReferenceID = x.ReferenceID
                    }).SingleOrDefaultAsync();
            }


            return new Response { ResponseCode = "00", ResponseResult = log };

        }
        public async Task<Response> DisputeLogDateRange(DateTime startDate, DateTime endDate)
        {
            //var disputeLog = new DisputeLog();
            var log = new List<VwDisputeLog>();


            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                log = await db.VwDisputeLog.Where(x => x.LogDate >= startDate && x.LogDate <= endDate)
                    .Select(x => new VwDisputeLog
                    {
                        SeqNo = x.SeqNo,
                        AccountNo = x.AccountNo,
                        AccountDesc = x.AccountDesc,
                        DisputeCode = x.DisputeCode,
                        DisputeDesc = x.DisputeDesc,
                        Comment = x.Comment,
                        LogDate = x.LogDate,
                        LogDateTime = x.LogDateTime,
                        PostingDate = x.PostingDate,
                        ReferenceID = x.ReferenceID
                    }).ToListAsync();
            }


            return new Response { ResponseCode = "00", ResponseResult = log };

        }
        public async Task<Response> ImageUpload(ViewAcctMandateSave req)
        {
            //var acctMandate = new AcctMandates();  
            var acctMaster = new AcctMaster();
            var record = new AcctMandates();
            var transID = "";


            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            if (acctMaster == null)
            {
                return new Response
                {
                    ResponseCode = "400",
                    ResponseResult = "Invalid client Id"
                };
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            using (var db = new ImageContext(imageContext.Options, imageConfiguration))
            {
                //record = db.AcctMandates.Where(x => x.AccountNo.Equals(acctMaster.AccountNo)).Distinct().SingleOrDefault(); //specify the desire sequence number for transaction id generation
                record = await db.AcctMandates.Where(x => x.AccountNo.Equals(acctMaster.AccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            if (record == null && !string.IsNullOrWhiteSpace(req.Image))
            {
                try
                {

                    byte[] fileBytes = Convert.FromBase64String(req.Image);

                    var acctMandate = new AcctMandates
                    {
                        Sequence = 0,
                        AccountNo = acctMaster.AccountNo,
                        FullName = acctMaster.AccountDesc,
                        Signature = Encoding.ASCII.GetBytes(""),         // ,
                        Photo = fileBytes,
                        Image = Encoding.ASCII.GetBytes(""),
                        Comments = "",
                        SignClass = "A",
                        AddedBy = eazCoreOptions.Value.UserID,//"MobileUpload",
                        LastModifiedBy = eazCoreOptions.Value.UserID,//"MobileUpload",
                        DateAdded = DateTime.Now.Date,
                        DateLastModified = DateTime.Now.Date,
                        TimeAdded = DateTime.Now.TimeOfDay,
                        TimeLastModified = DateTime.Now.TimeOfDay,
                        account_no = "",
                        AccountNoOld = "",
                        BvnNo = "",
                        BirthDate = string.IsNullOrEmpty(req.BirthDate.ToString()) ? DateTime.Now : req.BirthDate,
                        TransID = transID
                    };
                    // Convert byte[] to Base64 String
                    using (var db = new ImageContext(imageContext.Options, imageConfiguration))
                    {
                        await db.AddAsync(acctMandate);
                        await db.SaveChangesAsync();
                    }

                    return new Response { ResponseCode = "200", ResponseResult = transID };
                }
                catch (Exception ex)
                {
                    return new Response { ResponseCode = "500", ResponseResult = ex.Message };
                }
            }
            else
            {
                try
                {

                    byte[] fileBytes = Convert.FromBase64String(req.Image);


                    record.Photo = fileBytes;
                    record.TransID = transID;

                    // Convert byte[] to Base64 String 

                    using (var db = new ImageContext(imageContext.Options, imageConfiguration))
                    {
                        db.Update(record);
                        await db.SaveChangesAsync();
                    }

                    return new Response { ResponseCode = "200", ResponseResult = transID };
                }
                catch (Exception ex)
                {
                    return new Response { ResponseCode = "500", ResponseResult = ex.Message };
                }
            }



        }
        public async Task<Response> DocumentUpload(ViewAcctMandateSave req)
        {
            //var acctMandate = new AcctMandates();
            var acctMaster = new AcctMaster();
            var record = new AcctMandates();
            var transID = "";


            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNo)).Take(1).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            if (acctMaster == null)
            {
                return new Response
                {
                    ResponseCode = "400",
                    ResponseResult = "Invalid client Id"
                };
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            using (var db = new ImageContext(imageContext.Options, imageConfiguration))
            {
                //record = db.AcctMandates.Where(x => x.AccountNo.Equals(acctMaster.AccountNo)).Distinct().SingleOrDefault(); //specify the desire sequence number for transaction id generation
                record = await db.AcctMandates.Where(x => x.AccountNo.Equals(acctMaster.AccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            if (record == null && !string.IsNullOrWhiteSpace(req.Image))
            {

                byte[] fileBytes = Convert.FromBase64String(req.Image);

                var acctMandate = new AcctMandates
                {
                    Sequence = 0,
                    AccountNo = acctMaster.AccountNo,
                    FullName = acctMaster.AccountDesc,
                    Signature = fileBytes, //signature upload
                    Photo = Encoding.ASCII.GetBytes(""),
                    Image = Encoding.ASCII.GetBytes(""),
                    Comments = "",
                    SignClass = "A",
                    AddedBy = eazCoreOptions.Value.UserID,//"MobileUpload",
                    LastModifiedBy = eazCoreOptions.Value.UserID,//"MobileUpload",
                    DateAdded = DateTime.Now.Date,
                    DateLastModified = DateTime.Now.Date,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    TimeLastModified = DateTime.Now.TimeOfDay,
                    account_no = "",
                    AccountNoOld = "",
                    BvnNo = "",
                    TransID = transID,
                    BirthDate = string.IsNullOrWhiteSpace(req.BirthDate.ToString()) ? DateTime.Now : req.BirthDate
                };

                // Convert byte[] to Base64 String
                try
                {
                    using (var db = new ImageContext(imageContext.Options, imageConfiguration))
                    {
                        await db.AddAsync(acctMandate);
                        await db.SaveChangesAsync();
                    }

                    return new Response { ResponseCode = "200", ResponseResult = transID };
                }
                catch (Exception ex)
                {
                    return new Response { ResponseCode = "500", ResponseResult = ex.Message };
                }
            }
            else
            {


                byte[] fileBytes = Convert.FromBase64String(req.Image);


                record.Signature = fileBytes;
                record.TransID = transID;

                // Convert byte[] to Base64 String
                try
                {
                    using (var db = new ImageContext(imageContext.Options, imageConfiguration))
                    {
                        db.Update(record);
                        await db.SaveChangesAsync();
                    }

                    return new Response { ResponseCode = "200", ResponseResult = transID };
                }
                catch (Exception ex)
                {
                    return new Response { ResponseCode = "500", ResponseResult = ex.Message };
                }
            }


        }
        public async Task<Response> IdentityUpload(ViewIdentityUploadRequest req)
        {
            //var acctMandate = new AcctMandates();
            var acctMaster = new AcctMaster();
            var transID = "";


            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            if (acctMaster == null)
            {
                return new Response
                {
                    ResponseCode = "400",
                    ResponseResult = "Invalid Saving Id"
                };
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            var acctMandate = new EntIdentities
            {
                Sequence = 0,
                EntityID = acctMaster.CusId,
                EntityTypeCode = "01",
                IDTypeCode = req.IDTypeCode,
                IDNo = req.IDNo,
                Issuer = req.Issuer,
                ExpiryDate = req.ExpiryDate,
                PrimaryID = true,
                AddedBy = eazCoreOptions.Value.UserID,//"System",
                LastmodifiedBy = eazCoreOptions.Value.UserID,//"System",
                DateAdded = DateTime.Now.Date,
                LastDateModified = DateTime.Now.Date,
                TimeAdded = DateTime.Now.TimeOfDay,
                LastTimeModified = DateTime.Now.TimeOfDay,
                IssueDate = req.IssueDate
            };
            // Convert byte[] to Base64 String
            try
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    await db.AddAsync(acctMandate);
                    await db .SaveChangesAsync();
                }

                var identityUpload = new ViewAcctMandateSave
                {
                    AccountNo = acctMaster.AccountNo,
                    Image = req.Image
                };
                IdentityPhotoUpload(identityUpload); // upload identification 

                return new Response { ResponseCode = "200", ResponseResult = transID };
            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "500", ResponseResult = ex.Message };
            }

        }
        //signature upload
        public async Task<Response> IdentityPhotoUpload(ViewAcctMandateSave req)
        {
            //var acctMandate = new AcctMandates();
            var acctMaster = new AcctMaster();
            var record = new AcctMandates();
            var transID = "";


            //using (var db = new EazybankContext(eazybank.Options, configuration))
            //{
            //    acctMaster = db.AcctMaster.Where(x => x.CusId.Equals(req.ClientId) ).Take(1).SingleOrDefault(); //specify the desire sequence number for transaction id generation
            //}

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                acctMaster = await db.AcctMaster.Where(x => x.AccountNo.Equals(req.AccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }


            if (acctMaster == null)
            {
                return new Response
                {
                    ResponseCode = "400",
                    ResponseResult = "Invalid client Id"
                };
            }


            transID = await funcAndProc.GenerateNumberSequence(1067); //specify the desire sequence number for transaction id generation

            using (var db = new ImageContext(imageContext.Options, imageConfiguration))
            {
                //record = db.AcctMandates.Where(x => x.AccountNo.Equals(acctMaster.AccountNo)).Distinct().SingleOrDefault(); //specify the desire sequence number for transaction id generation
                record = await db.AcctMandates.Where(x => x.AccountNo.Equals(acctMaster.AccountNo)).SingleOrDefaultAsync(); //specify the desire sequence number for transaction id generation
            }

            if (record == null && !string.IsNullOrWhiteSpace(req.Image))
            {

                byte[] fileBytes = Convert.FromBase64String(req.Image);


                var acctMandate = new AcctMandates
                {
                    Sequence = 0,
                    AccountNo = acctMaster.AccountNo,
                    FullName = acctMaster.AccountDesc,
                    Signature = Encoding.ASCII.GetBytes(""), //signature upload
                    Photo = Encoding.ASCII.GetBytes(""),
                    Image = fileBytes,
                    Comments = "",
                    SignClass = "A",
                    AddedBy = eazCoreOptions.Value.UserID,//"MobileUpload",
                    LastModifiedBy = eazCoreOptions.Value.UserID,//"MobileUpload",
                    DateAdded = DateTime.Now.Date,
                    DateLastModified = DateTime.Now.Date,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    TimeLastModified = DateTime.Now.TimeOfDay,
                    account_no = "",
                    AccountNoOld = "",
                    BvnNo = "",
                    TransID = transID,
                    BirthDate = string.IsNullOrWhiteSpace(req.BirthDate.ToString()) ? DateTime.Now : req.BirthDate
                };

                // Convert byte[] to Base64 String
                try
                {
                    using (var db = new ImageContext(imageContext.Options, imageConfiguration))
                    {
                        await db.AddAsync(acctMandate);
                        await db.SaveChangesAsync();
                    }

                    return new Response { ResponseCode = "200", ResponseResult = transID };
                }
                catch (Exception ex)
                {
                    return new Response { ResponseCode = "500", ResponseResult = ex.Message };
                }
            }
            else
            {

                byte[] fileBytes = Convert.FromBase64String(req.Image);

                record.Image = fileBytes;
                record.TransID = transID;

                // Convert byte[] to Base64 String
                try
                {
                    using (var db = new ImageContext(imageContext.Options, imageConfiguration))
                    {
                        db.Update(record);
                        await db.SaveChangesAsync();
                    }

                    return new Response { ResponseCode = "200", ResponseResult = transID };
                }
                catch (Exception ex)
                {
                    return new Response { ResponseCode = "500", ResponseResult = ex.Message };
                }
            }
        }
        public async Task<Response> BankCodesLookup()
        {
            var lookups = new List<ViewBankCode>();

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                lookups = await db.OrgBankCodes.Where(x => x.SortCode != null).Select(x => new ViewBankCode
                            {
                                BankCode = x.SortCode,
                                BankName = x.BankName
                            }).ToListAsync();
            }           

            return new Response { ResponseCode = "00", ResponseResult = lookups };

        }
        public async Task<Response> SendMessageAlerts(string transID)
        {

            //using (var db = new EazyCoreContext(eazybank.Options, configuration))
            //{
            //    funcAndProc.SendMessageAlerts(transID);
            //}

            try
            {

                await funcAndProc.SendMessageAlerts(transID);

                return new Response { ResponseCode = "00", ResponseResult = "Successful" };

            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "01", ResponseResult = ex.Message };
            }
        }
        public async Task<bool> IsTransRefExist(string transRef)
        {
            var result = false;

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                if (await db.TransLine.AnyAsync(x => x.OtherRefNo.Equals(transRef)))
                    result = true;
                //if (await db.TransHists.AnyAsync(x => x.OtherRefNo.Equals(transRef)))
                //    result = true;
            }

            return result;

        }
        public async Task<Response> InterbankListBankCodes()
        {
            var finList = new List<NIPFinancialInstitution>();
            var lists = new List<ViewFinancialIntitutionList>();

            try
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    finList = await db.NIPFinancialInstitutions.ToListAsync();
                }

                if (finList.Count > 0)
                {
                    foreach (var record in finList)
                    {
                        lists.Add(new ViewFinancialIntitutionList
                        {
                            BankCode = record.InstitutionCode,
                            BankName = record.InstitutionName
                        });
                    }

                    return new Response
                    {
                        ResponseCode = "00",
                        ResponseResult =  lists.OrderByDescending(x => x.BankName)
                    };
                }
                else
                {
                    return new Response
                    {
                        ResponseCode = "01",
                        ResponseResult = "Empty data"
                    };

                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = "01",
                    ResponseResult = "Empty data"
                };
            }

        }
        //public string GetBeneficiaryAccountName(string sessionId, string accountNo, string transType)
        //{
        //    var accountName = string.Empty;

        //    if (transType == "C")
        //    {
        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            accountName = db.AcctMaster.Where(x => x.AccountNo.Equals(accountNo)).Select(x => x.AccountDesc).SingleOrDefault();

        //            if (!string.IsNullOrEmpty(accountName))
        //            {
        //                var value = db.PrmRulesText.Where(x => x.Parameter.Equals(175)).Select(x => x.Value).FirstOrDefault();

        //                if (value == accountNo)
        //                {
        //                    accountName = db.NIPFTInwardDirectCreditReqs.Where(x => x.ContraAccount.Equals(accountNo) && x.SessionID == sessionId).Select(x => x.OriginatorAccountName).SingleOrDefault();
        //                    return accountName;
        //                }
        //                else
        //                    return accountName;
        //            }
        //            else
        //            {
        //                accountName = db.NIPFTInwardDirectCreditReqs.Where(x => x.OriginatorAccountNumber.Equals(accountNo) && x.SessionID == sessionId).Select(x => x.OriginatorAccountName).SingleOrDefault();
        //                return accountName;
        //            }
        //        }

        //    }
        //    else
        //    {
        //        using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //        {
        //            accountName = db.AcctMaster.Where(x => x.AccountNo.Equals(accountNo)).Select(x => x.AccountDesc).SingleOrDefault();

        //            if (!string.IsNullOrEmpty(accountName))
        //            {
        //                var value = db.PrmRulesText.Where(x => x.Parameter.Equals(915)).Select(x => x.Value).FirstOrDefault();
        //                if (value == accountNo)
        //                {
        //                    accountName = db.NIPFTOutwardDirectCredit.Where(x => x.ContraAccount.Equals(accountNo) && x.SessionID == sessionId).Select(x => x.BeneficiaryAccountName).SingleOrDefault();
        //                    return accountName;
        //                }
        //                else
        //                    return accountName;
        //            }
        //            else
        //            {
        //                accountName = db.NIPFTOutwardDirectCredit.Where(x => x.BeneficiaryAccountNumber.Equals(accountNo) && x.SessionID == sessionId).Select(x => x.BeneficiaryAccountName).SingleOrDefault();
        //                return accountName;
        //            }
        //        }

        //    }

        //}
        public async Task<string> GetBeneficiaryAccountName(string sessionId, string accountNo, string transType)
        {
            var accountName = string.Empty;
            string beneficiaryName = string.Empty;

            try
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountName = await db.NIPFTInwardDirectCreditReqs.Where(x => x.SessionID.Equals(sessionId)).Select(x => x.BeneficiaryAccountName).SingleOrDefaultAsync();
                }

                if (!string.IsNullOrEmpty(accountName))
                    beneficiaryName = accountName;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountName = await db.NIPFTOutwardDirectCredit.Where(x => x.SessionID.Equals(sessionId)).Select(x => x.BeneficiaryAccountName).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountName))
                    beneficiaryName = accountName;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountName = await db.AcctMaster.Where(x => x.AccountNo.Equals(accountNo)).Select(x => x.AccountDesc).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountName))
                    beneficiaryName = accountName;

                return beneficiaryName;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<string> SenderAccountName(string sessionId, string accountNo, string transType, long seqNo)
        {
            var accountName = string.Empty;
            string senderAccountName = string.Empty;

            try
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountName = await db.NIPFTInwardDirectCreditReqs.Where(x => x.SessionID.Equals(sessionId)).Select(x => x.OriginatorAccountName).SingleOrDefaultAsync();
                }

                if (!string.IsNullOrEmpty(accountName))
                    senderAccountName = accountName;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountName = await db.NIPFTOutwardDirectCredit.Where(x => x.SessionID.Equals(sessionId)).Select(x => x.OriginatorAccountName).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountName))
                    senderAccountName = accountName;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountName = await db.VwTransHist.Where(x => x.SeqNo.Equals(seqNo) && x.Debit != 0).Select(x => x.AccountDesc).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountName))
                    senderAccountName = accountName;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountName = await db.VwTransLines.Where(x => x.SeqNo.Equals(seqNo) && x.Debit != 0 && x.EditMode == false && x.TransStatus == "3" && x.Reversed == false).Select(x => x.AccountDesc).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountName))
                    senderAccountName = accountName;

                return senderAccountName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> SenderAccountNumber(string sessionId, string accountNo, string transType, long seqNo)
        {
            var accountNumber = string.Empty;
            string senderAccountNumber = string.Empty;

            try
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountNumber = await db.NIPFTInwardDirectCreditReqs.Where(x => x.SessionID.Equals(sessionId)).Select(x => x.OriginatorAccountNumber).SingleOrDefaultAsync();
                }

                if (!string.IsNullOrEmpty(accountNumber))
                    senderAccountNumber = accountNumber;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountNumber = await db.NIPFTOutwardDirectCredit.Where(x => x.SessionID.Equals(sessionId)).Select(x => x.OriginatorAccountNumber).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountNumber))
                    senderAccountNumber = accountNumber;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountNumber = await db.VwTransHist.Where(x => x.SeqNo.Equals(seqNo) && x.Debit != 0).Select(x => x.AccountNo).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountNumber))
                    senderAccountNumber = accountNumber;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    accountNumber = await db.VwTransLines.Where(x => x.SeqNo.Equals(seqNo) && x.Debit != 0 && x.EditMode == false && x.TransStatus == "3" && x.Reversed == false).Select(x => x.AccountNo).SingleOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(accountNumber))
                    senderAccountNumber = accountNumber;

                return senderAccountNumber;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task FTOutwardDCTSQ(NIPFTOutwardDirectCreditReq responseDC)
        //{
        //    string beneficiaryBankName = string.Empty;

        //    try
        //    {
        //        if (responseDC != null)
        //        {
        //            var tsqrequest = new ViewTSQuerySingleRequest
        //            {
        //                ChannelCode = responseDC.ChannelCode,
        //                SessionID = responseDC.SessionID
        //            };

        //            var tsueryResponse = await nibbs.TSQuerySingleRequest(tsqrequest);

        //            if (tsueryResponse.ResponseCode == "00")
        //            {

        //                var outwardDirectCredit = new NIPFTOutwardDirectCreditReq();

        //                using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //                {
        //                    outwardDirectCredit = await db.NIPFTOutwardDirectCredit.Where(x => x.SessionID == responseDC.SessionID).SingleOrDefaultAsync();
        //                }

        //                outwardDirectCredit.TSQStatus = "POSTED"; //Mark transaction as reversed

        //                using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //                {
        //                    var local = db.Set<NIPFTOutwardDirectCreditReq>()
        //                             .Local
        //                             .FirstOrDefault(entry => entry.SessionID.Equals(outwardDirectCredit.SessionID));

        //                    if (local != null)
        //                    {

        //                        db.Entry(local).State = EntityState.Detached;
        //                    }

        //                    db.Update(outwardDirectCredit);
        //                    await db.SaveChangesAsync();
        //                }
        //            }
        //            else
        //            {

        //                var outwardDirectCredit = new NIPFTOutwardDirectCreditReq();

        //                using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //                {
        //                    outwardDirectCredit = await db.NIPFTOutwardDirectCredit.Where(x => x.SessionID == responseDC.SessionID).SingleOrDefaultAsync();
        //                }
        //                if (outwardDirectCredit.RetryCount < 3)
        //                {
        //                    outwardDirectCredit.TSQStatus = "WAITING"; //Mark transaction as reversed
        //                    outwardDirectCredit.Reversed = false;
        //                    outwardDirectCredit.RetryCount += 1;

        //                    using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //                    {
        //                        var local = db.Set<NIPFTOutwardDirectCreditReq>()
        //                                 .Local
        //                                 .FirstOrDefault(entry => entry.SessionID.Equals(outwardDirectCredit.SessionID));

        //                        if (local != null)
        //                        {

        //                            db.Entry(local).State = EntityState.Detached;
        //                        }

        //                        db.Update(outwardDirectCredit);
        //                        await db.SaveChangesAsync();
        //                    }
        //                }
        //                else
        //                {
        //                    outwardDirectCredit.TSQStatus = "FAILED"; //Mark transaction as reversed
        //                    outwardDirectCredit.Reversed = true;
        //                    outwardDirectCredit.RetryCount += 1;

        //                    using (var db = new EazyCoreContext(eazybank.Options, configuration))
        //                    {
        //                        var local = db.Set<NIPFTOutwardDirectCreditReq>()
        //                                 .Local
        //                                 .FirstOrDefault(entry => entry.SessionID.Equals(outwardDirectCredit.SessionID));

        //                        if (local != null)
        //                        {

        //                            db.Entry(local).State = EntityState.Detached;
        //                        }

        //                        db.Update(outwardDirectCredit);
        //                        await db.SaveChangesAsync();
        //                    }
        //                    var reversal = new ViewTransferReversal
        //                    {
        //                        CustNo = outwardDirectCredit.SessionID,
        //                        Narration = outwardDirectCredit.Narration,
        //                        ReferenceNo = outwardDirectCredit.SessionID
        //                    };

        //                    await FundTransferReversal(reversal);

        //                }

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        public async Task<Response> GetTransactionLine(string transID, string prodCode)
        {
            var transLine = new VwTransLines();

            using (var db = new EazyCoreContext(eazybank.Options, configuration))
            {
                transLine = await db.VwTransLines.Where(x => x.TransID == transID && x.ProdCode == prodCode && x.Credit != 0 && x.EditMode == false && x.TransStatus =="3" && x.Reversed == false).SingleOrDefaultAsync();
            }

            return new Response { ResponseCode = "00", ResponseResult = transLine };

        }
        public async Task<VwCreditMaster> GetCreditDetailByLoanId(string loanId)
        {
            var result = new VwCreditMaster();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwCredits.Where(x => x.CreditID == loanId).SingleOrDefaultAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<VwAccMaster> GetAccountByLedgerCodeandBranch(string ledgerCode, string branchCode)
        {
            var result = new VwAccMaster();
            var accountTypeList = new List<string>() { "1", "2","3" };
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.VwAccMaster.Where(x => x.LedgerChartCode == ledgerCode && x.BranchCode == branchCode && accountTypeList.Contains(x.AccountType)).SingleOrDefaultAsync();
                }

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Response> Loanbooking(ViewLoanBooking record)
        {
            var postingDate = new PostingDates();
            var acctMaster = new AcctMaster();
            var prodmaster = new ProdMaster();

            try
            {
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    postingDate = await db.PostingDates.FirstOrDefaultAsync(x => x.Record == 1);
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    acctMaster = await db.AcctMaster.FirstOrDefaultAsync(x => x.AccountNo == record.OperativeAccountNo);
                }

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    prodmaster = await db.ProdMaster.FirstOrDefaultAsync(x => x.ProdCode == record.ProdCode);
                }

                ViewNewAccountOpening loanAccount = new ViewNewAccountOpening
                {
                    CusID = acctMaster.CusId,
                    ProdCode = record.ProdCode,
                    BranchCode = acctMaster.BranchCode,
                    CusName = acctMaster.AccountDesc,
                    Arrears = false,
                    AccountNo = "",
                    AgentCode = ""
                };


                var loanAccountResponse = await NewAccountOpening(loanAccount);

                ViewNewAccountOpening arrearsAccount = new ViewNewAccountOpening
                {
                    CusID = acctMaster.CusId,
                    ProdCode = prodmaster.ArrearsProdCode,
                    BranchCode = acctMaster.BranchCode,
                    CusName = acctMaster.AccountDesc,
                    Arrears = true,
                    AccountNo = loanAccountResponse.ResponseResult.ToString(),
                    AgentCode = ""
                };

                var arreasAccountResponse = await NewAccountOpening(arrearsAccount);

                var creditMaster = new CreditMaster
                {
                    CreditId = record.CreditId,
                    Description = record.Description,
                    CreditType = record.CreditType,
                    OperativeAccountNo = record.OperativeAccountNo,
                    Overdraft = record.Overdraft,
                    ProdCode = record.ProdCode,
                    AccountNo = loanAccountResponse.ResponseResult.ToString(),
                    DisbursementAccountNo = record.DisbursementAccountNo,
                    ArrearsAccountNo = arreasAccountResponse.ResponseResult.ToString(),
                    AmountGranted = record.AmountGranted,
                    EquityContribution = record.EquityContribution,
                    DateApproved = record.DateApproved,
                    EffectiveDate = record.EffectiveDate,
                    TenorType = record.TenorType,
                    Tenor = record.Tenor,
                    MaturityDate = record.MaturityDate,
                    Rate = record.Rate,
                    ArrearsRate = record.ArrearsRate,
                    BaseYear = record.BaseYear,
                    AutoRepayArrears = false,
                    FirstDisbursementDate = record.FirstDisbursementDate,
                    FirstDisbursementAmount = record.FirstDisbursementAmount,
                    LastDisbursementDate = DateTime.Now,
                    RepaymentType = record.RepaymentType,
                    FixedRepaymentAmount = record.FixedRepaymentAmount,
                    FixedRepaymentStartDate = record.FixedRepaymentStartDate,
                    FixedRepaymentFrequency = string.IsNullOrEmpty(record.FixedRepaymentFrequency) ? "M" : record.FixedRepaymentFrequency,
                    FixedRepaymentNextDate = record.FixedRepaymentNextDate,
                    FixedRepaymentLastDate = record.FixedRepaymentLastDate,
                    FixedRepaymentArrearsTreatment = string.IsNullOrEmpty(record.FixedRepaymentArrearsTreatment) ? "02" : record.FixedRepaymentArrearsTreatment,
                    ConsentStatus = record.ConsentStatus,
                    PurposeType = record.PurposeType,
                    OwnershipId = record.OwnershipId,
                    SecurityCoverageCode = record.SecurityCoverageCode,
                    GuaranteeCoverageCode = record.GuaranteeCoverageCode,
                    LegalActionCode = record.LegalActionCode,
                    CreditCardNo = record.CreditCardNo,
                    OnLendingStatus = record.OnLendingStatus,
                    OnLendingCreditId = record.OnLendingCreditId,
                    Active = record.Active,
                    Memo = record.Memo, 
                    ObserveLimit = record.ObserveLimit,
                    MaxValueDate = record.MaxValueDate,
                    LastIntAppDate = record.LastIntAppDate,
                    PostingDateAdded = record.PostingDateAdded,
                    AddedBy = record.AddedBy,
                    BranchAdded = record.BranchAdded,
                    AddedApprovedBy = string.IsNullOrEmpty(record.AddedApprovedBy) ? record.AddedBy : record.AddedApprovedBy,
                    LastModifiedBy = record.LastModifiedBy,
                    LastApprovedBy = record.LastApprovedBy,
                    BranchAddedApproved = string.IsNullOrEmpty(record.BranchAddedApproved) ? acctMaster.BranchCode : record.BranchAddedApproved,
                    DateAdded = record.DateAdded,
                    DateAddedApproved = record?.DateAddedApproved,
                    PostingDateLastModified = record?.PostingDateLastModified,
                    DateLastModified = record?.DateLastModified,
                    DateLastApproved = record?.DateLastApproved,    
                    TimeAdded = record?.TimeAdded,
                    TimeAddedApproved = record?.TimeAddedApproved,
                    TimeLastModified = record?.TimeLastModified,
                    TimeLastApproved = record?.TimeLastApproved,
                    Migrated = record?.Migrated,
                    UnpaidPrincipal = record?.UnpaidPrincipal,
                    UnpaidInterest = record?.UnpaidInterest,
                    CapitalizeInterest = record?.CapitalizeInterest,    
                    EffectiveRate = record?.EffectiveRate,
                    CarryingAmount = record?.CarryingAmount,
                    UpfrontInterest = record?.UpfrontInterest,
                    SegmentId = record?.SegmentId,
                    DaysPastDue = record?.DaysPastDue,
                    DisbursedAmount = record?.DisbursedAmount,
                    TotalScheduledAmount = record?.TotalScheduledAmount,
                    Installments = record?.Installments,
                    AmountPaidToDate = record?.AmountPaidToDate,
                    OverdueEmi = record?.OverdueEmi,
                    ChargesDueToDate = record?.ChargesDueToDate,
                    TotalOverdue = record?.TotalOverdue,
                    AccruedIntBilled = record?.AccruedIntBilled,
                    AccruedIntNotBilled = record.AccruedIntNotBilled,
                    NetReceivable = record?.NetReceivable,
                    PayOffBalance = record.PayOffBalance,
                    LastDayAmountPaidToDate = record?.LastDayAmountPaidToDate,
                    LastDayAccruedIntBilled = record.LastDayAccruedIntBilled,
                    LoanDueMonth = record?.LoanDueMonth,
                    LoanRecoveredMonth = record?.LoanRecoveredMonth,
                    LoanNotRecoveredMonth = record?.LoanNotRecoveredMonth,
                    LoanRecoveredCumm = record?.LoanRecoveredCumm,
                    NextDueDate = record?.NextDueDate,
                    LastRepaymentDate = record?.LastRepaymentDate,
                    FundSourceCode = record?.FundSourceCode
                };

                //var customerTelephone = new EntTels
                //{
                //    Sequence = 0,
                //    EntityID = customerID,
                //    EntityTypeCode = "01",
                //    TelTypeCode = "01",
                //    TelNo = record.EntTels.TelNo,
                //    Extension = "",
                //    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    DateAdded = DateTime.Now.Date,
                //    DateLastModified = DateTime.Now.Date,
                //    TimeAdded = DateTime.Now.TimeOfDay,
                //    TimeLastModified = DateTime.Now.TimeOfDay,
                //    Remarks = record.FirstName
                //};

                //var customerEmail = new EntEmails
                //{
                //    Sequence = 0,
                //    EntityID = customerID,
                //    EntityTypeCode = "01",
                //    EmailAddr = record.EmailAddress,
                //    PrimaryAddr = true,
                //    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    DateAdded = DateTime.Now.Date,
                //    DateLastModified = DateTime.Now.Date,
                //    TimeAdded = DateTime.Now.TimeOfDay,
                //    TimeLastModified = DateTime.Now.TimeOfDay,
                //    Remarks = record.FirstName
                //};

                //var identification = new EntIdentities
                //{
                //    Sequence = 0,
                //    EntityID = customerID,
                //    EntityTypeCode = "01",
                //    IDTypeCode = record.Identification.IDTypeCode,
                //    IDNo = record.Identification.IDNo,
                //    Issuer = record.Identification.Issuer,
                //    ExpiryDate = string.IsNullOrEmpty(record.Identification.ExpiryDate.ToString()) ? null : record.Identification.ExpiryDate,
                //    PrimaryID = true,
                //    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    LastmodifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    DateAdded = DateTime.Now.Date,
                //    LastDateModified = DateTime.Now.Date,
                //    TimeAdded = DateTime.Now.TimeOfDay,
                //    LastTimeModified = DateTime.Now.TimeOfDay,
                //    IssueDate = string.IsNullOrEmpty(record.Identification.IssueDate.ToString()) ? null : record.Identification.IssueDate
                //};

                //CusRelParties nextOfKin = record.CusRelParty == null ? null : new CusRelParties
                //{
                //    Sequence = 0,
                //    CusID = customerID,
                //    CusRelTypeCode = record.CusRelParty.CusRelTypeCode,
                //    SurName = record.CusRelParty.SurName,
                //    FirstName = record.CusRelParty.FirstName,
                //    MiddleName = record.CusRelParty.MiddleName,
                //    Title = record.CusRelParty.Title,
                //    Gender = record.CusRelParty.Gender,
                //    MaritalStatus = record.CusRelParty.MaritalStatus,
                //    CountryCode = record.CusRelParty.CountryCode,
                //    StateCode = record.CusRelParty.StateCode,
                //    LocalGovtCode = record.CusRelParty.LocalGovtCode,
                //    BirthDate = record.CusRelParty.BirthDate,
                //    BirthPlace = record.CusRelParty.BirthPlace,
                //    EmployStatus = record.CusRelParty.EmployStatus,
                //    OccCode = record.CusRelParty.OccCode,
                //    PostingDateAdded = record.PostingDateAdded,
                //    AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //    DateAdded = DateTime.Now.Date,
                //    PostingDateLastModified = record.PostingDateAdded,
                //    DateLastModified = DateTime.Now.Date,
                //    TimeAdded = DateTime.Now.TimeOfDay,
                //    TimeLastModified = DateTime.Now.TimeOfDay,
                //    BvnNo = record.CusRelParty.BvnNo,
                //    PEP = record.CusRelParty.PEP
                //};
                //CusAddress address = record.CusAddress == null ? null :
                //    new CusAddress
                //    {
                //        Sequence = 0,
                //        EntityID = customerID,
                //        EntityTypeCode = "01",
                //        AddrTypeCode = record.CusAddress.AddrTypeCode,
                //        Street = record.CusAddress.Street,
                //        CountryCode = record.CusAddress.CountryCode,
                //        StateCode = record.CusAddress.StateCode,
                //        LocalGovtCode = record.CusAddress.LocalGovtCode,
                //        City = record.CusAddress.City,
                //        PostalCode = record.CusAddress.PostalCode,
                //        Dispatch = record.CusAddress.Dispatch,
                //        AddedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //        LastModifiedBy = eazCoreOptions.Value.UserID,//record.AddedBy,
                //        DateAdded = DateTime.Now.Date,
                //        DateLastModified = DateTime.Now.Date,
                //        TimeAdded = DateTime.Now.TimeOfDay,
                //        TimeLastModified = DateTime.Now.TimeOfDay
                //    };


                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    //await db.AddAsync(customer);
                    //if (customerTelephone != null)
                    //    await db.AddAsync(customerTelephone);
                    //if (customerEmail != null)
                    //    await db.AddAsync(customerEmail);
                    //if (record.Identification.IssueDate != null && record.Identification.ExpiryDate != null)
                    //    await db.AddAsync(identification);
                    //if (nextOfKin != null)
                    //    await db.AddAsync(nextOfKin);
                    //if (address != null)
                    await db.AddAsync(creditMaster);
                    await db.SaveChangesAsync();
                }

                return new Response
                {
                    ResponseCode = "00",
                    ResponseResult = creditMaster.CreditId
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ViewChargeBaseCode>> GetChargeBaseCodeLookup()
        {
            var result = new List<ViewChargeBaseCode>();
            try
            {

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    result = await db.ChargeBaseCodes.Where(x => x.TransCode == "506") //credit facility types
                                    .Select(x => new ViewChargeBaseCode 
                                    { ChargeBaseCode = x.ChargeBaseCode, 
                                        ChargeBaseDesc = x.ChargeBaseDesc 
                                    }).ToListAsync();
                }

                

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void WriteLog(string strMessage)
        {
            string fileName = "processingLog";
            try
            {
                var keyPath = Path.GetFullPath(configuration.GetSection("RecovaOptions:FileLocation").Value);
                //string fileName = $"{"plainText"}{DateTime.Now.ToString("yyyy-MM-dd")}";
                string fullFileName = $"{keyPath}{String.Format("\\" + fileName + "{0}.txt", DateTime.UtcNow.ToString("yyyy-MM-dd"))}";

                if (File.Exists(fullFileName))
                {
                    FileStream objFilestream = new FileStream(fullFileName, FileMode.Append, FileAccess.Write);
                    StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                    objStreamWriter.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+Text Seperator=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");
                    objStreamWriter.WriteLine($"request sent at {DateTime.UtcNow} ");
                    objStreamWriter.WriteLine(strMessage);
                    objStreamWriter.WriteLine();
                    objStreamWriter.Close();
                    objFilestream.Close();
                    //return true;

                }
                else
                {
                    FileStream objFilestream = new FileStream(fullFileName, FileMode.Create, FileAccess.Write);
                    StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                    objStreamWriter.WriteLine(strMessage);
                    objStreamWriter.Close();
                    objFilestream.Close();
                    //return true;

                }
            }
            catch (Exception ex)
            {
                var keyPath = Path.GetFullPath(configuration.GetSection("RecovaOptions:FileLocation").Value);
                //string fileName = $"{"plainText"}{DateTime.Now.ToString("yyyy-MM-dd")}";
                string filename = $"{keyPath}{String.Format("\\" + fileName + "{0}.txt", DateTime.UtcNow.ToString("yyyy-MM-dd"))}";


                FileStream objFilestream = new FileStream(filename, FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine($"{ex.Message} {" "}{ex.InnerException} {" "} {"occur"}");
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
            }
        }

    }
}
