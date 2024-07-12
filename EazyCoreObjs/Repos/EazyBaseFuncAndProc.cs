using EazyCoreObjs.Data;
using EazyCoreObjs.Interfaces;
using EazyCoreObjs.Models;
using EazyCoreObjs.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EazyCoreObjs.Repos
{
    public class EazyBaseFuncAndProc: IEazyBaseFuncAndProc
    {
        private readonly IConfiguration configuration;
        DbContextOptionsBuilder<EazyCoreContext> eazybank = new DbContextOptionsBuilder<EazyCoreContext>();

        public EazyBaseFuncAndProc(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>  
        /// Generate transaction ID by passing the appropriate numberID.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<int> GenerateAccountingEntries(string screenCode, string transID, string transCode)
        {
            try
            {
                var ScreenCode = new SqlParameter { ParameterName = "ScreenCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = screenCode };
                var TransID = new SqlParameter { ParameterName = "TransID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = transID };
                var TransCode = new SqlParameter { ParameterName = "TransCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = transCode };
                // Processing.  
                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    
                     result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpGenerateAccountingEntries] " +
                                    "@ScreenCode" + "," +
                                    "@TransID" + "," +
                                    "@TransCode", ScreenCode, TransID, TransCode);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Generate transaction ID by passing the appropriate numberID.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<int> TransHeaderApprove(string transID, bool approved, string branchAddedApproved, string workstationApproved, string workstationIPApproved)
        {
            try
            {

                var TransID = new SqlParameter { ParameterName = "TransID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = transID };
                var Approved = new SqlParameter { ParameterName = "Approved", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = approved };
                var BranchAddedApproved = new SqlParameter { ParameterName = "BranchAddedApproved", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = branchAddedApproved };

                var WorkstationApproved = new SqlParameter { ParameterName = "WorkstationApproved", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = workstationApproved };
                var WorkstationIPApproved = new SqlParameter { ParameterName = "WorkstationIPApproved", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = workstationIPApproved };
                var ReturnValue = new SqlParameter { ParameterName = "ReturnValue", DbType = DbType.Int16, Direction = ParameterDirection.Output };

                // Processing.  
                

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpTransHeaderApprove] " +
                                    "@TransID" + "," +
                                    "@Approved" + "," +
                                    "@BranchAddedApproved" + "," +
                                    "@WorkstationApproved" + "," +
                                    "@WorkstationIPApproved" + "," +
                                    "@ReturnValue OUTPUT", TransID, Approved, BranchAddedApproved, WorkstationApproved, WorkstationIPApproved, ReturnValue);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Generate transaction ID by passing the appropriate numberID.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>   
        public async Task<bool> TransApprovalLogApprove(string transID, string screenCode, string addedBy, string approvedBy, bool approved, string approveRejectReason, string branchAddedApproved, string workstationApproved, string workstationIPApproved, long sequence)
        {
            try
            {

                var TransID = new SqlParameter { ParameterName = "TransID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = transID };
                var ScreenCode = new SqlParameter { ParameterName = "ScreenCode", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = screenCode };
                var AddedBy = new SqlParameter { ParameterName = "AddedBy", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = addedBy };
                var ApprovedBy = new SqlParameter { ParameterName = "ApprovedBy", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = approvedBy };

                var Approved = new SqlParameter { ParameterName = "Approved", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = approved };
                var ApproveRejectReason = new SqlParameter { ParameterName = "ApproveRejectReason", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = approveRejectReason };
                var BranchAddedApproved = new SqlParameter { ParameterName = "BranchAddedApproved", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = branchAddedApproved };

                var WorkstationApproved = new SqlParameter { ParameterName = "WorkstationApproved", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = workstationApproved };
                var WorkstationIPApproved = new SqlParameter { ParameterName = "WorkstationIPApproved", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = workstationIPApproved };
                var Sequence = new SqlParameter { ParameterName = "Sequence", DbType = DbType.Int64, Direction = ParameterDirection.Input, Value = sequence } ;

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpTransApproval] " +
                                    "@TransID" + "," +
                                    "@ScreenCode" + "," +
                                    "@AddedBy" + "," +
                                    "@ApprovedBy" + "," +
                                    "@Approved" + "," +
                                    "@ApproveRejectReason" + "," +
                                    "@BranchAddedApproved" + "," +
                                    "@WorkstationApproved" + "," +
                                    "@WorkstationIPApproved" + "," +
                                    "@Sequence", TransID, ScreenCode, AddedBy,ApprovedBy,Approved,ApproveRejectReason,BranchAddedApproved,WorkstationApproved,WorkstationIPApproved, Sequence);
                }

                if (result > 0)
                    return true;
                else
                    return
                        false;
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }

        public async Task<string> GenerateNumberSequence(int numberID)
        {
            try
            {

                var NumberID = new SqlParameter { ParameterName = "NumberID", DbType = DbType.Int16, Direction = ParameterDirection.Input, Value = numberID };
                var GeneratedNumber = new SqlParameter { ParameterName = "GeneratedNumber", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Output };

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                     await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpGenerateNumberSequence] " +
                                    "@NumberID" + "," +
                                    "@GeneratedNumber OUTPUT", NumberID, GeneratedNumber);
                }

                return GeneratedNumber.Value.ToString(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Get account book balance by calling dbo.FnBookBal function .  
        /// </summary>  
        /// <param name="accountNo">account number expecting it's book balance</param> 
        /// <returns>Returns - a decimal numeric value</returns>  
        public async Task<decimal> GetFnBookBal(string accountNo)
        {

            try
            {

                var AccountNo = new SqlParameter { ParameterName = "AccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = accountNo };

                ViewFnBookBal sqlQuery = null;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    sqlQuery = await db.ViewFnBookBals
                                .FromSqlRaw("SELECT dbo.FnBookBal(@AccountNo) AS BookBal", AccountNo).SingleOrDefaultAsync();
                }

                return sqlQuery.BookBal;

                //string sqlQuery = "SELECT dbo.FnBookBal(@AccountNo) AS BookBal";

                //var record = this.Query<ViewFnBookBal>().FromSql(sqlQuery, usernameParam).SingleOrDefaultAsync().Result;

                //return record.BookBal;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Get account book balance by calling dbo.FnBookBal function .  
        /// </summary>  
        /// <param name="accountNo">account number expecting it's book balance</param> 
        /// <returns>Returns - a decimal numeric value</returns>  
        public async Task<decimal> GetFnAvailableBal(string accountNo)
        {

            try
            {
                var AccountNo = new SqlParameter { ParameterName = "AccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = accountNo };

                // Processing.  
                ViewFnAvailableBal sqlQuery = null;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    sqlQuery = await db.ViewFnAvailableBal
                                .FromSqlRaw("SELECT dbo.FnAvailableBal(@AccountNo) AS Available", AccountNo).SingleOrDefaultAsync();
                }

                return sqlQuery.Available;                
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Get Account Statement store procedure method.  
        /// </summary>  
        /// <param name="startDate">start date value parameter</param> 
        /// <param name="endDate">end date value parameter</param> 
        /// <param name="accountno">institution code value parameter</param> 
        /// <returns>Returns - List of product by ID</returns>  
        public async Task<List<VwAcctMasterStatement>> GetAcctMasterStatement(DateTime startDate, DateTime endDate, string accountno, bool printAll)
        {
            // Initialization.  
            List<VwAcctMasterStatement> lst = new List<VwAcctMasterStatement>();

            try
            {
                // Settings.                 
                var AccountNo = new SqlParameter("@AccountNo", accountno);
                var StartDate = new SqlParameter("@StartDate", Convert.ToDateTime(startDate));
                var EndDate = new SqlParameter("@EndDate", Convert.ToDateTime(endDate));
                var PrintAll = new SqlParameter("@PrintAll", printAll);

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    lst = await db.VwAcctMasterStatement
                                .FromSqlRaw("EXEC [dbo].[SpAcctMasterStatement] " +
                                    "@AccountNo" + "," +
                                    "@StartDate" + "," +
                                    "@EndDate" + ", " +
                                    "@PrintAll", AccountNo, StartDate, EndDate, PrintAll).ToListAsync();
                }



                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>  
        /// Get Account Statement store procedure method.  
        /// </summary>  
        /// <param name="startDate">start date value parameter</param> 
        /// <param name="endDate">end date value parameter</param> 
        /// <param name="accountno">institution code value parameter</param> 
        /// <returns>Returns - List of product by ID</returns>  
        public async Task<List<VwAcctMasterStatementMobileApp>> GetAcctMasterStatementMobileApp(DateTime startDate, DateTime endDate, string accountno, bool printAll)
        {
            // Initialization.  
            List<VwAcctMasterStatementMobileApp> lst = new List<VwAcctMasterStatementMobileApp>();

            try
            {
                var AccountNo = new SqlParameter("@AccountNo", accountno);
                var StartDate = new SqlParameter("@StartDate", Convert.ToDateTime(startDate));
                var EndDate = new SqlParameter("@EndDate", Convert.ToDateTime(endDate));
                var PrintAll = new SqlParameter("@PrintAll", printAll);

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    lst = await db.VwAcctMasterStatementMobileApp
                                .FromSqlRaw("EXEC [dbo].[SpAcctMasterStatementMobileApp] " +
                                    "@AccountNo" + "," +
                                    "@StartDate" + "," +
                                    "@EndDate" + ", " +
                                    "@PrintAll", AccountNo, StartDate, EndDate, PrintAll).ToListAsync();
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
        }


        ///// <summary>  
        ///// Get Account Statement store procedure method.  
        ///// </summary>  
        ///// <param name="startDate">start date value parameter</param> 
        ///// <param name="endDate">end date value parameter</param> 
        ///// <param name="accountno">institution code value parameter</param> 
        ///// <returns>Returns - List of product by ID</returns>  
        //public List<ViewAcctMasterStatementII> GetAcctMasterStatementII(DateTime startDate, DateTime endDate, string accountno, bool printAll)
        //{
        //    // Initialization.  
        //    List<ViewAcctMasterStatementII> lst = new List<ViewAcctMasterStatementII>();

        //    try
        //    {
        //        // Settings.                 
        //        var usernameParam = new[]{
        //                                new SqlParameter("@AccountNo",accountno),
        //                                new SqlParameter("@StartDate", Convert.ToDateTime(startDate)),
        //                                new SqlParameter("@EndDate",  Convert.ToDateTime(endDate)),
        //                                new SqlParameter("@PrintAll",  printAll),
        //                                };

        //        // Processing.  
        //        string sqlQuery = "EXEC [dbo].[SpAcctMasterStatementHistory] " +
        //                            "@AccountNo" + "," +
        //                            "@StartDate" + "," +
        //                            "@EndDate" + ", " +
        //                            "@PrintAll";

        //        lst = this.Query<ViewAcctMasterStatementII>().FromSql(sqlQuery, usernameParam).ToListAsync().Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    // Info.  
        //    return lst;
        //}



        ///// <summary>  
        ///// Get Account Statement store procedure method.  
        ///// </summary>  
        ///// <param name="startDate">start date value parameter</param> 
        ///// <param name="endDate">end date value parameter</param> 
        ///// <param name="accountno">institution code value parameter</param> 
        ///// <returns>Returns - List of product by ID</returns>  
        //public List<ViewAcctMasterMiniStatement> GetAcctMasterMiniStatement(string accountno)
        //{
        //    // Initialization.  
        //    List<ViewAcctMasterMiniStatement> lst = new List<ViewAcctMasterMiniStatement>();

        //    try
        //    {
        //        // Settings.                 
        //        var usernameParam = new[]{
        //                                new SqlParameter("@AccountNo",accountno)
        //                                };

        //        // Processing.  
        //        string sqlQuery = "EXEC [dbo].[SpAcctMasterMiniStatement] " +
        //                            "@AccountNo";

        //        lst = this.Query<ViewAcctMasterMiniStatement>().FromSql(sqlQuery, usernameParam).ToListAsync().Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    // Info.  
        //    return lst;
        //}

        /// <summary>  
        /// Get credit account details store procedure method.  
        /// </summary>          
        /// <param name="accountno">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<int> TransLinesCopy(VwTransLinesCopy transLines)
        {
            try
            {

                var TransIdFrom = new SqlParameter("@TransIdFrom", transLines.TransIdFrom);
                var TransIdTo = new SqlParameter("@TransIdTo", transLines.TransIdTo);
                var CopyOption = new SqlParameter("@CopyOption", transLines.CopyOption);
                var ClearBatch = new SqlParameter("@ClearBatch", transLines.ClearBatch);
                var Charge = new SqlParameter("@Charge", transLines.Charge);
                var ProdCode = new SqlParameter("@ProdCode", transLines.ProdCode);
                var Narration = new SqlParameter("@Narration", transLines.Narration);

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpTransLinesCopy] " +
                                    "@TransIdFrom" + "," +
                                    "@TransIdTo" + "," +
                                    "@CopyOption" + "," +
                                    "@ClearBatch" + "," +
                                    "@Charge" + "," +
                                    "@ProdCode" + "," +
                                    "@Narration", TransIdFrom, TransIdTo, CopyOption, ClearBatch,Charge, ProdCode, Narration);
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>  
        /// Get credit account details store procedure method.  
        /// </summary>          
        /// <param name="accountno">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<int> TransApproval(ViewTransApproval transApproval)
        {
            try
            {
                
                var TransID = new SqlParameter("@TransID", transApproval.TransID);
                var ScreenCode = new SqlParameter("@ScreenCode", transApproval.ScreenCode);
                var AddedBy = new SqlParameter("@AddedBy", transApproval.AddedBy);
                var ApprovedBy = new SqlParameter("@ApprovedBy", transApproval.ApprovedBy);
                var Approved = new SqlParameter("@Approved", transApproval.Approved);
                var ApproveRejectReason = new SqlParameter("@ApproveRejectReason", transApproval.ApproveRejectionReason);
                var BranchAddedApproved = new SqlParameter("@BranchAddedApproved", transApproval.BranchAddedApproved);
                var WorkstationApproved = new SqlParameter("@WorkstationApproved", transApproval.WorkstationApproved);
                var WorkstationIPApproved = new SqlParameter("@WorkstationIPApproved", transApproval.WorkstationIPApproved);
                var Sequence = new SqlParameter("@Sequence", transApproval.Sequence);
                // Processing.  

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpTransApproval] " +
                                    "@TransID" + "," +
                                    "@ScreenCode" + "," +
                                    "@AddedBy" + "," +
                                    "@ApprovedBy" + "," +
                                    "@Approved" + "," +
                                    "@ApproveRejectReason" + "," +
                                    "@BranchAddedApproved" + "," +
                                    "@WorkstationApproved" + "," +
                                    "@WorkstationIPApproved" + "," +
                                    "@Sequence", TransID, ScreenCode, AddedBy, ApprovedBy, Approved, ApproveRejectReason, BranchAddedApproved, WorkstationApproved, WorkstationIPApproved,Sequence);
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// Get account balance store procedure method.  
        /// </summary>          
        /// <param name="accountNo">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<VwAcctMasterBalance> GetAcctMasterBalance(string accountNo)
        {
            try
            {
                VwAcctMasterBalance record = null;
                var AccountNo = new SqlParameter("@AccountNo", accountNo);
                // Processing.  
                //string sqlQuery = "EXEC [dbo].[SpAcctMasterBalance] " +
                //                    "@AccountNo";

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    record = await db.VwAcctMasterBalance
                                .FromSqlRaw("EXEC [dbo].[SpAcctMasterBalance] " +
                                    "@AccountNo", AccountNo).SingleOrDefaultAsync();
                }           

                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>  
        /// Get Account Statement store procedure method.  
        /// </summary>  
        /// <param name="cusID">institution code value parameter</param> 
        /// <returns>Returns - List of product by ID</returns>  
        public async Task<List<VwAccMaster>> GetAcctMasterSelectByCustID(string cusID)
        {
            // Initialization.  
            List<VwAccMaster> records = new List<VwAccMaster>();

            try
            {
                // Settings.                 
                var CusID = new SqlParameter("@CusID", cusID);

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    records = await db.VwAccMaster
                                .FromSqlRaw("EXEC [dbo].[SpAcctMasterSelectByCustID] " +
                                    "@CusID", CusID).ToListAsync();
                }

                return records;
                //// Processing.  
                //string sqlQuery = "EXEC [dbo].[SpAcctMasterSelectByCustID] " +
                //                    "@CusID";

                //lst = this.Query<VwAccMaster>().FromSql(sqlQuery, usernameParam).ToListAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            //return lst;
        }

        /// <summary>  
        /// Get account balance store procedure method.  
        /// </summary>          
        /// <param name="cusID">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<VwCusMasterSelectCorrespondenceDetails> GetCusMasterSeelectCorrespondenceDetails(string cusID)
        {
            try
            {
                VwCusMasterSelectCorrespondenceDetails record = null;
                var CusID = new SqlParameter("@CusID", cusID);
                // Processing.  
                //string sqlQuery = ;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    record = await db.VwCusMasterSelectCorrespondenceDetails
                                .FromSqlRaw("EXEC [dbo].[SpCusMasterSeelectCorrespondenceDetails] " +
                                    "@CusID", CusID).SingleOrDefaultAsync();
                }

                //var record = this.Query<VwCusMasterSelectCorrespondenceDetails>().FromSql(sqlQuery, usernameParam).SingleOrDefaultAsync().Result;

                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>  
        /// Get customer details store procedure method.  
        /// </summary>          
        /// <param name="cusID">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<VwCusMaster> GetCusMasterSelectOneAsync(string cusID)
        {
            try
            {
                VwCusMaster record = null;
                var CusID = new SqlParameter("@CusID", cusID);
                // Processing.  
                //string sqlQuery = "EXEC [dbo].[SpCusMasterSelectOne] " +
                //                    "@CusID";
                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    record = await db.VwCusMaster
                                .FromSqlRaw("EXEC [dbo].[SpCusMasterSelectOne] " +
                                    "@CusID", CusID).SingleOrDefaultAsync();
                }
                //var record = VwCusMaster.FromSql(sqlQuery, usernameParam).SingleOrDefaultAsync().Result;

                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>  
        /// Generate transaction ID by passing the appropriate numberID.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<string> GenerateAccountNumber(string cusID, string branchCode, string prodCode)
        {
            try
            {

                var CusID = new SqlParameter { ParameterName = "CusID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = cusID };
                var BranchCode = new SqlParameter { ParameterName = "BranchCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = branchCode };
                var ProdCode = new SqlParameter { ParameterName = "ProdCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = prodCode };
                var Sequence = new SqlParameter { ParameterName = "Sequence", DbType = DbType.Int64, Direction = ParameterDirection.Output };
                var AccountNo = new SqlParameter { ParameterName = "AccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Output } ;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpGenerateAccountNumber] " +
                                    "@CusID" + "," +
                                    "@BranchCode" + "," +
                                    "@ProdCode" + "," +
                                    "@Sequence OUTPUT" + "," +
                                    "@AccountNo OUTPUT", CusID, BranchCode, ProdCode, Sequence, AccountNo);
                }

                return AccountNo.Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Get account book balance by calling dbo.FnBookBal function .  
        /// </summary>  
        /// <param name="accountNo">account number expecting it's book balance</param> 
        /// <returns>Returns - a decimal numeric value</returns>  
        public async Task<string> ProdMasterAccount(string prodCode, string branchCode, string code)
        {

            try
            {
                var ProdCode = new SqlParameter { ParameterName = "@ProdCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = prodCode };
                var BranchCode = new SqlParameter { ParameterName = "@BranchCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = branchCode };
                var Code = new SqlParameter { ParameterName = "@Code", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = code };

                // Processing.  
                //string sqlQuery = "SELECT dbo.FnProdMasterAccount(@ProdCode, @BranchCode, @Code) AS ProdAccount";
                ViewFnProdMasterAccount sqlQuery = null;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    sqlQuery = await db.ViewFnProdMasterAccount
                                .FromSqlRaw("SELECT dbo.FnProdMasterAccount(@ProdCode, @BranchCode, @Code) AS ProdAccount", ProdCode,BranchCode, Code).SingleOrDefaultAsync();
                }

                return sqlQuery.ProdAccount;

                //var record = this.Query<ViewFnProdMasterAccount>().FromSql(sqlQuery, usernameParam).SingleOrDefaultAsync().Result;

                //return record.ProdAccount;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Generate transaction ID by passing the appropriate numberID.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<int> GenerateRepaymentSchedule(VwRepaymentScheduleParam scheduleParam)
        {
            try
            {

                var TransID = new SqlParameter { ParameterName = "TransID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = scheduleParam.TransID };
                var CreditID = new SqlParameter { ParameterName = "CreditID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = scheduleParam.CreditID };
                var ScheduleType = new SqlParameter { ParameterName = "ScheduleType", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = scheduleParam.ScheduleType };
                var InterestRepayNextDate = new SqlParameter { ParameterName = "InterestRepayNextDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = scheduleParam.InterestRepayNextDate };
                var PrincipalRepayNextDate = new SqlParameter { ParameterName = "PrincipalRepayNextDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = scheduleParam.PrincipalRepayNextDate };
                var InterestRepayFreq = new SqlParameter { ParameterName = "InterestRepayFreq", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = scheduleParam.InterestRepayFreq };
                var PrincipalRepayFreq = new SqlParameter { ParameterName = "PrincipalRepayFreq", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = scheduleParam.PrincipalRepayFreq };
                var InterestRepayNumber = new SqlParameter { ParameterName = "InterestRepayNumber", DbType = DbType.Int16, Direction = ParameterDirection.Input, Value = scheduleParam.InterestRepayNumber };
                var PrincipalRepayNumber = new SqlParameter { ParameterName = "PrincipalRepayNumber", DbType = DbType.Int16, Direction = ParameterDirection.Input, Value = scheduleParam.PrincipalRepayNumber };
                var PrincipalAmount = new SqlParameter { ParameterName = "PrincipalAmount", DbType = DbType.Decimal, Direction = ParameterDirection.Input, Value = scheduleParam.PrincipalAmount };
                var InterestRate = new SqlParameter { ParameterName = "InterestRate", DbType = DbType.Decimal, Direction = ParameterDirection.Input, Value = scheduleParam.InterestRate };
                var EffectiveDate = new SqlParameter { ParameterName = "EffectiveDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = scheduleParam.EffectiveDate };
                var MaturityDate = new SqlParameter { ParameterName = "MaturityDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = scheduleParam.MaturityDate };
                var Base = new SqlParameter { ParameterName = "Base", DbType = DbType.Int16, Direction = ParameterDirection.Input, Value = scheduleParam.Base };
                var Simulation = new SqlParameter { ParameterName = "Simulation", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = scheduleParam.Simulation };
                var UseExistingAnnuityAmount = new SqlParameter { ParameterName = "UseExistingAnnuityAmount", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = scheduleParam.UseExistingAnnuityAmount };
                var StaggeredRepayment = new SqlParameter { ParameterName = "StaggeredRepayment", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = scheduleParam.StaggeredRepayment };

                //// Processing.  
                //string sqlQuery = "EXEC [dbo].[SpGenerateRepaymentSchedule] " +
                //                    "@TransID" + "," +
                //                    "@CreditID" + "," +
                //                    "@ScheduleType" + "," +
                //                    "@InterestRepayNextDate" + "," +
                //                    "@PrincipalRepayNextDate" + "," +
                //                    "@InterestRepayFreq" + "," +
                //                    "@PrincipalRepayFreq" + "," +
                //                    "@InterestRepayNumber" + "," +
                //                    "@PrincipalRepayNumber" + "," +
                //                    "@PrincipalAmount" + "," +
                //                    "@InterestRate" + "," +
                //                    "@EffectiveDate" + "," +
                //                    "@MaturityDate" + "," +
                //                    "@Base" + "," +
                //                    "@Simulation" + "," +
                //                    "@UseExistingAnnuityAmount" + "," +
                //                    "@StaggeredRepayment";

                //var record = Database.ExecuteSqlCommand(sqlQuery, usernameParam);
                ////var record = 
                ////var outputValue = (string)usernameParam[4].Value;
                //return record;

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpGenerateRepaymentSchedule] " +
                                    "@TransID" + "," +
                                    "@CreditID" + "," +
                                    "@ScheduleType" + "," +
                                    "@InterestRepayNextDate" + "," +
                                    "@PrincipalRepayNextDate" + "," +
                                    "@InterestRepayFreq" + "," +
                                    "@PrincipalRepayFreq" + "," +
                                    "@InterestRepayNumber" + "," +
                                    "@PrincipalRepayNumber" + "," +
                                    "@PrincipalAmount" + "," +
                                    "@InterestRate" + "," +
                                    "@EffectiveDate" + "," +
                                    "@MaturityDate" + "," +
                                    "@Base" + "," +
                                    "@Simulation" + "," +
                                    "@UseExistingAnnuityAmount" + "," +
                                    "@StaggeredRepayment", TransID, CreditID, ScheduleType, InterestRepayNextDate, PrincipalRepayNextDate,InterestRepayFreq, PrincipalRepayFreq,
                                    InterestRepayNumber, PrincipalRepayNumber, PrincipalAmount, InterestRate, EffectiveDate,MaturityDate, Base, Simulation,UseExistingAnnuityAmount,StaggeredRepayment);
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Generate transaction ID by passing the appropriate numberID.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<string> CreditTransSave(CreditTransSave creditTrans)
        {
            try
            {

                var TransID = new SqlParameter { ParameterName = "TransID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditTrans.TransID };
                var TransCode = new SqlParameter { ParameterName = "TransCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditTrans.TransCode };
                var ValueDate = new SqlParameter { ParameterName = "ValueDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = creditTrans.ValueDate };
                var AccountNo = new SqlParameter { ParameterName = "AccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditTrans.AccountNo };

                var ContraAccountNo = new SqlParameter { ParameterName = "ContraAccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditTrans.ContraAccountNo };
                var MainNarrative = new SqlParameter { ParameterName = "MainNarrative", DbType = DbType.String, Size = 70, Direction = ParameterDirection.Input, Value = creditTrans.MainNarrative };
                var ContraNarrative = new SqlParameter { ParameterName = "ContraNarrative", DbType = DbType.String, Size = 70, Direction = ParameterDirection.Input, Value = creditTrans.ContraNarrative };
                var Amount = new SqlParameter { ParameterName = "Amount", DbType = DbType.Decimal, Precision = 32, Scale = 2, Direction = ParameterDirection.Input, Value = creditTrans.Amount };

                var CurrCode = new SqlParameter { ParameterName = "CurrCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditTrans.CurrCode };
                var ExchRate = new SqlParameter { ParameterName = "ExchRate", DbType = DbType.Decimal, Size = 20, Precision = 32, Scale = 2, Direction = ParameterDirection.Input, Value = creditTrans.ExchRate };
                var ServiceIntFirst = new SqlParameter { ParameterName = "ServiceIntFirst", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = creditTrans.ServiceIntFirst };
                var DocumentNo = new SqlParameter { ParameterName = "DocumentNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditTrans.DocumentNo };

                var EditMode = new SqlParameter { ParameterName = "EditMode", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = creditTrans.EditMode };
                var TransStatus = new SqlParameter { ParameterName = "TransStatus", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = creditTrans.TransStatus };
                var Reversed = new SqlParameter { ParameterName = "Reversed", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = creditTrans.Reversed };
                var BranchAdded = new SqlParameter { ParameterName = "BranchAdded", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = creditTrans.BranchAdded };

                var AddedBy = new SqlParameter { ParameterName = "AddedBy", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = creditTrans.AddedBy };
                var WorkstationAdded = new SqlParameter { ParameterName = "WorkstationAdded", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = creditTrans.WorkstationAdded };
                var WorkstationIPAdded = new SqlParameter { ParameterName = "WorkstationIPAdded", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = creditTrans.WorkstationIPAdded };

                // Processing.  
                //string sqlQuery = "EXEC [dbo].[SpCreditTransSave] " +
                //                    "@TransID" + "," +
                //                    "@TransCode" + "," +
                //                    "@ValueDate" + "," +
                //                    "@AccountNo" + "," +
                //                    "@ContraAccountNo" + "," +
                //                    "@MainNarrative" + "," +
                //                    "@ContraNarrative" + "," +
                //                    "@Amount" + "," +
                //                    "@CurrCode" + "," +
                //                    "@ExchRate" + "," +
                //                    "@ServiceIntFirst" + "," +
                //                    "@DocumentNo" + "," +
                //                    "@EditMode" + "," +
                //                    "@TransStatus" + "," +
                //                    "@Reversed" + "," +
                //                    "@BranchAdded" + "," +
                //                    "@AddedBy" + "," +
                //                    "@WorkstationAdded" + "," +
                //                    "@WorkstationIPAdded";

                //var record = Database.ExecuteSqlCommand(sqlQuery, usernameParam);
                ////var record = 
                //var outputValue = (string)usernameParam[1].Value;
                //return outputValue;

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpCreditTransSave] " +
                                    "@TransID" + "," +
                                    "@TransCode" + "," +
                                    "@ValueDate" + "," +
                                    "@AccountNo" + "," +
                                    "@ContraAccountNo" + "," +
                                    "@MainNarrative" + "," +
                                    "@ContraNarrative" + "," +
                                    "@Amount" + "," +
                                    "@CurrCode" + "," +
                                    "@ExchRate" + "," +
                                    "@ServiceIntFirst" + "," +
                                    "@DocumentNo" + "," +
                                    "@EditMode" + "," +
                                    "@TransStatus" + "," +
                                    "@Reversed" + "," +
                                    "@BranchAdded" + "," +
                                    "@AddedBy" + "," +
                                    "@WorkstationAdded" + "," +
                                    "@WorkstationIPAdded", TransID, TransCode, ValueDate, AccountNo, ContraAccountNo,MainNarrative, ContraNarrative,Amount,CurrCode,
                                    ExchRate,ServiceIntFirst,DocumentNo,EditMode,TransStatus,Reversed,BranchAdded,AddedBy,WorkstationAdded,WorkstationIPAdded);
                }


                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Get customer details store procedure method.  
        /// </summary>          
        /// <param name="cusID">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<List<VwCreditMasterStatement>> CreditMasterStatement(string creditID, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<VwCreditMasterStatement> record = null;

                var CreditID = new SqlParameter { ParameterName = "CreditID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditID };
                var StartingDate = new SqlParameter { ParameterName = "StartingDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = startDate };
                var EndingDate = new SqlParameter { ParameterName = "EndingDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = endDate };


                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    record = await db.VwCreditMasterStatement
                                .FromSqlRaw("EXEC [dbo].[SpCreditMasterStatement] " +
                    "@CreditID" + "," +
                    "@StartingDate" + "," +
                    "@EndingDate", CreditID,StartingDate,EndingDate).ToListAsync();
                }

                //var record = VwCreditMasterStatement.FromSql(sqlQuery, usernameParam).ToListAsync().Result;

                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>  
        /// Generate transaction ID by passing the appropriate numberID.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<string> RetailFundsTrfLocalSave(ViewRetailFundsTrfLocalSave fundTrans)
        {
            try
            {

                var TransID = new SqlParameter { ParameterName = "TransID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.TransID };
                var TransCode = new SqlParameter { ParameterName = "TransCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.TransCode };
                var ValueDate = new SqlParameter { ParameterName = "ValueDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = fundTrans.ValueDate };

                var AccountNoDr = new SqlParameter { ParameterName = "AccountNoDr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.AccountNoDr };
                var NarrativeDr = new SqlParameter { ParameterName = "NarrativeDr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.NarrativeDr };
                var TelephoneDr = new SqlParameter { ParameterName = "TelephoneDr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.TelephoneDr };

                var AccountNoCr = new SqlParameter { ParameterName = "AccountNoCr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.AccountNoCr };
                var NarrativeCr = new SqlParameter { ParameterName = "NarrativeCr", DbType = DbType.String, Size = 70, Scale = 2, Direction = ParameterDirection.Input, Value = fundTrans.NarrativeCr };
                var TelephoneCr = new SqlParameter { ParameterName = "TelephoneCr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.TelephoneCr };

                var Amount = new SqlParameter { ParameterName = "Amount", DbType = DbType.Decimal, Precision = 32, Scale = 2, Direction = ParameterDirection.Input, Value = fundTrans.Amount };
                var CurrCode = new SqlParameter { ParameterName = "CurrCode", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.CurrCode };
                var ExchRate = new SqlParameter { ParameterName = "ExchRate", DbType = DbType.Decimal, Precision = 32, Scale = 2, Direction = ParameterDirection.Input, Value = fundTrans.ExchRate };
                var DocumentNo = new SqlParameter { ParameterName = "DocumentNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.DocumentNo };

                var BranchAdded = new SqlParameter { ParameterName = "BranchAdded", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = fundTrans.BranchAdded };
                var AddedBy = new SqlParameter { ParameterName = "AddedBy", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = fundTrans.AddedBy };
                var WorkstationAdded = new SqlParameter { ParameterName = "WorkstationAdded", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = fundTrans.WorkstationAdded };
                var WorkstationIPAdded = new SqlParameter { ParameterName = "WorkstationIPAdded", DbType = DbType.String, Size = 50, Direction = ParameterDirection.Input, Value = fundTrans.WorkstationIPAdded };

                var InstrumentNo = new SqlParameter { ParameterName = "InstrumentNo", DbType = DbType.Int16, Direction = ParameterDirection.Input, Value = (Int16)fundTrans.InstrumentNo };
                var AllocRuleCodeDr = new SqlParameter { ParameterName = "AllocRuleCodeDr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.AllocRuleCodeDr };
                var AllocRuleCodeCr = new SqlParameter { ParameterName = "AllocRuleCodeCr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.AllocRuleCodeCr };

                var InterbankTransfer = new SqlParameter { ParameterName = "InterbankTransfer", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = fundTrans.InterbankTransfer };
                var MISCodeDr = new SqlParameter { ParameterName = "MISCodeDr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.MISCodeDr };
                var MISCodeCr = new SqlParameter { ParameterName = "MISCodeCr", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.MISCodeCr };
                var RefAccountNo = new SqlParameter { ParameterName = "RefAccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = fundTrans.RefAccountNo };


                // Processing.  
                string sqlQuery = "EXEC [dbo].[SpRetailFundsTrfLocalSave] " +
                                    "@TransID" + "," +
                                    "@TransCode" + "," +
                                    "@ValueDate" + "," +

                                    "@AccountNoDr" + "," +
                                    "@NarrativeDr" + "," +
                                    "@TelephoneDr" + "," +

                                    "@AccountNoCr" + "," +
                                    "@NarrativeCr" + "," +
                                    "@TelephoneCr" + "," +

                                    "@Amount" + "," +
                                    "@CurrCode" + "," +
                                    "@ExchRate" + "," +
                                    "@DocumentNo" + "," +

                                    "@BranchAdded" + "," +
                                    "@AddedBy" + "," +
                                    "@WorkstationAdded" + "," +
                                    "@WorkstationIPAdded" + "," +

                                    "@InstrumentNo" + "," +
                                    "@AllocRuleCodeDr" + "," +
                                    "@AllocRuleCodeCr" + "," +

                                    "@InterbankTransfer" + "," +
                                    "@MISCodeDr" + "," +
                                    "@MISCodeCr" + "," +
                                    "@RefAccountNo";

                //var record = Database.ExecuteSqlCommand(sqlQuery, usernameParam);
                ////var record = 
                //var outputValue = (string)usernameParam[1].Value;
                //return outputValue;

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpRetailFundsTrfLocalSave] " +
                                    "@TransID" + "," +
                                    "@TransCode" + "," +
                                    "@ValueDate" + "," +

                                    "@AccountNoDr" + "," +
                                    "@NarrativeDr" + "," +
                                    "@TelephoneDr" + "," +

                                    "@AccountNoCr" + "," +
                                    "@NarrativeCr" + "," +
                                    "@TelephoneCr" + "," +

                                    "@Amount" + "," +
                                    "@CurrCode" + "," +
                                    "@ExchRate" + "," +
                                    "@DocumentNo" + "," +

                                    "@BranchAdded" + "," +
                                    "@AddedBy" + "," +
                                    "@WorkstationAdded" + "," +
                                    "@WorkstationIPAdded" + "," +

                                    "@InstrumentNo" + "," +
                                    "@AllocRuleCodeDr" + "," +
                                    "@AllocRuleCodeCr" + "," +

                                    "@InterbankTransfer" + "," +
                                    "@MISCodeDr" + "," +
                                    "@MISCodeCr" + "," +
                                    "@RefAccountNo", TransID, TransCode, ValueDate, ValueDate, AccountNoDr, NarrativeDr, TelephoneDr, AccountNoCr, NarrativeCr, TelephoneCr,
                                    Amount, CurrCode, ExchRate, DocumentNo, BranchAdded, AddedBy, WorkstationAdded, WorkstationIPAdded, InstrumentNo, AllocRuleCodeDr, AllocRuleCodeCr,
                                    InterbankTransfer, MISCodeDr, MISCodeCr, RefAccountNo);
                }


                return result.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Get credit account details store procedure method.  
        /// </summary>          
        /// <param name="accountno">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>   
        public async Task<int> TransHeadersSave(VwTransHeaderSave transHeader)
        {
            try
            {

                var TransID = new SqlParameter("@TransID", transHeader.TransID);
                var TransDesc = new SqlParameter("@TransDesc", transHeader.TransDesc);
                var CurrCode = new SqlParameter("@CurrCode", transHeader.CurrCode);
                var ExchRate = new SqlParameter("@ExchRate", transHeader.ExchRate);
                var ModuleCode = new SqlParameter("@ModuleCode", transHeader.ModuleCode);
                var ScreenCode = new SqlParameter("@ScreenCode", transHeader.ScreenCode);
                var BranchAdded = new SqlParameter("@BranchAdded", transHeader.BranchAdded);
                var ValueDate = new SqlParameter("@ValueDate", transHeader.ValueDate);
                var AddedBy = new SqlParameter("@AddedBy", transHeader.AddedBy);
                var WorkstationAdded = new SqlParameter("@WorkstationAdded", transHeader.WorkstationAdded);
                var WorkstationIPAdded = new SqlParameter("@WorkstationIPAdded", transHeader.WorkstationIPAdded);
                // Processing.  
                

                //return this.Database.ExecuteSqlCommand(sqlQuery, usernameParam);
                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpTransHeadersSave] " +
                                    "@TransID" + "," +
                                    "@TransDesc" + "," +
                                    "@CurrCode" + "," +
                                    "@ExchRate" + "," +
                                    "@ModuleCode" + "," +
                                    "@ScreenCode" + "," +
                                    "@BranchAdded" + "," +
                                    "@ValueDate" + "," +
                                    "@AddedBy" + "," +
                                    "@WorkstationAdded" + "," +
                                    "@WorkstationIPAdded", TransID, TransDesc, CurrCode,ExchRate,ModuleCode,ScreenCode,BranchAdded,ValueDate,AddedBy,WorkstationAdded,WorkstationIPAdded);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// Get credit account details store procedure method.  
        /// </summary>          
        /// <param name="accountno">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<int> MessagesLogSave(VwMessagesLogSave message)
        {
            try
            {

                var Sequence = new SqlParameter("@Sequence", message.Sequence);
                var MessageText = new SqlParameter("@MessageText", message.MessageText);
                var AccountNo = new SqlParameter("@AccountNo", message.AccountNo);
                var PhoneNo = new SqlParameter("@PhoneNo", message.PhoneNo);
                var EmailAddr = new SqlParameter("@EmailAddr", message.EmailAddr);
                var MessageType = new SqlParameter("@MessageType", message.MessageType);
                var TransID = new SqlParameter("@TransID", message.TransID);
                var TransLineNo = new SqlParameter("@TransLineNo", message.TransLineNo);
                var TransAmount = new SqlParameter("@TransAmount", message.TransAmount);
                var AvailBalance = new SqlParameter("@AvailBalance", message.AvailBalance);
                // Processing.  

                //return this.Database.ExecuteSqlCommand(sqlQuery, usernameParam);

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpMessagesLogSave] " +
                                    "@Sequence" + "," +
                                    "@MessageText" + "," +
                                    "@AccountNo" + "," +
                                    "@PhoneNo" + "," +
                                    "@EmailAddr" + "," +
                                    "@MessageType" + "," +
                                    "@TransID" + "," +
                                    "@TransLineNo" + "," +
                                    "@TransAmount" + "," +
                                    "@AvailBalance", Sequence, MessageText, AccountNo, PhoneNo, EmailAddr, MessageType, TransID, TransLineNo, TransAmount, AvailBalance);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// Get credit account details store procedure method.  
        /// </summary>          
        /// <param name="accountno">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  
        public async Task<int> SendMessageAlerts(string transID)
        {
            try
            {

                var TransID = new SqlParameter("@TransID", transID);
                                        
                // Processing.  
                string sqlQuery = "EXEC [dbo].[SpSendMessageAlerts] " +
                                    "@TransID";

                //return this.Database.ExecuteSqlCommand(sqlQuery, usernameParam);

                int result = 0;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    result = await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpSendMessageAlerts] " +
                                    "@TransID", TransID);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// Generate loan account number
        /// </summary>          
        /// <param name="accountno">end date value parameter</param> 
        /// <returns>Returns - List of properties of account</returns>  

        public async Task<string> GenerateCreditAccountNumber(string operativeAccountNo, string prodCode, string branchCode, bool overdraft)
        {
            try
            {

                var OperativeAccountNo = new SqlParameter { ParameterName = "OperativeAccountNo", DbType = DbType.String, Size = 15, Direction = ParameterDirection.Input, Value = operativeAccountNo };
                var ProdCode = new SqlParameter { ParameterName = "ProdCode", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = prodCode };
                var BranchCode = new SqlParameter { ParameterName = "BranchCode", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = branchCode };
                var Overdraft = new SqlParameter { ParameterName = "Overdraft", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = overdraft };
                var AccountNo = new SqlParameter { ParameterName = "AccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Output };

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {

                    await db.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpGenerateCreditAccountNumber] " +
                                   "@OperativeAccountNo" + "," +
                                   "@ProdCode" + "," +
                                   "@BranchCode" + "," +
                                   "@Overdraft" + "," +
                                   "@AccountNo OUTPUT", OperativeAccountNo, ProdCode, BranchCode,Overdraft, AccountNo);
                }

                return AccountNo.Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>  
        /// Get account book balance by calling dbo.FnBookBal function .  
        /// </summary>  
        /// <param name="accountNo">account number expecting it's book balance</param> 
        /// <returns>Returns - a decimal numeric value</returns>  
        public async Task<decimal> GetFnCreditBal(string accountNo)
        {

            try
            {

                var AccountNo = new SqlParameter { ParameterName = "AccountNo", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = accountNo };

                ViewFnCreditBal sqlQuery = null;

                using (var db = new EazyCoreContext(eazybank.Options, configuration))
                {
                    sqlQuery = await db.ViewFnCreditBals
                                .FromSqlRaw("SELECT dbo.FnCreditBal(@AccountNo) AS CurrBal", AccountNo).SingleOrDefaultAsync();
                }

                return sqlQuery.CurrBal;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
