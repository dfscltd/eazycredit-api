using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eazy.Credit.Security.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkflowP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AssignRoleToLimit",
                schema: "dbo",
                columns: table => new
                {
                    LimitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserRoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignRoleToLimit", x => new { x.LimitId, x.UserRoleID });
                });

            migrationBuilder.CreateTable(
                name: "AssignRoleToWorkflowLevel",
                schema: "dbo",
                columns: table => new
                {
                    LevelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkflowID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignRoleToWorkflowLevel", x => new { x.LevelID, x.WorkflowID, x.RoleID });
                });

            migrationBuilder.CreateTable(
                name: "CBNBankCode",
                columns: table => new
                {
                    CBNCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SortCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NIPBankCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CBNBankCode", x => x.CBNCode);
                });

            migrationBuilder.CreateTable(
                name: "ChargeBaseCodes",
                schema: "dbo",
                columns: table => new
                {
                    ChargeBaseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChargeBaseDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CeilingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tier = table.Column<bool>(type: "bit", nullable: false),
                    ChargeDebitTrans = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargeRateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargeAppFreq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargeAppMonth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargeAppDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentChargeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransCodeTax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargeContraGLAcct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargeTaxGLAcct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeLastModified = table.Column<TimeSpan>(type: "time", nullable: false),
                    ChargeBaseCodeType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeBaseCodes", x => x.ChargeBaseCode);
                });

            migrationBuilder.CreateTable(
                name: "CreditMaintHist",
                schema: "dbo",
                columns: table => new
                {
                    CreditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperativeAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overdraft = table.Column<bool>(type: "bit", nullable: false),
                    ProdCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisbursementAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountGranted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EquityContribution = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateApproved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tenor = table.Column<short>(type: "smallint", nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArrearsRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseYear = table.Column<short>(type: "smallint", nullable: false),
                    AutoRepayArrears = table.Column<bool>(type: "bit", nullable: false),
                    FirstDisbursementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstDisbursementAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastDisbursementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RepaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixedRepaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixedRepaymentStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FixedRepaymentFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixedRepaymentNextDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FixedRepaymentLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FixedRepaymentArrearsTreatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurposeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnershipId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityCoverageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuaranteeCoverageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalActionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnLendingStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnLendingCreditId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObserveLimit = table.Column<bool>(type: "bit", nullable: false),
                    EditMode = table.Column<bool>(type: "bit", nullable: true),
                    TransStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    WorkstationAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkstationIpadded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchApproved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostingDateApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAddedApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeAddedApproved = table.Column<TimeSpan>(type: "time", nullable: true),
                    WorkstationApproved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkstationIpapproved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapitalizeInterest = table.Column<bool>(type: "bit", nullable: false),
                    EffectiveRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarryingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpfrontInterest = table.Column<bool>(type: "bit", nullable: false),
                    SegmentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FundSourceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtEntry = table.Column<bool>(type: "bit", nullable: false),
                    ExtAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MisofficeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredRepaymentBankCBNCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredRepaymentAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditMaintHist", x => x.CreditId);
                });

            migrationBuilder.CreateTable(
                name: "CreditSchedulesHeadersMaint",
                schema: "dbo",
                columns: table => new
                {
                    CreditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRepayNextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrincipalRepayNextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterestRepayFreq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrincipalRepayFreq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRepayNumber = table.Column<int>(type: "int", nullable: false),
                    PrincipalRepayNumber = table.Column<int>(type: "int", nullable: false),
                    InterestArrearsTreatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrincipalArrearsTreatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChargesArrearsTreatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObserveLimit = table.Column<bool>(type: "bit", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingDateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeLastModified = table.Column<TimeSpan>(type: "time", nullable: false),
                    BaseYear = table.Column<short>(type: "smallint", nullable: false),
                    PrincipalMorat = table.Column<short>(type: "smallint", nullable: false),
                    PrincipalMoratFreq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestMorat = table.Column<short>(type: "smallint", nullable: false),
                    InterestMoratFreq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmortizeMoratoriumInterest = table.Column<bool>(type: "bit", nullable: false),
                    MoratoriumInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MoratoriumIntAppDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MoratoriumIntApplied = table.Column<bool>(type: "bit", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditSchedulesHeadersMaint", x => x.CreditId);
                });

            migrationBuilder.CreateTable(
                name: "CreditSchedulesLinesHist",
                schema: "dbo",
                columns: table => new
                {
                    Sequence = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargeBaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditSchedulesLinesHist", x => x.Sequence);
                });

            migrationBuilder.CreateTable(
                name: "CreditSchedulesLinesIntTempSim",
                schema: "dbo",
                columns: table => new
                {
                    Sequence = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditSchedulesLinesIntTempSim", x => x.Sequence);
                });

            migrationBuilder.CreateTable(
                name: "CreditSchedulesLinesSim",
                schema: "dbo",
                columns: table => new
                {
                    Sequence = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargeBaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditSchedulesLinesSim", x => x.Sequence);
                });

            migrationBuilder.CreateTable(
                name: "CreditScheduleSummary",
                columns: table => new
                {
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrincipal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrinPlusInt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Charges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CummRepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CummPrincipal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutstandPrincipal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CreditTrans",
                schema: "dbo",
                columns: table => new
                {
                    TransId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContraAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainNarrative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContraNarrative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceIntFirst = table.Column<bool>(type: "bit", nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditMode = table.Column<bool>(type: "bit", nullable: true),
                    TransStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reversed = table.Column<bool>(type: "bit", nullable: false),
                    BranchAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    WorkstationAdded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkstationIpadded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchApproved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostingDateApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAddedApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeAddedApproved = table.Column<TimeSpan>(type: "time", nullable: true),
                    WorkstationApproved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkstationIpapproved = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cancellation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditTran", x => x.TransId);
                });

            migrationBuilder.CreateTable(
                name: "GenerateRepaymentScheduleDto",
                columns: table => new
                {
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRepayNextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrincipalRepayNextDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterestRepayFreq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrincipalRepayFreq = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRepayNumber = table.Column<int>(type: "int", nullable: false),
                    PrincipalRepayNumber = table.Column<int>(type: "int", nullable: false),
                    PrincipalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Base = table.Column<short>(type: "smallint", nullable: false),
                    Simulation = table.Column<bool>(type: "bit", nullable: false),
                    UseExistingAnnuityAmount = table.Column<bool>(type: "bit", nullable: false),
                    StaggeredRepayment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OrgBankCodes",
                schema: "dbo",
                columns: table => new
                {
                    BankCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgBankCodes", x => x.BankCode);
                });

            migrationBuilder.CreateTable(
                name: "OrgBankInfo",
                schema: "dbo",
                columns: table => new
                {
                    Record = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgBankInfo", x => x.Record);
                });

            migrationBuilder.CreateTable(
                name: "OrgBranches",
                schema: "dbo",
                columns: table => new
                {
                    BranchCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CsuCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadOpsCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchMnemonic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AxDataAreaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowFcyTrans = table.Column<bool>(type: "bit", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeLastModified = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgBranches", x => x.BranchCode);
                });

            migrationBuilder.CreateTable(
                name: "PrmCountries",
                schema: "dbo",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmCountries", x => x.CountryCode);
                });

            migrationBuilder.CreateTable(
                name: "PrmNumberSequence",
                schema: "dbo",
                columns: table => new
                {
                    NumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextNumber = table.Column<int>(type: "int", nullable: false),
                    NumberLength = table.Column<byte>(type: "tinyint", nullable: false),
                    Separator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lockup = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmNumberSequence", x => x.NumberId);
                });

            migrationBuilder.CreateTable(
                name: "PrmRoleLimit",
                schema: "dbo",
                columns: table => new
                {
                    LimitId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LimitDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowerLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpperLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CummulativeLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmRoleLimit", x => x.LimitId);
                });

            migrationBuilder.CreateTable(
                name: "PrmRulesBoolean",
                schema: "dbo",
                columns: table => new
                {
                    Parameter = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: false),
                    ParameterDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmRulesBoolean", x => x.Parameter);
                });

            migrationBuilder.CreateTable(
                name: "PrmRulesNumbers",
                schema: "dbo",
                columns: table => new
                {
                    Parameter = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmRulesNumbers", x => x.Parameter);
                });

            migrationBuilder.CreateTable(
                name: "PrmRulesText",
                schema: "dbo",
                columns: table => new
                {
                    Parameter = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmRulesText", x => x.Parameter);
                });

            migrationBuilder.CreateTable(
                name: "PrmTypes",
                schema: "dbo",
                columns: table => new
                {
                    TypeCode = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDefined = table.Column<bool>(type: "bit", nullable: false),
                    LabelCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabelDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmTypes", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "PrmTypesDetails",
                schema: "dbo",
                columns: table => new
                {
                    TypeCode = table.Column<short>(type: "smallint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrmTypesDetails", x => new { x.TypeCode, x.Code });
                });

            migrationBuilder.CreateTable(
                name: "ProdMaster",
                columns: table => new
                {
                    ProdCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProdName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProdCatCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CurrCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('0')"),
                    DebitIntBaseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('000')"),
                    DebitIntAppFreq = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('M')"),
                    DebitIntAppMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    DebitIntAppDay = table.Column<byte>(type: "tinyint", nullable: false),
                    DebitIntCalcBalType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "((1))"),
                    DebitIntAppMethod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "((1))"),
                    CreditIntBaseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('000')"),
                    CreditIntAppFreq = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('M')"),
                    CreditIntAppMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    CreditIntAppDay = table.Column<byte>(type: "tinyint", nullable: false),
                    CreditIntCalcBalType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "((1))"),
                    CreditIntAppMethod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "((1))"),
                    COTChargeBaseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('000')"),
                    COTChargeAppFreq = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('M')"),
                    COTChargeAppMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    COTChargeAppDay = table.Column<byte>(type: "tinyint", nullable: false),
                    LastDebitIntAppDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "('01/01/2010')"),
                    LastCreditIntAppDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "('01/01/2010')"),
                    LastCOTAppDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "('01/01/2010')"),
                    AddedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    DateLastModified = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false, defaultValueSql: "(getdate())"),
                    TimeLastModified = table.Column<TimeSpan>(type: "time", nullable: false, defaultValueSql: "(getdate())"),
                    CreditClassSchemeCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('ZZZ')"),
                    ArrearsProdCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('ZZZ')"),
                    SegmentID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "('ZZZ')"),
                    PriorityOrder = table.Column<int>(type: "int", nullable: false),
                    TierCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('ZZZ')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdMaster_1", x => x.ProdCode);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowLevel",
                schema: "dbo",
                columns: table => new
                {
                    LevelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkflowID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkflowLevelTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowDocUpload = table.Column<bool>(type: "bit", nullable: false),
                    FinalLevel = table.Column<bool>(type: "bit", nullable: false),
                    LevelOrder = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowLevel", x => new { x.LevelID, x.WorkflowID });
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                schema: "dbo",
                columns: table => new
                {
                    WorkflowID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkflowTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkflowNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.WorkflowID);
                });

            migrationBuilder.CreateTable(
                name: "CreditChargesMaint",
                schema: "dbo",
                columns: table => new
                {
                    Sequence = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChargeBaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FreqCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextExecDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastExecDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Upfront = table.Column<bool>(type: "bit", nullable: true),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingDateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeLastModified = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditChargesMaint", x => x.Sequence);
                    table.ForeignKey(
                        name: "FK_CreditChargesMaint_CreditMaintHist_CreditId",
                        column: x => x.CreditId,
                        principalSchema: "dbo",
                        principalTable: "CreditMaintHist",
                        principalColumn: "CreditId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCollateral",
                schema: "dbo",
                columns: table => new
                {
                    SeqNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SecurityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityRefNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValuerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingDateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeLastModified = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForcedSaleValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityLocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityTitleCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCollateral", x => x.SeqNo);
                    table.ForeignKey(
                        name: "FK_CreditCollateral_CreditMaintHist_CreditId",
                        column: x => x.CreditId,
                        principalSchema: "dbo",
                        principalTable: "CreditMaintHist",
                        principalColumn: "CreditId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditGuarantorsMaint",
                schema: "dbo",
                columns: table => new
                {
                    SeqNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuarantorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuarantorFullNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainBusiness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessRegNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertIncorp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessTaxNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalConstitution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingDateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeAdded = table.Column<TimeSpan>(type: "time", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Liability = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BvnNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pep = table.Column<bool>(type: "bit", nullable: false),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditGuarantorsMaint", x => x.SeqNo);
                    table.ForeignKey(
                        name: "FK_CreditGuarantorsMaint_CreditMaintHist_CreditId",
                        column: x => x.CreditId,
                        principalSchema: "dbo",
                        principalTable: "CreditMaintHist",
                        principalColumn: "CreditId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditSchedulesLinesMaint",
                schema: "dbo",
                columns: table => new
                {
                    Sequence = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreditId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargeBaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditSchedulesLinesMaint", x => x.Sequence);
                    table.ForeignKey(
                        name: "FK_CreditSchedulesLinesMaint_CreditSchedulesHeadersMaint_TransId",
                        column: x => x.TransId,
                        principalSchema: "dbo",
                        principalTable: "CreditSchedulesHeadersMaint",
                        principalColumn: "CreditId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditChargesMaint_CreditId",
                schema: "dbo",
                table: "CreditChargesMaint",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCollateral_CreditId",
                schema: "dbo",
                table: "CreditCollateral",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditGuarantorsMaint_CreditId",
                schema: "dbo",
                table: "CreditGuarantorsMaint",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditSchedulesLinesMaint_TransId",
                schema: "dbo",
                table: "CreditSchedulesLinesMaint",
                column: "TransId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignRoleToLimit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AssignRoleToWorkflowLevel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CBNBankCode");

            migrationBuilder.DropTable(
                name: "ChargeBaseCodes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditChargesMaint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditCollateral",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditGuarantorsMaint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditSchedulesLinesHist",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditSchedulesLinesIntTempSim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditSchedulesLinesMaint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditSchedulesLinesSim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditScheduleSummary");

            migrationBuilder.DropTable(
                name: "CreditTran",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GenerateRepaymentScheduleDto");

            migrationBuilder.DropTable(
                name: "OrgBankCodes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrgBankInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrgBranches",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmCountries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmNumberSequence",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmRoleLimit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmRulesBoolean",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmRulesNumbers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmRulesText",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PrmTypesDetails",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ProdMaster");

            migrationBuilder.DropTable(
                name: "WorkflowLevel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Workflows",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditMaintHist",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CreditSchedulesHeadersMaint",
                schema: "dbo");
        }
    }
}
