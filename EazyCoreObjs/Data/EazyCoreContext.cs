using EazyCoreObjs.Models;
using EazyCoreObjs.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EazyCoreObjs.Data
{
    public class EazyCoreContext : DbContext 
    {
        private readonly string _connectionString;
        //private readonly IConfiguration configuration;
        public EazyCoreContext()
        {
        }

        public EazyCoreContext(DbContextOptions<EazyCoreContext> options) : base(options)
        {
        }

        public EazyCoreContext(DbContextOptions<EazyCoreContext> options, IConfiguration configuration) : base(options)
        {
            //_connectionString = ConfigurationExtensions.GetConnectionString(configuration, "EazyPushCon").ToString();
            _connectionString = configuration.GetConnectionString("EazybankCon").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var _connectionString = "Server=DLINKS;Database=SampleDBAX;Trusted_Connection=false;MultipleActiveResultSets=true;User ID=ApiLogin;Password=abc@123;";
                optionsBuilder.UseSqlServer(_connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    });
                base.OnConfiguring(optionsBuilder);
            }
        }


        public DbSet<AcctMaster> AcctMaster { get; set; }
        public DbSet<PostingDates> PostingDates { get; set; }
        public DbSet<PrmCurrencies> PrmCurrencies { get; set; }
        public DbSet<TransHeader> TransHeader { get; set; }
        public DbSet<TransLine> TransLine { get; set; }
        public DbSet<TransHist> TransHists { get; set; }
        public DbSet<AcctMaint> AcctMaint { get; set; }
        public DbSet<AcctMaintDetails> AcctMaintDetails { get; set; }
        public DbSet<PrmRulesText> PrmRulesText { get; set; }
        public DbSet<CusMaster> CusMaster { get; set; }
        public DbSet<SubscriptionAccounts> SubscriptionAccount { get; set; }
        public DbSet<OTPRequests> OTPRequest { get; set; }
        public DbSet<OTOValidationRequests> OTOValidationRequest { get; set; }
        public DbSet<ProdAcctsMapping> ProdAcctsMapping { get; set; }
        public DbSet<ProdMaster> ProdMaster { get; set; }
        public DbSet<ProdRulesText> ProdRulesText { get; set; }
        public DbSet<LedgerChart> LedgerChart { get; set; }
        public DbSet<SubscriptionAccounts> SubscriptionAccounts { get; set; }
        public DbSet<OrgBranches> OrgBranches { get; set; }
        public DbSet<CreditMaster> CreditMasters { get; set; }
        public DbSet<CreditGuarantors> CreditGuarantors { get; set; }
        public DbSet<CreditSecurities> CreditSecurities { get; set; }
        public DbSet<CreditTrans> CreditTrans { get; set; }
        public DbSet<CreditSchedulesLinesMaint> CreditSchedulesLinesMaint { get; set; }
        public DbSet<ParamTypesDetails> ParamTypesDetails { get; set; }
        public DbSet<ParamCountries> ParamCountries { get; set; }
        public DbSet<MMFixturesMaintHist> MMFixturesMaintHists { get; set; }
        public DbSet<MMFixturesMaster> MMFixturesMasters { get; set; }
        public DbSet<RetailFundsTrfLocal> RetailFundsTrfLocal { get; set; }
        public DbSet<TransApprovalLog> TransApprovalLog { get; set; }
        public DbSet<EntIdentities> EntIdentities { get; set; }
        public DbSet<ProdRulesNumbers> ProdRulesNumbers { get; set; }
        public DbSet<StandingOrdersMaintHist> StandingOrdersMaintHist { get; set; }
        public DbSet<StandingOrdersMaster> StandingOrdersMaster { get; set; }
        //public DbSet<AcctMandates> AcctMandates { get; set; }
        public DbSet<EntTels> EntTels { get; set; }
        public DbSet<EntEmails> EntEmails { get; set; }
        public DbSet<ProdInvestRateGrid> ProdInvestRateGrid { get; set; }
        public DbSet<AcctSMSNos> AcctSMSNos { get; set; }
        public DbSet<AcctEmailAddr> AcctEmailAddr { get; set; }
        public DbSet<CusAddress> CusAddress { get; set; }
        public DbSet<CusRelParties> CusRelParty { get; set; }
        public DbSet<AcctCardTrans> AcctCardTran { get; set; }
        public DbSet<AcctCardTransDetails> AcctCardTransDetail { get; set; }
        public DbSet<AcctCardMaster> AcctCardMaster { get; set; }
        public DbSet<PrmCardTypes> PrmCardType { get; set; }
        public DbSet<PrmDisputeType> PrmDisputeTypes { get; set; }
        public DbSet<DisputeLog> DisputeLogs { get; set; }
        public DbSet<OrgBankCode> OrgBankCodes { get; set; }
        public DbSet<TransactionReceipt> TransactionReceipts { get; set; }
        public DbSet<PrmRulesNumber> PrmRulesNumbers { get; set; }
        public DbSet<ChargeBaseCodes> ChargeBaseCodes { get; set; }
        public DbSet<NIPFinancialInstitution> NIPFinancialInstitutions { get; set; }
        public DbSet<NIPFTOutwardDirectCreditReq> NIPFTOutwardDirectCredit { get; set; }
        public DbSet<NIPFTInwardDirectCreditReq> NIPFTInwardDirectCreditReqs { get; set; }





        //View objects
        public DbSet<VwTransLines> VwTransLines { get; set; }
        public DbSet<VwAccMaster> VwAccMaster { get; set; }
        public DbSet<VwCusMaster> VwCusMaster { get; set; }
        public DbSet<VwCreditMaster> VwCredits { get; set; }
        public DbSet<VwAccMasterRelationship> VwAccMasterRelationship { get; set; }
        public DbSet<VwCreditMasterStatement> VwCreditMasterStatement { get; set; }
        public DbSet<VwAcctMasterStatement> VwAcctMasterStatement { get; set; }
        public DbSet<VwAcctMasterStatementMobileApp> VwAcctMasterStatementMobileApp { get; set; }

        public DbSet<VwAccEmailAddr> VwAccEmailAddr { get; set; }
        public DbSet<VwAccSMSNos> VwAccSMSNos { get; set; }
        public DbSet<VwEntAddress> VwEntAddress { get; set; }
        public DbSet<VwEntEmails> VwEntEmails { get; set; }
        public DbSet<VwEntTels> VwEntTels { get; set; }
        public DbSet<VwOrgBranches> VwOrgBranches { get; set; }
        public DbSet<VwAcctCardMaster> VwAcctCardMaster { get; set; }
        public DbSet<VwAcctCardTransDetails> VwAcctCardTransDetails { get; set; }
        public DbSet<VwDisputeLog> VwDisputeLog { get; set; }
        public DbSet<VwOrgBranchesCordinate> VwOrgBranchesCordinate { get; set; }
        public DbSet<VwProdMaster> VwProdMaster { get; set; }
        public DbSet<VwTransHist> VwTransHist { get; set; }

        public virtual DbSet<ViewFnBookBal> ViewFnBookBals { get; set; }
        public virtual DbSet<ViewFnCreditBal> ViewFnCreditBals { get; set; }
        public virtual DbSet<ViewFnAvailableBal> ViewFnAvailableBal  { get; set; }
        public virtual DbSet<VwAcctMasterBalance> VwAcctMasterBalance { get; set; }
        public virtual DbSet<VwCusMasterSelectCorrespondenceDetails> VwCusMasterSelectCorrespondenceDetails { get; set; }
        public virtual DbSet<ViewFnProdMasterAccount> ViewFnProdMasterAccount { get; set; }
        //public virtual DbSet<VwCreditMasterStatement> VwCreditMasterStatement { get; set; }
        public virtual DbSet<ViewRetailFundsTrfLocalSave> ViewRetailFundsTrfLocalSave { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcctMaster>().HasKey(k => k.AccountNo); // Fluet API composite key configuration
            modelBuilder.Entity<OTPRequests>().HasKey(k => k.Sequence);
            modelBuilder.Entity<OTOValidationRequests>().HasKey(k => k.Sequence);
            modelBuilder.Entity<SubscriptionAccounts>().HasKey(k => k.AccountNo);
            modelBuilder.Entity<ProdMaster>().HasKey(k => k.ProdCode);
            modelBuilder.Entity<ProdAcctsMapping>().HasKey(k => new { k.ProdCode, k.ProdAcctTypeCode });
            modelBuilder.Entity<PrmCurrencies>().HasKey(k => k.CurrCode);
            modelBuilder.Entity<PrmRulesText>().HasKey(k => k.Parameter);
            modelBuilder.Entity<PostingDates>().HasKey(k => k.Record);
            modelBuilder.Entity<TransHeader>().HasKey(k => k.TransID);
            modelBuilder.Entity<TransLine>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<TransHist>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<CusMaster>().HasKey(k => k.CusId);
            modelBuilder.Entity<AcctMaint>().HasKey(k => k.TransID);
            modelBuilder.Entity<AcctMaintDetails>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<ProdRulesText>().HasKey(k => new { k.ProdCode, k.ProdTextRulesNo });
            modelBuilder.Entity<LedgerChart>().HasKey(k => k.LedgerChartCode);
            modelBuilder.Entity<CreditMaster>().HasKey(k => k.CreditId);
            modelBuilder.Entity<CreditMaintHist>().HasKey(k => k.TransId);
            modelBuilder.Entity<CreditSchedulesHeaders>().HasKey(k => k.CreditId);
            modelBuilder.Entity<CreditSchedulesHeadersMaint>().HasKey(k => k.TransId);
            modelBuilder.Entity<CreditSecurities>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<CreditSchedulesLines>().HasKey(k => k.Sequence);
            modelBuilder.Entity<CreditGuarantors>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<CreditTrans>().HasKey(k => k.TransID);
            modelBuilder.Entity<CreditSchedulesLinesMaint>().HasKey(k => k.Sequence);
            modelBuilder.Entity<ParamTypesDetails>().HasKey(k => new { k.TypeCode, k.Code });
            modelBuilder.Entity<ParamCountries>().HasKey(k => k.CountryCode);
            modelBuilder.Entity<MMFixturesMaster>().HasKey(k => k.AccountNo);
            modelBuilder.Entity<MMFixturesMaintHist>().HasKey(k => k.TransID);
            modelBuilder.Entity<RetailFundsTrfLocal>().HasKey(k => k.TransID);
            modelBuilder.Entity<TransApprovalLog>().HasKey(k => k.Sequence);
            modelBuilder.Entity<EntIdentities>().HasKey(k => k.Sequence);
            modelBuilder.Entity<ProdRulesNumbers>().HasKey(k => new { k.ProdCode, k.ProdNumberRuleNo });
            modelBuilder.Entity<StandingOrdersMaintHist>().HasKey(k => k.TransID);
            modelBuilder.Entity<StandingOrdersMaster>().HasKey(k => k.StandingOrderNo);
            //modelBuilder.Entity<AcctMandates>().HasKey(k => k.Sequence);
            modelBuilder.Entity<EntTels>().HasKey(k => k.Sequence);
            modelBuilder.Entity<EntEmails>().HasKey(k => k.Sequence);
            modelBuilder.Entity<ProdInvestRateGrid>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<AcctSMSNos>().HasKey(k => k.Sequence);
            modelBuilder.Entity<AcctEmailAddr>().HasKey(k => k.Sequence);
            modelBuilder.Entity<CusRelParties>().HasKey(k => k.Sequence);
            modelBuilder.Entity<CusAddress>().HasKey(k => k.Sequence);
            modelBuilder.Entity<AcctCardMaster>().HasKey(k => k.Sequence);
            modelBuilder.Entity<AcctCardTrans>().HasKey(k => k.TransID);
            modelBuilder.Entity<AcctCardTransDetails>().HasKey(k => k.Sequence);
            modelBuilder.Entity<PrmCardTypes>().HasKey(k => k.CardType);
            modelBuilder.Entity<PrmDisputeType>().HasKey(k => k.DisputeCode);
            modelBuilder.Entity<DisputeLog>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<OrgBankCode>().HasKey(k => k.BankCode);
            modelBuilder.Entity<NIPFinancialInstitution>().HasKey(k => k.InstitutionCode);
            modelBuilder.Entity<NIPFTOutwardDirectCreditReq>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<TransactionReceipt>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<PrmRulesNumber>().HasKey(k => k.Parameter);
            modelBuilder.Entity<NIPFTInwardDirectCreditReq>().HasKey(k => k.SeqNo);
            modelBuilder.Entity<ChargeBaseCodes>().HasKey(k => k.ChargeBaseCode);

            modelBuilder.Entity<VwTransLines>().HasNoKey();
            modelBuilder.Entity<VwAccMaster>().HasNoKey();

            modelBuilder.Entity<VwCusMaster>().HasNoKey();
            modelBuilder.Entity<VwAccMasterRelationship>().HasNoKey();
            modelBuilder.Entity<VwCreditMasterStatement>().HasNoKey();
            modelBuilder.Entity<VwAcctMasterStatement>().HasNoKey();
            modelBuilder.Entity<VwAcctMasterStatementMobileApp>().HasNoKey();
            modelBuilder.Entity<VwAccEmailAddr>().HasNoKey();

            modelBuilder.Entity<VwAccSMSNos>().HasNoKey();
            modelBuilder.Entity<VwEntAddress>().HasNoKey();
            modelBuilder.Entity<VwEntEmails>().HasNoKey();
            modelBuilder.Entity<VwEntTels>().HasNoKey();
            modelBuilder.Entity<VwOrgBranches>().HasNoKey();
            modelBuilder.Entity<VwAcctCardMaster>().HasNoKey();

            modelBuilder.Entity<VwAcctCardTransDetails>().HasNoKey();
            modelBuilder.Entity<VwDisputeLog>().HasNoKey();
            modelBuilder.Entity<VwOrgBranchesCordinate>().HasNoKey();
            modelBuilder.Entity<VwProdMaster>().HasNoKey();
            modelBuilder.Entity<VwTransHist>().HasNoKey();

            modelBuilder.Entity<ViewFnBookBal>().HasNoKey();
            modelBuilder.Entity<ViewFnAvailableBal>().HasNoKey();
            modelBuilder.Entity<VwAcctMasterBalance>().HasNoKey();
            modelBuilder.Entity<VwCusMasterSelectCorrespondenceDetails>().HasNoKey();
            modelBuilder.Entity<ViewFnProdMasterAccount>().HasNoKey();
            modelBuilder.Entity<ViewRetailFundsTrfLocalSave>().HasNoKey();
            modelBuilder.Entity<VwCreditMaster>().HasNoKey();
            modelBuilder.Entity<ViewFnCreditBal>().HasNoKey();






            //modelBuilder.Query<ViewPFIUpcomingRepayments>();
            //modelBuilder.Query<ViewAcctMasterMiniStatement>();
            //modelBuilder.Query<ViewAcctMasterStatement>();
            //modelBuilder.Query<ViewAcctMasterStatementII>(); 


            //modelBuilder.Query<ViewFnBookBal>();
            //modelBuilder.Query<ViewFnAvailableBal>();
            //modelBuilder.Query<VwAcctMasterBalance>();
            //modelBuilder.Query<VwCusMasterSelectCorrespondenceDetails>();
            //modelBuilder.Query<ViewFnProdMasterAccount>();
            //modelBuilder.Query<VwCreditMasterStatement>();
            //modelBuilder.Query<ViewRetailFundsTrfLocalSave>();


        }

    }
}
