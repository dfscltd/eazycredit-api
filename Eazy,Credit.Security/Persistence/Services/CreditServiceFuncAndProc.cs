using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Persistence.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Eazy.Credit.Security.Persistence.Services
{
    public class CreditServiceFuncAndProc: ICreditServiceFuncAndProc
    {
        public PersistenceContext context;
        //private readonly IConfiguration configuration;
        public CreditServiceFuncAndProc(PersistenceContext context)
        {
            this.context = context;
        }


        /// <summary>  
        /// Generate loan repayment schedule.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<int> GenerateRepaymentSchedule(GenerateRepaymentScheduleDto repaymentSchedule)
        {
            try
            {
                var TransID = new SqlParameter { ParameterName = "TransID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = repaymentSchedule.TransID };
                var CreditID = new SqlParameter { ParameterName = "CreditID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = repaymentSchedule.CreditID };
                var ScheduleType = new SqlParameter { ParameterName = "ScheduleType", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = repaymentSchedule.ScheduleType };

                var InterestRepayNextDate = new SqlParameter { ParameterName = "InterestRepayNextDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = repaymentSchedule.InterestRepayNextDate };
                var PrincipalRepayNextDate = new SqlParameter { ParameterName = "PrincipalRepayNextDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = repaymentSchedule.PrincipalRepayNextDate };
                var InterestRepayFreq = new SqlParameter { ParameterName = "InterestRepayFreq", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = repaymentSchedule.InterestRepayFreq };

                var PrincipalRepayFreq = new SqlParameter { ParameterName = "PrincipalRepayFreq", DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, Value = repaymentSchedule.PrincipalRepayFreq };
                var InterestRepayNumber = new SqlParameter { ParameterName = "InterestRepayNumber", DbType = DbType.Int32, Direction = ParameterDirection.Input, Value = repaymentSchedule.InterestRepayNumber };
                var PrincipalRepayNumber = new SqlParameter { ParameterName = "PrincipalRepayNumber", DbType = DbType.Int32, Direction = ParameterDirection.Input, Value = repaymentSchedule.PrincipalRepayNumber };

                //var PrincipalAmount = new SqlParameter { ParameterName = "PrincipalAmount", DbType = DbType.Decimal, Scale = 38, Precision = 2, Direction = ParameterDirection.Input, Value = repaymentSchedule.PrincipalAmount };
                //var InterestRate = new SqlParameter { ParameterName = "InterestRate", DbType = DbType.Decimal, Scale = 18, Precision = 2, Direction = ParameterDirection.Input, Value = repaymentSchedule.InterestRate };
                var PrincipalAmount = new SqlParameter { ParameterName = "PrincipalAmount", Direction = ParameterDirection.Input, Value = repaymentSchedule.PrincipalAmount };
                var InterestRate = new SqlParameter { ParameterName = "InterestRate", Direction = ParameterDirection.Input, Value = repaymentSchedule.InterestRate };
                var EffectiveDate = new SqlParameter { ParameterName = "EffectiveDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = repaymentSchedule.EffectiveDate };

                var MaturityDate = new SqlParameter { ParameterName = "MaturityDate", DbType = DbType.Date, Direction = ParameterDirection.Input, Value = repaymentSchedule.MaturityDate };
                var Base = new SqlParameter { ParameterName = "Base", DbType = DbType.Int32, Direction = ParameterDirection.Input, Value = repaymentSchedule.Base };
                var Simulation = new SqlParameter { ParameterName = "Simulation", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = repaymentSchedule.Simulation };

                var UseExistingAnnuityAmount = new SqlParameter { ParameterName = "UseExistingAnnuityAmount", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = repaymentSchedule.UseExistingAnnuityAmount };
                var StaggeredRepayment = new SqlParameter { ParameterName = "StaggeredRepayment", DbType = DbType.Boolean, Direction = ParameterDirection.Input, Value = repaymentSchedule.StaggeredRepayment };

                // Processing.  
                int result = 0;



                result = await context.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpGenerateRepaymentSchedule] " +
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
                                   "@StaggeredRepayment", TransID, CreditID, ScheduleType, InterestRepayNextDate, PrincipalRepayNextDate, InterestRepayFreq, PrincipalRepayFreq,
                                                        InterestRepayNumber, PrincipalRepayNumber, PrincipalAmount, InterestRate, EffectiveDate, MaturityDate, Base, Simulation,
                                                        UseExistingAnnuityAmount, StaggeredRepayment);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        /// <summary>  
        /// Generate loan repayment schedule.  
        /// </summary>  
        /// <param name="numberID">NumberID parameter</param> 
        /// <returns>Returns - a generated TransID</returns>  
        public async Task<List<CreditScheduleSummaryDto>> CreditSchedulesLinesSelectSummary(string creditID, string status)
        {
            try
            {
                List<CreditScheduleSummaryDto> lst = new List<CreditScheduleSummaryDto>();

                var CreditID = new SqlParameter { ParameterName = "CreditID", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = creditID };
                var Status = new SqlParameter { ParameterName = "Status", DbType = DbType.String, Size = 20, Direction = ParameterDirection.Input, Value = status };

                // Processing.  

                lst = await context.CreditScheduleSummary.FromSqlRaw("EXEC [dbo].[SpCreditSchedulesLinesSelectSummary] " +
                                   "@CreditID" + "," +
                                   "@Status", CreditID, Status).ToListAsync();

                return lst;
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



                await context.Database.ExecuteSqlRawAsync("EXEC [dbo].[SpGenerateNumberSequence] " +
                               "@NumberID" + "," +
                               "@GeneratedNumber OUTPUT", NumberID, GeneratedNumber);

                return GeneratedNumber.Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
