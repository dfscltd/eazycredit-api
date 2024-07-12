using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Microsoft.EntityFrameworkCore;


namespace Eazy.Credit.Security.Persistence.Data
{
    public class PersistenceContext: DbContext
    {
        public PersistenceContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<CreditCharge> CreditCharges { get; set; }
        public virtual DbSet<CreditGuarantor> CreditGuarantors { get; set; }
        public virtual DbSet<CreditSecurity> CreditSecurities { get; set; }
        public virtual DbSet<CreditMaintHist> CreditMaintHists { get; set; }
        public virtual DbSet<CreditSchedulesHeader> CreditSchedulesHeaders { get; set; }
        public virtual DbSet<CreditSchedulesLineHist> CreditSchedulesLines { get; set; }
        public virtual DbSet<CreditSchedulesLine> CreditSchedulesLinesMaints { get; set; }
        public virtual DbSet<CreditSchedulesLinesSim> CreditSchedulesLinesSims { get; set; }
        public virtual DbSet<CreditSchedulesLinesIntTempSim> CreditSchedulesLinesIntTempSims { get; set; }
        public virtual DbSet<CreditSchedulesLinesIntTemp> CreditSchedulesLinesIntTemps { get; set; }
        public virtual DbSet<PrmNumberSequence> PrmNumberSequences { get; set; }
        public virtual DbSet<PrmRulesBoolean> PrmRulesBooleans { get; set; }
        public virtual DbSet<PrmRoleLimit> PrmRoleLimts { get; set; }
        public virtual DbSet<PrmTypesDetail> PrmTypesDetails { get; set; }
        public virtual DbSet<PrmType> PrmTypes { get; set; }
        public virtual DbSet<PrmRulesText> PrmRulesTexts { get; set; }        
        public virtual DbSet<Workflows> Workflows { get; set; }
        public virtual DbSet<WorkflowLevel> WorkflowLevels { get; set; }
        public virtual DbSet<AssignRoleToLimit> AssignRoleToLimits { get; set; }
        public virtual DbSet<AssignRoleToWorkflowLevel> AssignRoleToWorkflowLevels {  get; set; }
        public virtual DbSet<ProdMaster> ProdMasters { get; set; }
        public virtual DbSet<PrmRulesNumber> PrmRulesNumbers { get; set; }
        public virtual DbSet<CBNBankCode> CBNBankCodes { get; set; }
        public virtual DbSet<ChargeBaseCodes> ChargeBaseCodes {  get; set; }
        public virtual DbSet<PrmCountry> PrmCountries { get; set; }
        public virtual DbSet<CreditTran> CreditTrans { get; set; }
        public virtual DbSet<OrgBankCode> OrgBankCodes { get; set; }
        public virtual DbSet<OrgBankInfo> OrgBankInfo { get; set; }
        public virtual DbSet<OrgBranch> OrgBranches { get; set; }
        public virtual DbSet<PrmCurrency> PrmCurrencies { get; set; }
        public  virtual DbSet<TransHeaderStage> TransHeaderStages { get; set; }
        public virtual DbSet<TransHeaderStatus> TransHeaderStatus { get; set; }
        public virtual DbSet<ActionTakenStatus> ActionTakenStatus { get; set; }
        public virtual DbSet<CreditScheduleSummaryDto> CreditScheduleSummary { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CreditCharge>(entity =>
            {
                entity.ToTable(name: "CreditChargesMaint", "dbo")
                .HasKey(c => c.Sequence);
            });

            builder.Entity<CreditGuarantor>(entity =>
            {
                entity.ToTable(name: "CreditGuarantorsMaint", "dbo")
                .HasKey(p => p.SeqNo);
            });

            builder.Entity<CreditMaintHist>(entity =>
            {
                entity.ToTable(name: "CreditMaintHist", "dbo")
                .HasKey(p => p.CreditId);

                entity.HasMany<CreditCharge>(f => f.CreditCharge)
                .WithOne(ad => ad.CreditMaintHist)
                .HasForeignKey(k => k.CreditId);

                entity.HasMany<CreditGuarantor>(f => f.CreditGuarantors)
                .WithOne(ad => ad.CreditMaintHist)
                .HasForeignKey(k => k.CreditId);

                entity.HasMany<CreditSecurity>(f => f.CreditSecurity)
                .WithOne(ad => ad.CreditMaintHist)
                .HasForeignKey(k => k.CreditId);
            });

            builder.Entity<CreditSecurity>(entity =>
            {
                entity.ToTable(name: "CreditCollateral", "dbo")
                .HasKey(p => p.SeqNo);
            });

            builder.Entity<CreditSchedulesHeader>(entity =>
            {
                entity.ToTable(name: "CreditSchedulesHeadersMaint", "dbo")
                .HasKey(p => p.CreditId);
            });

            builder.Entity<CreditSchedulesLineHist>(entity =>
            {
                entity.ToTable(name: "CreditSchedulesLinesHist", "dbo")
                .HasKey(p => p.Sequence);
            });
            builder.Entity<CreditSchedulesLine>(entity =>
            {
                entity.ToTable(name: "CreditSchedulesLinesMaint", "dbo")
                .HasKey(p => p.Sequence);
            });

            builder.Entity<PrmRoleLimit>(entity =>
            {
                entity.ToTable(name: "PrmRoleLimit", "dbo")
                .HasKey(p => p.LimitId);
            });

            builder.Entity<Workflows>(entity =>
            {
                entity.ToTable(name: "Workflows", "dbo")
                .HasKey(p => p.WorkflowID);
            });

            builder.Entity<WorkflowLevel>(entity =>
            {
                entity.ToTable(name: "WorkflowLevel", "dbo")
                .HasKey(p => new {p.LevelID, p.WorkflowID });
            });

            builder.Entity<PrmNumberSequence>(entity =>
            {
                entity.ToTable(name: "PrmNumberSequence", "dbo")
                .HasKey(p =>  p.NumberId);
            });

            builder.Entity<PrmRulesBoolean>(entity =>
            {
                entity.ToTable(name: "PrmRulesBoolean", "dbo")
                .HasKey(p => p.Parameter);
            });

            builder.Entity<PrmTypesDetail>(entity =>
            {
                entity.ToTable(name: "PrmTypesDetails", "dbo")
                .HasKey(p => new { p.TypeCode, p.Code });
            });

            builder.Entity<PrmType>(entity =>
            {
                entity.ToTable(name: "PrmTypes", "dbo")
                .HasKey(p => p.TypeCode);
            });

            builder.Entity<PrmRulesText>(entity =>
            {
                entity.ToTable(name: "PrmRulesText", "dbo")
                .HasKey(p => p.Parameter);
            });

            builder.Entity<AssignRoleToLimit>(entity =>
            {
                entity.ToTable(name: "AssignRoleToLimit", "dbo")
                .HasKey(p => new { p.LimitId, p.UserRoleID });
            });

            builder.Entity<AssignRoleToWorkflowLevel>(entity =>
            {
                entity.ToTable(name: "AssignRoleToWorkflowLevel", "dbo")
                .HasKey(p => new { p.LevelID, p.WorkflowID, p.RoleID });
            });

            builder.Entity<CBNBankCode>(entity =>
            {
                entity.HasKey(e => e.CBNCode);

                entity.ToTable("CBNBankCode");

                entity.Property(e => e.CBNCode).HasMaxLength(0);

                entity.Property(e => e.InstitutionName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ShortName)
                    .HasMaxLength(50);

                entity.Property(e => e.SortCode)
                    .HasMaxLength(50);

                entity.Property(e => e.NIPBankCode)
                    .HasMaxLength(50);
            });

            builder.Entity<ProdMaster>(entity =>
            {
                entity.HasKey(e => e.ProdCode)
                    .HasName("PK_ProdMaster_1");

                entity.ToTable("ProdMaster");

                entity.Property(e => e.ProdCode).HasMaxLength(10);

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ArrearsProdCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('ZZZ')");

                entity.Property(e => e.CotchargeAppDay).HasColumnName("COTChargeAppDay");

                entity.Property(e => e.CotchargeAppFreq)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("COTChargeAppFreq")
                    .HasDefaultValueSql("('M')")
                    .IsFixedLength(true);

                entity.Property(e => e.CotchargeAppMonth).HasColumnName("COTChargeAppMonth");

                entity.Property(e => e.CotchargeBaseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("COTChargeBaseCode")
                    .HasDefaultValueSql("('000')");

                entity.Property(e => e.CreditClassSchemeCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('ZZZ')");

                entity.Property(e => e.CreditIntAppFreq)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('M')")
                    .IsFixedLength(true);

                entity.Property(e => e.CreditIntAppMethod)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreditIntBaseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('000')");

                entity.Property(e => e.CreditIntCalcBalType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CurrCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DebitIntAppFreq)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('M')")
                    .IsFixedLength(true);

                entity.Property(e => e.DebitIntAppMethod)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DebitIntBaseCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('000')");

                entity.Property(e => e.DebitIntCalcBalType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastCotappDate)
                    .HasColumnType("date")
                    .HasColumnName("LastCOTAppDate")
                    .HasDefaultValueSql("('01/01/2010')");

                entity.Property(e => e.LastCreditIntAppDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('01/01/2010')");

                entity.Property(e => e.LastDebitIntAppDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('01/01/2010')");

                entity.Property(e => e.LastModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProdCatCode).HasMaxLength(10);

                entity.Property(e => e.ProdName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SegmentId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SegmentID")
                    .HasDefaultValueSql("('ZZZ')");

                entity.Property(e => e.TierCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('ZZZ')");

                entity.Property(e => e.TimeAdded).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TimeLastModified).HasDefaultValueSql("(getdate())");
            });
            
            builder.Entity<PrmRulesNumber>(entity =>
            {
                entity.ToTable(name: "PrmRulesNumbers", "dbo")
                .HasKey(p => p.Parameter);
            });

            builder.Entity<ChargeBaseCodes>(entity =>
            {
                entity.ToTable(name: "ChargeBaseCodes", "dbo")
                .HasKey(p => p.ChargeBaseCode);
            });

            builder.Entity<PrmCountry>(entity =>
            {
                entity.ToTable(name: "PrmCountries", "dbo")
                .HasKey(p => p.CountryCode);
            });
         
            builder.Entity<CreditTran>(entity =>
            {
                entity.ToTable(name: "CreditTrans", "dbo")
                .HasKey(p => p.TransId);
            });

            builder.Entity<CreditSchedulesLinesSim>(entity =>
            {
                entity.ToTable(name: "CreditSchedulesLinesSim", "dbo")
                .HasKey(p => p.Sequence);
            });

            builder.Entity<CreditSchedulesLinesIntTempSim>(entity =>
            {
                entity.ToTable(name: "CreditSchedulesLinesIntTempSim", "dbo")
                .HasKey(p => p.Sequence);
            });

            builder.Entity<CreditSchedulesLinesIntTemp>(entity =>
            {
                entity.ToTable(name: "CreditSchedulesLinesIntTemp", "dbo")
                .HasKey(p => p.Sequence);
            });


            builder.Entity<OrgBankCode>(entity =>
            {
                entity.ToTable(name: "OrgBankCodes", "dbo")
                .HasKey(p => p.BankCode);
            });

            builder.Entity<OrgBankInfo>(entity =>
            {
                entity.ToTable(name: "OrgBankInfo", "dbo")
                .HasKey(p => p.Record);
            });

            builder.Entity<OrgBranch>(entity =>
            {
                entity.ToTable(name: "OrgBranches", "dbo")
                .HasKey(p => p.BranchCode);
            });

            builder.Entity<TransHeaderStage>(entity =>
            {
                entity.ToTable(name: "TransHeaderStages", "dbo")
                .HasKey(p => new { p.CreditID, p.Workflow, p.Workflowlevel });
            });

            builder.Entity<TransHeaderStatus>(entity =>
            {
                entity.ToTable(name: "TransHeaderStatus", "dbo")
                .HasKey(p => p.StatusCode);
            });

            builder.Entity<ActionTakenStatus>(entity =>
            {
                entity.ToTable(name: "ActionTakenStatus", "dbo")
                .HasKey(p =>  p.ActionCode);
            });
           
            builder.Entity<PrmCurrency>(entity =>
            {
                entity.ToTable(name: "PrmCurrencies", "dbo")
                .HasKey(p => p.CurrCode);
            });


            builder.Entity<GenerateRepaymentScheduleDto>().HasNoKey();
           
            builder.Entity<CreditScheduleSummaryDto>().HasNoKey();
        }

    }
}
