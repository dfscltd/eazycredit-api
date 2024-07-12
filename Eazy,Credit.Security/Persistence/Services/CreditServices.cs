using Eazy.Credit.Security.Contracts.Persistence;
using Eazy.Credit.Security.Dtos;
using Eazy.Credit.Security.Entities;
using Eazy.Credit.Security.Persistence.Data;
using EazyCoreObjs.Interfaces;
using EazyCoreObjs.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System;

namespace Eazy.Credit.Security.Persistence.Services
{
    public class CreditServices : ICreditServices
    {
        private readonly PersistenceContext db;
        private readonly ICreditServiceFuncAndProc eazyCoreFun;
        private readonly IEazyCore eazyCore;
        private readonly IEazyBaseFuncAndProc coreFun;
        private readonly ICreateTransHeaderStageService headerStageService;

        public CreditServices(PersistenceContext db, ICreditServiceFuncAndProc eazyCoreFun, IEazyCore eazyCore, IEazyBaseFuncAndProc coreFun, ICreateTransHeaderStageService headerStageService)
        {
            this.db = db;
            this.eazyCoreFun = eazyCoreFun;
            this.eazyCore = eazyCore;
            this.coreFun = coreFun;
            this.headerStageService = headerStageService;
        }

        public async Task<ViewAPIResponse<List<CreditScheduleSummaryDto>>> LoanBooking(LoanApplicationRequestDto request)
        {
            ViewAPIResponse<List<CreditScheduleSummaryDto>> response = null;
            VwAccMaster accMaster;
            List< CreditScheduleSummaryDto > scheduleResponse = null;
            try
            {
                string json = JsonConvert.SerializeObject(request);

                //WriteLogFromPortal(json);

                accMaster = await eazyCore.GetCustomerAccountByAccountNo(request.OperativeAccount);


                if (accMaster == null)
                {
                    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "operative account number does not exist in CBA",
                        ResponseResult = null
                    };

                    return response; ;
                }

                //if (!await db.CBNBankCodes.AnyAsync(x => x.CBNCode == request.PreferredRepaymentBankCBNCode))
                //{
                //    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                //    {
                //        ResponseCode = "01",
                //        ResponseMessage = $"{"The provide Preferred Repayment BankCBNCode of "}{request.PreferredRepaymentBankCBNCode}{" is invalid"}",
                //        ResponseResult = null
                //    };

                //    return response;
                //}

                if (!await db.ProdMasters.AnyAsync(x => x.ProdCode == request.LoanProduct))
                {
                    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = $"{"The provide loan product of "}{request.LoanProduct}{" is invalid"}",
                        ResponseResult = null
                    };

                    return response;
                }

                //if (await db.CreditMaintHists.AnyAsync(x => x.ExtAccountNo == request.CreditIDRef && x.EditMode == false))
                //{
                //    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                //    {
                //        ResponseCode = "01",
                //        ResponseMessage = "fail",
                //        ResponseResult = null
                //    };

                //    return response;
                //}

                string temCreditId = await eazyCoreFun.GenerateNumberSequence(4002);
                string transID = await eazyCoreFun.GenerateNumberSequence(4003);

                string creditId = $"{temCreditId}{"01"}";

                string loanAccount = await coreFun.GenerateCreditAccountNumber(request.OperativeAccount, "", "", false);

                var creditMaintHist = new CreateCreditMaintHistDto
                {
                    TransId = transID,
                    PostingDate = DateTime.Now,
                    CreditId = creditId,
                    Description = request.FacilityDescription,
                    CreditType = request.CreditType,
                    OperativeAccountNo = request.OperativeAccount,
                    Overdraft = false,
                    ProdCode = request.LoanProduct,
                    AccountNo = loanAccount,
                    DisbursementAccountNo = request.OperativeAccount,
                    AmountGranted = request.FacilityAmount,
                    EquityContribution = request.BeneficiaryEquityContribution,
                    DateApproved = request.DateOfApproval,
                    EffectiveDate = request.EffectiveDate,
                    TenorType = request.TenorType,
                    Tenor = request.Tenor,
                    MaturityDate = MaturityDateMM(request.EffectiveDate, request.Tenor, request.TenorType),
                    Rate = request.InterestRate,
                    ArrearsRate = request.InterestRate,
                    BaseYear = (short)(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365),
                    RepaymentType = "02",
                    ConsentStatus = "001",
                    PurposeType = request.FacilityPurpose,//"002",
                    OwnershipId = "001",
                    SecurityCoverageCode = "01",
                    GuaranteeCoverageCode = "002",
                    LegalActionCode = "002",
                    CreditCardNo = "",
                    OnLendingStatus = "00",
                    OnLendingCreditId = "000",
                    Memo = "",
                    ObserveLimit = false,
                    EditMode = false,
                    TransStatus = "1",
                    BranchAdded = accMaster.BranchCode,
                    AddedBy = request.AddedBy,
                    WorkstationAdded = "EazyCredit",
                    WorkstationIpadded = "192.1.1.1",
                    BranchApproved = accMaster.BranchCode,
                    PostingDateApproved = DateTime.Now,
                    AddedApprovedBy = "System",
                    SegmentId = "ZZZ",
                    FundSourceCode = "00",
                    MisofficeCode = "ZZZ",
                    //ExtAccountNo = request.CreditIDRef,                    
                    AccountDesc = request.AccountName,
                    PreferredRepaymentBankCBNCode = request.PreferredRepaymentBankCBNCode,
                    PreferredRepaymentAccount = request.PreferredRepaymentAccount
                };

                var result = await CreditMaintHist(creditMaintHist);

                if (result.ResponseCode != "00")
                {
                    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = null
                    };

                    return response;
                }


                DateTime maturityDate = MaturityDateMM(request.EffectiveDate, request.Tenor, request.TenorType);

                

                if(request.ScheduleParameters != null)
                {
                    DateTime interestRepaymentStartDate = MaturityDateMM(request.EffectiveDate, (short)(request.ScheduleParameters.InterestMorat == 0 ? 1 : request.ScheduleParameters.InterestMorat), request.ScheduleParameters.InterestRepaymentFreq);
                    DateTime principalRepaymentStartDate = MaturityDateMM(request.EffectiveDate, (short)(request.ScheduleParameters.PrincipalMorat == 0 ? 1 : request.ScheduleParameters.PrincipalMorat), request.ScheduleParameters.PrincipalMoratFreq);

                    var creditSchedulesHeadersMaint = new CreateCreditSchedulesHeaderDto
                    {
                        TransId = transID,
                        CreditId = creditId,
                        ScheduleType = request.ScheduleType,
                        InterestRepayNextDate = interestRepaymentStartDate, //request.InterestRepaymentStartDate,
                        PrincipalRepayNextDate = principalRepaymentStartDate, //request.PrincipalRepaymentStartDate,
                        InterestRepayFreq = request.ScheduleParameters.InterestRepaymentFreq,
                        PrincipalRepayFreq = request.ScheduleParameters.PrincipalRepaymentFreq,
                        InterestRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.InterestMorat == 0 ? request.EffectiveDate : interestRepaymentStartDate, request.ScheduleParameters.InterestRepaymentFreq, maturityDate),
                        PrincipalRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.PrincipalMorat == 0 ? request.EffectiveDate : principalRepaymentStartDate, request.ScheduleParameters.PrincipalRepaymentFreq, maturityDate),
                        InterestArrearsTreatment = "01",
                        PrincipalArrearsTreatment = "01",
                        ChargesArrearsTreatment = "01",
                        ObserveLimit = false,
                        AddedBy = request.AddedBy,
                        PostingDateAdded = DateTime.Now,
                        BaseYear = (short)(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365),
                        StaggeredRepaymeent = false,
                        UseExistingAnnuity = false,
                        PrincipalMorat = request.ScheduleParameters.PrincipalMorat,
                        PrincipalMoratFreq = request.ScheduleParameters.PrincipalMoratFreq,
                        InterestMorat = request.ScheduleParameters.InterestMorat,
                        InterestMoratFreq = request.ScheduleParameters.InterestMoratFreq,
                        AmortizeMoratoriumInterest = false,
                        MoratoriumInterest = 0,
                        MoratoriumIntAppDate = DateTime.Now,
                        EffectiveDate = request.EffectiveDate
                    };


                    var sheduleHeaderResult = await CreditScheduleHeaderMaint(creditSchedulesHeadersMaint);

                    if (sheduleHeaderResult.ResponseCode != "00")
                    {
                        response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                        {
                            ResponseCode = "01",
                            ResponseMessage = "fail",
                            ResponseResult = null
                        };

                        return response;
                    }

                    var schedule = new GenerateRepaymentScheduleDto
                    {
                        TransID = transID,
                        CreditID = creditId,
                        ScheduleType = request.ScheduleType,
                        InterestRepayNextDate = interestRepaymentStartDate, //request.InterestRepaymentStartDate,
                        PrincipalRepayNextDate = principalRepaymentStartDate, //request.PrincipalRepaymentStartDate,
                        InterestRepayFreq = request.ScheduleParameters.InterestRepaymentFreq,
                        PrincipalRepayFreq = request.ScheduleParameters.PrincipalRepaymentFreq,
                        InterestRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.InterestMorat == 0 ? request.EffectiveDate : interestRepaymentStartDate, request.ScheduleParameters.InterestRepaymentFreq, maturityDate),
                        PrincipalRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.PrincipalMorat == 0 ? request.EffectiveDate : principalRepaymentStartDate, request.ScheduleParameters.PrincipalRepaymentFreq, maturityDate),
                        PrincipalAmount = request.FacilityAmount,
                        InterestRate = request.InterestRate,
                        EffectiveDate = request.EffectiveDate,
                        MaturityDate = maturityDate,
                        Base = 365,
                        Simulation = false,
                        UseExistingAnnuityAmount = false,
                        StaggeredRepayment = false
                    };

                    var scheduleResult = await eazyCoreFun.GenerateRepaymentSchedule(schedule);

                    scheduleResponse = await eazyCoreFun.CreditSchedulesLinesSelectSummary(creditId, "3");


                }

                //if (request.IsRentLoan)
                //    request.ScheduleType = "03";


                //await CreateConsentAsync(request.OperativeAccount, creditId);

                var workFlowLever = await db.WorkflowLevels.FirstOrDefaultAsync(x => x.WorkflowID == request.Workflow && x.LevelOrder == 1);

                var transheder = new CreateTransHeaderStageDto
                {
                    CreditID = creditId,
                    TransDesc = request.FacilityDescription,
                    TransStatus = "201",
                    Workflow = request.Workflow,
                    Workflowlevel = workFlowLever.LevelID,
                    TransComment = request.FacilityDescription,
                    ActionCode = "101",
                    UserId = request.AddedBy
                };

                await headerStageService.CreateTransHeaderStage(transheder);

                response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = scheduleResponse
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ViewAPIResponse<List<CreditScheduleSummaryDto>>> LoanBookingWithDepencies(CreateCreditMaintHistWithDependenciesDto request)
        {
            ViewAPIResponse<List<CreditScheduleSummaryDto>> response = null;
            VwAccMaster accMaster;
            List<CreditGuarantor> creditGuarantors = null;
            List<CreditCharge> creditCharges = null;
            List<CreditSecurity> creditSecurities = null;
            try
            {
                string json = JsonConvert.SerializeObject(request);

                //WriteLogFromPortal(json);

                accMaster = await eazyCore.GetCustomerAccountByAccountNo(request.OperativeAccount);


                if (accMaster == null)
                {
                    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "operative account number does not exist in CBA",
                        ResponseResult = null
                    };

                    return response; ;
                }

                //if (!await db.CBNBankCodes.AnyAsync(x => x.CBNCode == request.PreferredRepaymentBankCBNCode))
                //{
                //    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                //    {
                //        ResponseCode = "01",
                //        ResponseMessage = $"{"The provide Preferred Repayment BankCBNCode of "}{request.PreferredRepaymentBankCBNCode}{" is invalid"}",
                //        ResponseResult = null
                //    };

                //    return response;
                //}

                if (!await db.ProdMasters.AnyAsync(x => x.ProdCode == request.LoanProduct))
                {
                    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = $"{"The provide loan product of "}{request.LoanProduct}{" is invalid"}",
                        ResponseResult = null
                    };

                    return response;
                }

                //if (await db.CreditMaintHists.AnyAsync(x => x.ExtAccountNo == request.CreditIDRef && x.EditMode == false))
                //{
                //    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                //    {
                //        ResponseCode = "01",
                //        ResponseMessage = "fail",
                //        ResponseResult = null
                //    };

                //    return response;
                //}

                string temPreditId = await eazyCoreFun.GenerateNumberSequence(4002);
                string transID = await eazyCoreFun.GenerateNumberSequence(4003);

                string creditId = $"{temPreditId}{"01"}";

                string loanAccount = await coreFun.GenerateCreditAccountNumber(request.OperativeAccount, "", "", false);

                var creditMaintHist = new CreditMaintHist
                {
                    TransID = transID,
                    PostingDate = DateTime.Now,
                    CreditId = creditId,
                    Description = request.FacilityDescription,
                    CreditType = request.CreditType,
                    OperativeAccountNo = request.OperativeAccount,
                    Overdraft = false,
                    ProdCode = request.LoanProduct,
                    AccountNo = loanAccount,
                    DisbursementAccountNo = request.OperativeAccount,
                    AmountGranted = request.FacilityAmount,
                    EquityContribution = request.BeneficiaryEquityContribution,
                    DateApproved = request.DateOfApproval,
                    EffectiveDate = request.EffectiveDate,
                    TenorType = request.TenorType,
                    Tenor = request.Tenor,
                    MaturityDate = MaturityDateMM(request.EffectiveDate, request.Tenor, request.TenorType),
                    Rate = request.InterestRate,
                    ArrearsRate = request.InterestRate,
                    BaseYear = (short)(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365),
                    AutoRepayArrears = true,
                    FirstDisbursementDate = DateTime.Now,
                    FirstDisbursementAmount = 0,
                    LastDisbursementDate = DateTime.Now,
                    RepaymentType = "02",
                    FixedRepaymentAmount = 0,
                    FixedRepaymentStartDate = DateTime.Now,
                    FixedRepaymentFrequency = "",
                    FixedRepaymentNextDate = DateTime.Now,
                    FixedRepaymentLastDate = DateTime.Now,
                    FixedRepaymentArrearsTreatment = "",
                    ConsentStatus = "001",
                    PurposeType = request.FacilityPurpose,//"002",
                    OwnershipId = "001",
                    SecurityCoverageCode = "01",
                    GuaranteeCoverageCode = "002",
                    LegalActionCode = "002",
                    CreditCardNo = "",
                    OnLendingStatus = "00",
                    OnLendingCreditId = "000",
                    Memo = "",
                    ObserveLimit = false,
                    //SecurityCoverageCode = creditMaintHist.SecurityCoverageCode,
                    //GuaranteeCoverageCode = creditMaintHist.GuaranteeCoverageCode,
                    //LegalActionCode = creditMaintHist.LegalActionCode,
                    //CreditCardNo = creditMaintHist.CreditCardNo,
                    //OnLendingStatus = creditMaintHist.OnLendingStatus,
                    //OnLendingCreditId = creditMaintHist.OnLendingCreditId,
                    //Memo = creditMaintHist.Memo,
                    //ObserveLimit = false,
                    EditMode = true,
                    TransStatus = "1",
                    BranchAdded = accMaster.BranchCode,
                    AddedBy = "AddedBy",
                    DateAdded = DateTime.Now,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    WorkstationAdded = "",
                    WorkstationIpadded = "",
                    BranchApproved = "",
                    PostingDateApproved = DateTime.Now,
                    AddedApprovedBy = "",
                    DateAddedApproved = DateTime.Now,
                    TimeAddedApproved = DateTime.Now.TimeOfDay,
                    WorkstationApproved = "",
                    WorkstationIpapproved = "",
                    CapitalizeInterest = false,
                    EffectiveRate = request.InterestRate,
                    CarryingAmount = 0,
                    UpfrontInterest = false,
                    SegmentId = "ZZZ",
                    FundSourceCode = "00",
                    ExtEntry = false,
                    ExtAccountNo = "",
                    MisofficeCode = "ZZZ",
                    AccountDesc = accMaster.AccountDesc,
                    PreferredRepaymentBankCBNCode = request.PreferredRepaymentBankCBNCode,
                    PreferredRepaymentAccount = request.PreferredRepaymentAccount

                };


                if (request.CreditCharges.Count > 0 )
                {
                    foreach ( var record in request.CreditCharges )
                    {
                        creditCharges.Add(new CreditCharge
                        {
                            Sequence = 0,
                            TransID = transID,
                            CreditId = creditId,
                            ChargeBaseCode = record.ChargeBaseCode,
                            Rate = record.Rate,
                            RateType = record.RateType,
                            FreqCode = record.FreqCode,
                            NextExecDate = record.NextExecDate,
                            LastExecDate = record.NextExecDate,
                            Upfront = record.Upfront,
                            AddedBy = record.AddedBy,
                            DateAdded = DateTime.Now,
                            PostingDateAdded = DateTime.Now,
                            TimeAdded = DateTime.Now.TimeOfDay,
                            DateLastModified = DateTime.Now,
                            LastModifiedBy = record.AddedBy,
                            TimeLastModified = DateTime.Now.TimeOfDay
                        });
                    }
                    
                }

                if(request.CreditGuarantors.Count > 0 )
                {
                    foreach ( var record in request.CreditGuarantors)
                    {
                        creditGuarantors.Add(new CreditGuarantor
                        {
                            SeqNo = 0,
                            TransID = transID,
                            CreditId = creditId,
                            Gender = record.Gender,
                            GuarantorType = record.GuarantorType,
                            GuarantorFullNames = record.GuarantorFullNames,
                            Nationality = record.Nationality,
                            MainBusiness = record.MainBusiness,
                            BusinessRegNo = record.BusinessRegNo,
                            BusinessTaxNo = record.BusinessTaxNo,
                            CertIncorp = record.CertIncorp,
                            BirthDate = record.BirthDate,
                            MaritalStatus = record.MaritalStatus,
                            LegalConstitution = record.LegalConstitution,
                            AddedBy = record.AddedBy,
                            DateAdded = DateTime.Now,
                            DateLastModified = DateTime.Now,
                            PostingDateAdded = DateTime.Now,
                            TelNo = record.TelNo,
                            TimeAdded = DateTime.Now.TimeOfDay,
                            Address = record.Address,
                            EmailAddr = record.EmailAddr,
                            LastModifiedBy = record.AddedBy,
                            Liability = record.Liability,
                            BankName = record.BankName,
                            BvnNo = record.BvnNo,
                            ChequeNo = record.ChequeNo,
                            Pep = record.Pep
                        });
                    }

                }

                if(request.CreditSecurities.Count > 0)
                {
                    foreach(var record in request.CreditSecurities)
                    {
                        creditSecurities.Add(new CreditSecurity
                        {
                            SeqNo = 0,
                            TransID = transID,
                            CreditId = creditId,
                            SecurityType = record.SecurityType,
                            SecurityRefNo = record.SecurityRefNo,
                            SecurityValue = record.SecurityValue,
                            CurrCode = record.CurrCode,
                            ValuerType = record.ValuerType,
                            AddedBy = record.AddedBy,
                            DateAdded = DateTime.Now,
                            PostingDateAdded= DateTime.Now,
                            TimeAdded= DateTime.Now.TimeOfDay,
                            LastModifiedBy= record.AddedBy,
                            DateLastModified= DateTime.Now,
                            TimeLastModified= DateTime.Now.TimeOfDay,
                            Description = record.Description,
                            ForcedSaleValue = record.ForcedSaleValue,
                            MaturityDate = record.MaturityDate,
                            SecurityAddress = record.SecurityAddress,
                            SecurityLocationCode = record.SecurityLocationCode,
                            SecurityTitleCode = record.SecurityTitleCode
                        });
                    }
                }

                //var result = await CreditMaintHist(creditMaintHist);

                await db.CreditMaintHists.AddAsync(creditMaintHist);
                if(creditGuarantors != null)
                    await db.AddRangeAsync(creditGuarantors);
                if (creditCharges != null)
                    await db.AddRangeAsync(creditCharges);
                if (creditSecurities != null)
                    await db.AddRangeAsync(creditSecurities);

                var result = await db.SaveChangesAsync();



                if (result == 0)
                {
                    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = null
                    };

                    return response;
                }


                DateTime maturityDate = MaturityDateMM(request.EffectiveDate, request.Tenor, request.TenorType);
                DateTime interestRepaymentStartDate = MaturityDateMM(request.EffectiveDate, (short)(request.ScheduleParameters.InterestMorat == 0 ? 1 : request.ScheduleParameters.InterestMorat), request.ScheduleParameters.InterestRepaymentFreq);
                DateTime principalRepaymentStartDate = MaturityDateMM(request.EffectiveDate, (short)(request.ScheduleParameters.PrincipalMorat == 0 ? 1 : request.ScheduleParameters.PrincipalMorat), request.ScheduleParameters.PrincipalMoratFreq);

                //if (request.IsRentLoan)
                //    request.ScheduleType = "03";

                var creditSchedulesHeadersMaint = new CreateCreditSchedulesHeaderDto
                {
                    TransId = transID,
                    CreditId = creditId,
                    ScheduleType = request.ScheduleType,
                    InterestRepayNextDate = interestRepaymentStartDate, //request.InterestRepaymentStartDate,
                    PrincipalRepayNextDate = principalRepaymentStartDate, //request.PrincipalRepaymentStartDate,
                    InterestRepayFreq = request.ScheduleParameters.InterestRepaymentFreq,
                    PrincipalRepayFreq = request.ScheduleParameters.PrincipalRepaymentFreq,
                    InterestRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.InterestMorat == 0 ? request.EffectiveDate : interestRepaymentStartDate, request.ScheduleParameters.InterestRepaymentFreq, maturityDate),
                    PrincipalRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.PrincipalMorat == 0 ? request.EffectiveDate : principalRepaymentStartDate, request.ScheduleParameters.PrincipalRepaymentFreq, maturityDate),
                    InterestArrearsTreatment = "",
                    PrincipalArrearsTreatment = "",
                    ChargesArrearsTreatment = "",
                    ObserveLimit = false,
                    AddedBy = request.AddedBy,
                    PostingDateAdded = DateTime.Now,
                    BaseYear = (short)(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365),
                    StaggeredRepaymeent = false,
                    UseExistingAnnuity = false,
                    PrincipalMorat = request.ScheduleParameters.PrincipalMorat,
                    PrincipalMoratFreq = request.ScheduleParameters.PrincipalMoratFreq,
                    InterestMorat = request.ScheduleParameters.InterestMorat,
                    InterestMoratFreq = request.ScheduleParameters.InterestMoratFreq,
                    AmortizeMoratoriumInterest = false,
                    MoratoriumInterest = 0,
                    MoratoriumIntAppDate = DateTime.Now,
                    EffectiveDate = request.EffectiveDate
                };

                var sheduleHeaderResult = await CreditScheduleHeaderMaint(creditSchedulesHeadersMaint);

                if (sheduleHeaderResult.ResponseCode != "00")
                {
                    response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = null
                    };

                    return response;
                }

                var schedule = new GenerateRepaymentScheduleDto
                {
                    TransID = transID,
                    CreditID = creditId,
                    ScheduleType = request.ScheduleType,
                    InterestRepayNextDate = interestRepaymentStartDate, //request.InterestRepaymentStartDate,
                    PrincipalRepayNextDate = principalRepaymentStartDate, //request.PrincipalRepaymentStartDate,
                    InterestRepayFreq = request.ScheduleParameters.InterestRepaymentFreq,
                    PrincipalRepayFreq = request.ScheduleParameters.PrincipalRepaymentFreq,
                    InterestRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.InterestMorat == 0 ? request.EffectiveDate : interestRepaymentStartDate, request.ScheduleParameters.InterestRepaymentFreq, maturityDate),
                    PrincipalRepayNumber = ComputeNumberOfRepayments(request.ScheduleParameters.PrincipalMorat == 0 ? request.EffectiveDate : principalRepaymentStartDate, request.ScheduleParameters.PrincipalRepaymentFreq, maturityDate),
                    PrincipalAmount = request.FacilityAmount,
                    InterestRate = request.InterestRate,
                    EffectiveDate = request.EffectiveDate,
                    MaturityDate = maturityDate,
                    Base = (short)(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365),
                    Simulation = false,
                    UseExistingAnnuityAmount = false,
                    StaggeredRepayment = false
                };

                var scheduleResult = await eazyCoreFun.GenerateRepaymentSchedule(schedule);

                var scheduleResponse = await eazyCoreFun.CreditSchedulesLinesSelectSummary(creditId, "3");

                //await CreateConsentAsync(request.OperativeAccount, creditId);


                response = new ViewAPIResponse<List<CreditScheduleSummaryDto>>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = scheduleResponse
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ViewAPIResponse<string>> CreditMaintHist(CreateCreditMaintHistDto creditMaintHist)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                if (!await db.CreditMaintHists.AnyAsync(x => x.CreditId == creditMaintHist.CreditId))
                {
                    var record = new CreditMaintHist
                    {
                        TransID = creditMaintHist.TransId,
                        PostingDate = creditMaintHist.PostingDate,
                        CreditId = creditMaintHist.CreditId,
                        Description = creditMaintHist.Description,
                        CreditType = creditMaintHist.CreditType,
                        OperativeAccountNo = creditMaintHist.OperativeAccountNo,
                        Overdraft = creditMaintHist.Overdraft,
                        ProdCode = creditMaintHist.ProdCode,
                        AccountNo = creditMaintHist.AccountNo,
                        DisbursementAccountNo = creditMaintHist.DisbursementAccountNo,
                        AmountGranted = creditMaintHist.AmountGranted,
                        EquityContribution = creditMaintHist.EquityContribution,
                        DateApproved = creditMaintHist.DateApproved,
                        EffectiveDate = creditMaintHist.EffectiveDate,
                        TenorType = creditMaintHist.TenorType,
                        Tenor = creditMaintHist.Tenor,
                        MaturityDate = creditMaintHist.MaturityDate,
                        Rate = creditMaintHist.Rate,
                        ArrearsRate = creditMaintHist.ArrearsRate,
                        BaseYear = creditMaintHist.BaseYear,
                        AutoRepayArrears = true,
                        FirstDisbursementDate = DateTime.Now,
                        FirstDisbursementAmount = 0,
                        LastDisbursementDate = DateTime.Now,
                        RepaymentType = creditMaintHist.RepaymentType,
                        FixedRepaymentAmount = 0,
                        FixedRepaymentStartDate = DateTime.Now,
                        FixedRepaymentFrequency = "",
                        FixedRepaymentNextDate = DateTime.Now,
                        FixedRepaymentLastDate = DateTime.Now,
                        FixedRepaymentArrearsTreatment = "",
                        ConsentStatus = creditMaintHist.ConsentStatus,
                        PurposeType = creditMaintHist.PurposeType,
                        OwnershipId = creditMaintHist.OwnershipId,
                        SecurityCoverageCode = creditMaintHist.SecurityCoverageCode,
                        GuaranteeCoverageCode = creditMaintHist.GuaranteeCoverageCode,
                        LegalActionCode = creditMaintHist.LegalActionCode,
                        CreditCardNo = creditMaintHist.CreditCardNo,
                        OnLendingStatus = creditMaintHist.OnLendingStatus,
                        OnLendingCreditId = creditMaintHist.OnLendingCreditId,
                        Memo = creditMaintHist.Memo,
                        ObserveLimit = false,
                        EditMode = creditMaintHist.EditMode,
                        TransStatus = creditMaintHist.TransStatus,
                        BranchAdded = creditMaintHist.BranchAdded,
                        AddedBy = creditMaintHist.AddedBy,
                        DateAdded = DateTime.Now,
                        TimeAdded = DateTime.Now.TimeOfDay,
                        WorkstationAdded = "",
                        WorkstationIpadded = "",
                        BranchApproved = "",
                        PostingDateApproved = DateTime.Now,
                        AddedApprovedBy = "",
                        DateAddedApproved = DateTime.Now,
                        TimeAddedApproved = DateTime.Now.TimeOfDay,
                        WorkstationApproved = "",
                        WorkstationIpapproved = "",
                        CapitalizeInterest = false,
                        EffectiveRate = creditMaintHist.Rate,
                        CarryingAmount = 0,
                        UpfrontInterest = false,
                        SegmentId = creditMaintHist.SegmentId,
                        FundSourceCode = creditMaintHist.FundSourceCode,
                        ExtEntry = false,
                        ExtAccountNo = "",
                        MisofficeCode = creditMaintHist.MisofficeCode,
                        AccountDesc = creditMaintHist.AccountDesc,
                        PreferredRepaymentBankCBNCode = creditMaintHist.PreferredRepaymentBankCBNCode,
                        PreferredRepaymentAccount = creditMaintHist.PreferredRepaymentAccount

                    };

                    await db.CreditMaintHists.AddAsync(record);
                    await db.SaveChangesAsync();

                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success",
                        ResponseResult = "record created"
                    };
                }
                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record could not be created"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> CreditSecurityMaint(CreateCreditSecuritiesDto creditSecuritiesMaint)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var isRecordExist = await db.CreditMaintHists.AnyAsync(x => x.CreditId == creditSecuritiesMaint.CreditId);
                if (!isRecordExist)
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record not exist in partent table"
                    };
                }

                var record = new CreditSecurity
                {
                    SeqNo = 0,
                    TransID = creditSecuritiesMaint.TransId,
                    CreditId = creditSecuritiesMaint.CreditId,
                    SecurityType = creditSecuritiesMaint.SecurityType,
                    SecurityRefNo = creditSecuritiesMaint.SecurityRefNo,
                    SecurityValue = creditSecuritiesMaint.SecurityValue,
                    CurrCode = creditSecuritiesMaint.CurrCode,
                    ValuerType = creditSecuritiesMaint.ValuerType,
                    AddedBy = creditSecuritiesMaint.AddedBy,
                    DateAdded = DateTime.Now,
                    PostingDateAdded = creditSecuritiesMaint.PostingDateAdded,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    Description = creditSecuritiesMaint.Description,
                    ForcedSaleValue = creditSecuritiesMaint.ForcedSaleValue,
                    MaturityDate = creditSecuritiesMaint.MaturityDate.Value,
                    SecurityAddress = creditSecuritiesMaint.SecurityAddress,
                    SecurityLocationCode = creditSecuritiesMaint.SecurityLocationCode,
                    SecurityTitleCode = creditSecuritiesMaint.SecurityTitleCode
                };

                await db.CreditSecurities.AddAsync(record);
                var result = await db.SaveChangesAsync();
                if(result > 0)
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success",
                        ResponseResult = "record created"
                    };
                }

                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record could not be created"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> CreditSecurityMaint(List<CreateCreditSecuritiesDto> creditSecuritiesMaint)
        {
            ViewAPIResponse<string> response = null;
            List<CreditSecurity> creditSecurityList = new List<CreditSecurity>();
            try
            {
                if(creditSecuritiesMaint != null)
                {
                    foreach(var record in creditSecuritiesMaint)
                    {
                        var isRecordExist = await db.CreditMaintHists.AnyAsync(x => x.CreditId == record.CreditId);
                        if (isRecordExist)
                        {
                            creditSecurityList.Add(new CreditSecurity
                            {
                                SeqNo = 0,
                                TransID = record.TransId,
                                CreditId = record.CreditId,
                                SecurityType = record.SecurityType,
                                SecurityRefNo = record.SecurityRefNo,
                                SecurityValue = record.SecurityValue,
                                CurrCode = record.CurrCode,
                                ValuerType = record.ValuerType,
                                AddedBy = record.AddedBy,
                                DateAdded = DateTime.Now,
                                PostingDateAdded = record.PostingDateAdded,
                                TimeAdded = DateTime.Now.TimeOfDay,
                                Description = record.Description,
                                ForcedSaleValue = record.ForcedSaleValue,
                                MaturityDate = record.MaturityDate.Value,
                                SecurityAddress = record.SecurityAddress,
                                SecurityLocationCode = record.SecurityLocationCode,
                                SecurityTitleCode = record.SecurityTitleCode
                            });
                        }
                    }

                    if (creditSecurityList.Count > 0)
                    {
                        await db.AddRangeAsync(creditSecurityList);
                        var result = await db.SaveChangesAsync();
                        if (result > 0)
                        {
                            response = new ViewAPIResponse<string>
                            {
                                ResponseCode = "00",
                                ResponseMessage = "success",
                                ResponseResult = "record created"
                            };
                        }

                        else
                        {
                            response = new ViewAPIResponse<string>
                            {
                                ResponseCode = "01",
                                ResponseMessage = "fail",
                                ResponseResult = "record could not be created"
                            };
                        }

                    }
                    else
                    {
                        response = new ViewAPIResponse<string>
                        {
                            ResponseCode = "01",
                            ResponseMessage = "fail",
                            ResponseResult = "record not exist in partent table"
                        };
                    }

                }
                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "invalid input"
                    };
                }
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ViewAPIResponse<string>> CreditGuarantorMaint(CreateCreditGuarantorsDto creditGuarantorsMaint)
        {
            ViewAPIResponse<string> response = null;

            try
            {

                var isRecordExist = await db.CreditMaintHists.AnyAsync(x => x.CreditId == creditGuarantorsMaint.CreditId);
                if(!isRecordExist)
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record not exist in partent table"
                    };
                }
                var record = new CreditGuarantor
                {
                    SeqNo = 0,
                    TransID = creditGuarantorsMaint.TransId,
                    CreditId = creditGuarantorsMaint.CreditId,
                    GuarantorType = creditGuarantorsMaint.GuarantorType,
                    GuarantorFullNames = creditGuarantorsMaint.GuarantorFullNames,
                    Nationality = creditGuarantorsMaint.Nationality,
                    MainBusiness = creditGuarantorsMaint.MainBusiness,
                    BusinessRegNo = creditGuarantorsMaint.BusinessRegNo,
                    CertIncorp = creditGuarantorsMaint.CertIncorp,
                    BusinessTaxNo = creditGuarantorsMaint.BusinessTaxNo,
                    BirthDate = creditGuarantorsMaint.BirthDate,
                    Gender = creditGuarantorsMaint.Gender,
                    MaritalStatus = creditGuarantorsMaint.MaritalStatus,
                    LegalConstitution = creditGuarantorsMaint.LegalConstitution,
                    AddedBy = creditGuarantorsMaint.AddedBy,
                    DateAdded = DateTime.Now,
                    PostingDateAdded = creditGuarantorsMaint.PostingDateAdded,
                    TimeAdded = DateTime.Now.TimeOfDay,
                    Address = creditGuarantorsMaint.Address,
                    TelNo = creditGuarantorsMaint.TelNo,
                    EmailAddr = creditGuarantorsMaint.EmailAddr,
                    Liability = creditGuarantorsMaint.Liability,
                    BvnNo = creditGuarantorsMaint.BvnNo,
                    ChequeNo = creditGuarantorsMaint.ChequeNo,
                    BankName = creditGuarantorsMaint.BankName,
                    Pep = creditGuarantorsMaint.Pep

                };

                await db.CreditGuarantors.AddAsync(record);
                var result = await db.SaveChangesAsync();

                if(result > 0)
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success",
                        ResponseResult = "record created"
                    };
                }

                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record could not be created"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> CreditGuarantorMaint(List<CreateCreditGuarantorsDto> creditGuarantorsMaint)
        {
            ViewAPIResponse<string> response = null;
            List<CreditGuarantor> creditGuarantorList = new List<CreditGuarantor>();

            try
            {
                if(creditGuarantorsMaint != null )
                {
                    foreach(var record in creditGuarantorsMaint)
                    {
                        var isRecordExist = await db.CreditMaintHists.AnyAsync(x => x.CreditId == record.CreditId);
                        if( isRecordExist )
                        {
                            creditGuarantorList.Add(new CreditGuarantor
                            {
                                SeqNo = 0,
                                TransID = record.TransId,
                                CreditId = record.CreditId,
                                GuarantorType = record.GuarantorType,
                                GuarantorFullNames = record.GuarantorFullNames,
                                Nationality = record.Nationality,
                                MainBusiness = record.MainBusiness,
                                BusinessRegNo = record.BusinessRegNo,
                                CertIncorp = record.CertIncorp,
                                BusinessTaxNo = record.BusinessTaxNo,
                                BirthDate = record.BirthDate,
                                Gender = record.Gender,
                                MaritalStatus = record.MaritalStatus,
                                LegalConstitution = record.LegalConstitution,
                                AddedBy = record.AddedBy,
                                DateAdded = DateTime.Now,
                                PostingDateAdded = record.PostingDateAdded,
                                TimeAdded = DateTime.Now.TimeOfDay,
                                Address = record.Address,
                                TelNo = record.TelNo,
                                EmailAddr = record.EmailAddr,
                                Liability = record.Liability,
                                BvnNo = record.BvnNo,
                                ChequeNo = record.ChequeNo,
                                BankName = record.BankName,
                                Pep = record.Pep

                            });
                        }

                    }

                    if (creditGuarantorList.Count > 0)
                    {
                        await db.AddRangeAsync(creditGuarantorList);
                        var result = await db.SaveChangesAsync();

                        if (result > 0)
                        {
                            response = new ViewAPIResponse<string>
                            {
                                ResponseCode = "00",
                                ResponseMessage = "success",
                                ResponseResult = "record created"
                            };
                        }

                        else
                        {
                            response = new ViewAPIResponse<string>
                            {
                                ResponseCode = "01",
                                ResponseMessage = "fail",
                                ResponseResult = "record could not be created"
                            };
                        }
                    }
                    else
                    {
                        response = new ViewAPIResponse<string>
                        {
                            ResponseCode = "01",
                            ResponseMessage = "fail",
                            ResponseResult = "record not exist in partent table"
                        };
                    }

                }
                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "invalid input"
                    };
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> CreditUpfrontChargesMaint(CreateCreditChargeDto creditChargesMaint)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var isRecordExist = await db.CreditMaintHists.AnyAsync(x => x.CreditId == creditChargesMaint.CreditId);
                if (!isRecordExist)
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record not exist in partent table"
                    };
                }

                var record = new CreditCharge
                {
                    Sequence = 0,
                    TransID = creditChargesMaint.TransId,
                    CreditId = creditChargesMaint.CreditId,
                    ChargeBaseCode = creditChargesMaint.ChargeBaseCode,
                    Rate = creditChargesMaint.Rate,
                    RateType = creditChargesMaint.RateType,
                    FreqCode = creditChargesMaint.FreqCode,
                    NextExecDate = creditChargesMaint.NextExecDate,
                    Upfront = creditChargesMaint.Upfront,
                    AddedBy = creditChargesMaint.AddedBy,
                    DateAdded = DateTime.Now,
                    PostingDateAdded = creditChargesMaint.PostingDateAdded,
                    TimeAdded = DateTime.Now.TimeOfDay
                };


                    await db.CreditCharges.AddAsync(record);
                   var result =  await db.SaveChangesAsync();
                if(result > 0)
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success",
                        ResponseResult = "record created"
                    };
                }

                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record could not be created"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> CreditUpfrontChargesMaint(List<CreateCreditChargeDto> creditChargesMaint)
        {
            ViewAPIResponse<string> response = null;
            List<CreditCharge> creditChargesList = new List<CreditCharge>();

            try
            {
                if(creditChargesMaint != null)
                {
                    foreach(var record in  creditChargesMaint)
                    {
                        var isRecordExist = await db.CreditMaintHists.AnyAsync(x => x.CreditId == record.CreditId);
                        if(isRecordExist)
                        {
                            creditChargesList.Add(new CreditCharge
                            {
                                Sequence = 0,
                                TransID = record.TransId,
                                CreditId = record.CreditId,
                                ChargeBaseCode = record.ChargeBaseCode,
                                Rate = record.Rate,
                                RateType = record.RateType,
                                FreqCode = record.FreqCode,
                                NextExecDate = record.NextExecDate,
                                Upfront = record.Upfront,
                                AddedBy = record.AddedBy,
                                DateAdded = DateTime.Now,
                                PostingDateAdded = record.PostingDateAdded,
                                TimeAdded = DateTime.Now.TimeOfDay
                            });

                        }
                    }

                    if (creditChargesList.Count > 0)
                    {
                        await db.AddRangeAsync(creditChargesList);
                        var result = await db.SaveChangesAsync();
                        if (result > 0)
                        {
                            response = new ViewAPIResponse<string>
                            {
                                ResponseCode = "00",
                                ResponseMessage = "success",
                                ResponseResult = "record created"
                            };
                        }

                        else
                        {
                            response = new ViewAPIResponse<string>
                            {
                                ResponseCode = "01",
                                ResponseMessage = "fail",
                                ResponseResult = "record could not be created"
                            };
                        }
                    }
                    else
                    {
                        response = new ViewAPIResponse<string>
                        {
                            ResponseCode = "01",
                            ResponseMessage = "fail",
                            ResponseResult = "record not exist in partent table"
                        };
                    }

                }
                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "invalid input"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> CreditScheduleHeaderMaint(CreateCreditSchedulesHeaderDto creditSchedulesHeadersMaint)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var isRecordExist = await db.CreditMaintHists.AnyAsync(x => x.CreditId == creditSchedulesHeadersMaint.CreditId);
                if (!isRecordExist)
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record not exist in partent table"
                    };
                }

                if (!await db.CreditSchedulesHeaders.AnyAsync(x => x.CreditId == creditSchedulesHeadersMaint.CreditId))
                {
                    var record = new CreditSchedulesHeader
                    {
                        TransID = creditSchedulesHeadersMaint.TransId,
                        CreditId = creditSchedulesHeadersMaint.CreditId,
                        ScheduleType = creditSchedulesHeadersMaint.ScheduleType,
                        InterestRepayNextDate = creditSchedulesHeadersMaint.InterestRepayNextDate,
                        PrincipalRepayNextDate = creditSchedulesHeadersMaint.PrincipalRepayNextDate,
                        InterestRepayFreq = creditSchedulesHeadersMaint.InterestRepayFreq,
                        PrincipalRepayFreq = creditSchedulesHeadersMaint.PrincipalRepayFreq,
                        InterestRepayNumber = creditSchedulesHeadersMaint.InterestRepayNumber,
                        PrincipalRepayNumber = creditSchedulesHeadersMaint.PrincipalRepayNumber,
                        InterestArrearsTreatment = creditSchedulesHeadersMaint.InterestArrearsTreatment,
                        PrincipalArrearsTreatment = creditSchedulesHeadersMaint.PrincipalArrearsTreatment,
                        ChargesArrearsTreatment = creditSchedulesHeadersMaint.ChargesArrearsTreatment,
                        ObserveLimit = creditSchedulesHeadersMaint.ObserveLimit,
                        AddedBy = creditSchedulesHeadersMaint.AddedBy,
                        LastModifiedBy = creditSchedulesHeadersMaint.AddedBy,
                        DateAdded = DateTime.Now,
                        PostingDateAdded = creditSchedulesHeadersMaint.PostingDateAdded,
                        TimeAdded = DateTime.Now.TimeOfDay,
                        DateLastModified = DateTime.Now,
                        TimeLastModified = DateTime.Now.TimeOfDay,
                        MoratoriumIntApplied = false,
                        BaseYear = creditSchedulesHeadersMaint.BaseYear,
                        //StaggeredRepaymeent = creditSchedulesHeadersMaint.StaggeredRepaymeent,
                        //UseExistingAnnuity = creditSchedulesHeadersMaint.UseExistingAnnuity,
                        PrincipalMorat = creditSchedulesHeadersMaint.PrincipalMorat,
                        PrincipalMoratFreq = creditSchedulesHeadersMaint.PrincipalMoratFreq,
                        InterestMorat = creditSchedulesHeadersMaint.InterestMorat,
                        InterestMoratFreq = creditSchedulesHeadersMaint.InterestMoratFreq,
                        AmortizeMoratoriumInterest = creditSchedulesHeadersMaint.AmortizeMoratoriumInterest,
                        MoratoriumInterest = creditSchedulesHeadersMaint.MoratoriumInterest,
                        MoratoriumIntAppDate = creditSchedulesHeadersMaint.MoratoriumIntAppDate,
                        EffectiveDate = creditSchedulesHeadersMaint.EffectiveDate
                    };

                    await db.CreditSchedulesHeaders.AddAsync(record);
                    await db.SaveChangesAsync();

                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success",
                        ResponseResult = "record created"
                    };
                }
                else
                {
                    response = new ViewAPIResponse<string>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "fail",
                        ResponseResult = "record could not be created"
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ComputeNumberOfRepayments(DateTime repayNextDate, string repayFreq, DateTime maturityDate)
        {
            int result = 0;
            if (repayFreq == "M" || repayFreq == "Monthly")
            {
                result = ((maturityDate.Year - repayNextDate.Year) * 12) + maturityDate.Month - repayNextDate.Month;
            }

            if (repayFreq == "2M" || repayFreq == "2-Month")
            {
                result = ((maturityDate.Year - repayNextDate.Year) * 12) + maturityDate.Month - repayNextDate.Month;
                result = result / 6;
            }

            if (repayFreq == "4M" || repayFreq == "4-Month")
            {
                result = ((maturityDate.Year - repayNextDate.Year) * 12) + maturityDate.Month - repayNextDate.Month;
                result = result / 4;
            }

            if (repayFreq == "D" || repayFreq == "Daily")
                result = (maturityDate - repayNextDate).Days;

            if (repayFreq == "Y" || repayFreq == "Yearly")
                result = (maturityDate.Year - repayNextDate.Year);

            if (repayFreq == "Q" || repayFreq == "Quarterly")
            {
                result = ((maturityDate.Year - repayNextDate.Year) * 12) + maturityDate.Month - repayNextDate.Month;
                result = result / 3;
            }

            if (repayFreq == "S" || repayFreq == "Half Yearly")
            {
                result = result = (maturityDate.Year - repayNextDate.Year);
                result = result * 2;
            }
            if (repayFreq == "W" || repayFreq == "Weekly")
            {
                result = (maturityDate - repayNextDate).Days;
                result = result / 7;
            }


            return result;
        }

        public DateTime MaturityDateMM(DateTime effectiveDate, short tenor, string tenorType)
        {
            DateTime matDate;

            if (tenorType == "D")
            {
                matDate = effectiveDate.AddDays(tenor);
            }
            else if (tenorType == "W")
            {
                matDate = effectiveDate.AddDays(tenor * 7);
            }
            else if (tenorType == "F")
            {
                matDate = effectiveDate.AddDays(tenor * 14);
            }
            else if (tenorType == "M")
            {
                matDate = effectiveDate.AddMonths(tenor);
            }
            else if (tenorType == "Q")
            {
                matDate = effectiveDate.AddMonths(tenor * 3);
            }
            else if (tenorType == "H")
            {
                matDate = effectiveDate.AddMonths(tenor * 6);
            }
            else if (tenorType == "Y")
            {
                matDate = effectiveDate.AddYears(tenor);
            }
            else
            {
                matDate = effectiveDate;
            }

            while (matDate.DayOfWeek.ToString() == "Saturday" || matDate.DayOfWeek.ToString() == "Sunday")
            {
                matDate = matDate.AddDays(1);
            }

            return matDate;
        }

        public async Task<ViewAPIResponse<ViewLookupResponse>> Lookup()
        {
            ViewAPIResponse<ViewLookupResponse> response = null;

            try
            {
                var lookupresponse = await db.ProdMasters.Where(x => x.ProdCatCode == "6" && x.ArrearsProdCode == "ZZZ") //bank loan products
                                    .Select(x => new ViewProdMasterLookup { ProdCode = x.ProdCode, ProdName = x.ProdName }).ToListAsync();

                List<ViewProdMasterLookup> loanProducts = new List<ViewProdMasterLookup>();

                foreach (var record in lookupresponse)
                {
                    loanProducts.Add(new ViewProdMasterLookup
                    {
                        ProdCode = record.ProdCode,
                        ProdName = record.ProdName
                    });
                }

                List<ViewCBNBankCodes> preferredRepaymentBankCBNCode = new List<ViewCBNBankCodes>();

                var lookupcbnresponse = await db.CBNBankCodes //cbn bank codes
                                    .Select(x => new ViewCBNBankCodes { BankCode = x.CBNCode, BankName = x.InstitutionName }).ToListAsync();

                foreach (var record in lookupcbnresponse)
                {
                    preferredRepaymentBankCBNCode.Add(new ViewCBNBankCodes
                    {
                        BankCode = record.BankCode,
                        BankName = record.BankName
                    });
                }

                List<ViewLookupResult> creditType = new List<ViewLookupResult>();

                var lookupcreditTyperesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1003) //credit facility types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupcreditTyperesponse)
                {
                    creditType.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> facilityPurpose = new List<ViewLookupResult>();

                var lookupcreditFacilityPurposeresponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1004) //credit facility types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupcreditFacilityPurposeresponse)
                {
                    facilityPurpose.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> tenorType = new List<ViewLookupResult>();

                var lookuptenorTyperesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 48) //tenor types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookuptenorTyperesponse)
                {
                    tenorType.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }


                List<ViewLookupResult> scheduleType = new List<ViewLookupResult>();

                var lookupscheduleTyperesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1000) //schedule types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupscheduleTyperesponse)
                {
                    scheduleType.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> principalRepaymentFreq = new List<ViewLookupResult>();

                var lookupprincipalRepaymentFreqresponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 26) //principal Repayment Freq
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupprincipalRepaymentFreqresponse)
                {
                    principalRepaymentFreq.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> interestRepaymentFreq = new List<ViewLookupResult>();

                foreach (var record in lookupprincipalRepaymentFreqresponse)
                {
                    interestRepaymentFreq.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> principalMoratFreq = new List<ViewLookupResult>();



                foreach (var record in lookupprincipalRepaymentFreqresponse)
                {
                    principalMoratFreq.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> interestMoratFreq = new List<ViewLookupResult>();



                foreach (var record in lookupprincipalRepaymentFreqresponse)
                {
                    interestMoratFreq.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                ViewLookupResponse lookupResponse = new ViewLookupResponse
                {
                    loanProduct = loanProducts,
                    preferredRepaymentBankCBNCode = preferredRepaymentBankCBNCode,
                    creditType = creditType,
                    facilityPurpose = facilityPurpose,
                    tenorType = tenorType,
                    scheduleType = scheduleType,
                    principalRepaymentFreq = principalRepaymentFreq,
                    interestRepaymentFreq = interestRepaymentFreq,
                    principalMoratFreq = principalMoratFreq,
                    interestMoratFreq = interestMoratFreq
                };

                response = new ViewAPIResponse<ViewLookupResponse>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = lookupResponse
                };
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<ViewGuarantorLookupResponse>> GuarantorLookup()
        {
            ViewAPIResponse<ViewGuarantorLookupResponse> response = null;

            try
            {

                List<ViewLookupResult> guarantorType = new List<ViewLookupResult>();

                var lookupguarantorTyperesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1031) //credit facility types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupguarantorTyperesponse)
                {
                    guarantorType.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> gender = new List<ViewLookupResult>();

                var lookupgenderresponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 10) //credit facility types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupgenderresponse)
                {
                    gender.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> maritalStatus = new List<ViewLookupResult>();

                var lookupmaritalStatusresponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 9) //tenor types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupmaritalStatusresponse)
                {
                    maritalStatus.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }


                List<ViewLookupResult> legalConstitution = new List<ViewLookupResult>();

                var lookupslegalConstitutionresponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1032) //schedule types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupslegalConstitutionresponse)
                {
                    legalConstitution.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewNationalityLookup> nationality = new List<ViewNationalityLookup>();

                var lookupNationalityresponse = await db.PrmCountries.ToListAsync();

                foreach (var record in lookupNationalityresponse)
                {
                    nationality.Add(new ViewNationalityLookup
                    {
                        CountryCode = record.CountryCode,
                        CountryName = record.CountryName
                    });
                }


                ViewGuarantorLookupResponse lookupResponse = new ViewGuarantorLookupResponse
                {
                    guarantorType = guarantorType,
                    nationality = nationality,
                    gender = gender,
                    maritalStatus = maritalStatus,
                    legalConstitution = legalConstitution
                };

                response = new ViewAPIResponse<ViewGuarantorLookupResponse>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = lookupResponse
                };
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<ViewSecurityLookupResponse>> SecurityLookup()
        {
            ViewAPIResponse<ViewSecurityLookupResponse> response = null;

            try
            {

                List<ViewLookupResult> securityType = new List<ViewLookupResult>();

                var lookupsecurityTyperesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1027) //credit facility types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupsecurityTyperesponse)
                {
                    securityType.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> valuerType = new List<ViewLookupResult>();

                var lookupvaluerTyperesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1029) //credit facility types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupvaluerTyperesponse)
                {
                    valuerType.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> securityLocationCode = new List<ViewLookupResult>();

                var lookupsecurityLocationCoderesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1060) //tenor types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupsecurityLocationCoderesponse)
                {
                    securityLocationCode.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }


                List<ViewLookupResult> securityTitleCode = new List<ViewLookupResult>();

                var lookupssecurityTitleCoderesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 1061) //schedule types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupssecurityTitleCoderesponse)
                {
                    securityTitleCode.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewPrmCurrencyLookup> currCode = new List<ViewPrmCurrencyLookup>();

                var lookupcurrCoderesponse = await db.PrmCurrencies.ToListAsync();

                foreach (var record in lookupcurrCoderesponse)
                {
                    currCode.Add(new ViewPrmCurrencyLookup
                    {
                        CurrCode = record.CurrCode,
                        CurrDesc = record.CurrDesc
                    });
                }


                ViewSecurityLookupResponse lookupResponse = new ViewSecurityLookupResponse
                {
                    securityType = securityType,
                    currCode = currCode,
                    valuerType = valuerType,
                    securityLocationCode = securityLocationCode,
                    securityTitleCode = securityTitleCode
                };

                response = new ViewAPIResponse<ViewSecurityLookupResponse>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = lookupResponse
                };
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<ViewChargesLookupResponse>> UpfrontFeeLookup()
        {
            ViewAPIResponse<ViewChargesLookupResponse> response = null;

            try
            {

                List<ViewLookupResult> chargeBaseCode = new List<ViewLookupResult>();

                var lookupchargeBaseCoderesponse = await eazyCore.GetChargeBaseCodeLookup();

                foreach (var record in lookupchargeBaseCoderesponse)
                {
                    chargeBaseCode.Add(new ViewLookupResult
                    {
                        Code = record.ChargeBaseCode,
                        Description = record.ChargeBaseDesc
                    });
                }

                List<ViewLookupResult> rateType = new List<ViewLookupResult>();

                var lookuprateTyperesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 24) //credit facility types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookuprateTyperesponse)
                {
                    rateType.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }

                List<ViewLookupResult> freqCode = new List<ViewLookupResult>();

                var lookupfreqCoderesponse = await db.PrmTypesDetails.Where(x => x.TypeCode == 26) //tenor types
                                    .Select(x => new ViewLookupResult { Code = x.Code, Description = x.Description }).ToListAsync();

                foreach (var record in lookupfreqCoderesponse)
                {
                    freqCode.Add(new ViewLookupResult
                    {
                        Code = record.Code,
                        Description = record.Description
                    });
                }




                ViewChargesLookupResponse lookupResponse = new ViewChargesLookupResponse
                {
                    chargeBaseCode = chargeBaseCode,
                    rateType = rateType,
                    freqCode = freqCode
                };

                response = new ViewAPIResponse<ViewChargesLookupResponse>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = lookupResponse
                };
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<List<CreditScheduleSummaryDto>>> GetCreditSchedulesLines(string loanId)
        {
            ViewAPIResponse<List<CreditScheduleSummaryDto>> viewAPIResponse = null;

            var scheduleResponse = await eazyCoreFun.CreditSchedulesLinesSelectSummary(loanId, "3");

            return new ViewAPIResponse<List<CreditScheduleSummaryDto>>
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = scheduleResponse
            };
        }

        public async Task<ViewAPIResponse<List<AccountSearchDto>>> AccountEnquiry(string searchToken, SearchFlag searchFlag)
        {
            ViewAPIResponse<List<AccountSearchDto>> viewAPIResponse = null;
            List<AccountSearchDto> accountSearchDto = new List<AccountSearchDto>();

            if (searchFlag == SearchFlag.AccountNo) {
                var response = await eazyCore.GetCustomerAccountByAccountNo(searchToken);

                if(response == null)
                {
                    return new ViewAPIResponse<List<AccountSearchDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "notfound"
                    };
                }

                accountSearchDto.Add(new AccountSearchDto
                {
                    AccountNo = response.AccountNo,
                    AccountName = response.AccountDesc,
                    BranchName = response.BranchName,
                    AccountType = response.AccountType,
                    CusNo = response.CusID
                });

            }

            if (searchFlag == SearchFlag.Bvn)
            {
                var response = await eazyCore.GetCusMasterByBvn(searchToken);

                if (response == null)
                {
                    return new ViewAPIResponse<List<AccountSearchDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "notfound"
                    };
                }


                var accounts = await eazyCore.GetCustomerAccountByCusID(response.CusID);

                if (accounts == null)
                {
                    return new ViewAPIResponse<List<AccountSearchDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "no account found for the bvn"
                    };
                }

                foreach (var record in accounts)
                {
                    accountSearchDto.Add(new AccountSearchDto
                    {
                        AccountNo = record.AccountNo,
                        AccountName = record.AccountDesc,
                        BranchName = record.BranchName,
                        AccountType = record.AccountType,
                        CusNo = record.CusID
                    });
                }
            }

            if (searchFlag == SearchFlag.Name)
            {
                var response = await eazyCore.GetCustomerAccountByName(searchToken);

                if (response == null)
                {
                    return new ViewAPIResponse<List<AccountSearchDto>>
                    {
                        ResponseCode = "01",
                        ResponseMessage = "notfound"
                    };
                }

                foreach (var record in response)
                {
                    accountSearchDto.Add(new AccountSearchDto
                    {
                        AccountNo = record.AccountNo,
                        AccountName = record.AccountDesc,
                        BranchName = record.BranchName,
                        AccountType = record.AccountType,
                        CusNo = record.CusID
                    });
                }
            }

            return new ViewAPIResponse<List<AccountSearchDto>>
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = accountSearchDto
            };
        }

        public async Task<ViewAPIResponse<string>> EditCreditSecurity(CreateCreditSecuritiesDto request)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditSecurities.FirstOrDefaultAsync(x => x.CreditId.Equals(request.CreditId) && x.TransID.Equals(request.TransId) && x.SeqNo == request.SeqNo);

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                existingRecord.SecurityType = request.SecurityType;
                existingRecord.SecurityRefNo = request.SecurityRefNo;
                existingRecord.SecurityValue = request.SecurityValue;
                existingRecord.ValuerType = request.ValuerType;
                existingRecord.LastModifiedBy = request.AddedBy;
                existingRecord.DateLastModified = DateTime.Now;
                existingRecord.TimeLastModified = DateTime.Now.TimeOfDay;
                existingRecord.Description = request.Description;
                existingRecord.ForcedSaleValue = request.ForcedSaleValue;
                existingRecord.MaturityDate = request.MaturityDate;
                existingRecord.SecurityAddress = request.SecurityAddress;
                existingRecord.SecurityLocationCode = request.SecurityLocationCode;
                existingRecord.SecurityTitleCode = request.SecurityTitleCode;

                db.Update<CreditSecurity>(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> EditCreditGuarantor(CreateCreditGuarantorsDto request)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditGuarantors.FirstOrDefaultAsync(x => x.CreditId.Equals(request.CreditId) && x.TransID.Equals(request.TransId) && x.SeqNo == request.SeqNo);

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                existingRecord.GuarantorType = request.GuarantorType;
                existingRecord.GuarantorFullNames = request.GuarantorFullNames;
                existingRecord.Nationality = request.Nationality;
                existingRecord.MainBusiness = request.MainBusiness;
                existingRecord.BusinessRegNo = request.BusinessRegNo;
                existingRecord.CertIncorp = request.CertIncorp;
                existingRecord.BusinessTaxNo = request.BusinessTaxNo;
                existingRecord.BirthDate = request.BirthDate;
                existingRecord.Gender = request.Gender;
                existingRecord.MaritalStatus = request.MaritalStatus;
                existingRecord.LegalConstitution = request.LegalConstitution;
                existingRecord.LastModifiedBy = request.AddedBy;
                existingRecord.DateLastModified = DateTime.Now;
                existingRecord.Address = request.Address;
                existingRecord.TelNo = request.TelNo;
                existingRecord.EmailAddr = request.EmailAddr;
                existingRecord.Liability = request.Liability;
                existingRecord.BvnNo = request.BvnNo;
                existingRecord.ChequeNo = request.ChequeNo;
                existingRecord.BankName = request.BankName;
                existingRecord.Pep = request.Pep;

                db.Update<CreditGuarantor>(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> EditCreditCharge(CreateCreditChargeDto request)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditCharges.FirstOrDefaultAsync(x => x.CreditId.Equals(request.CreditId) && x.TransID.Equals(request.TransId) && x.Sequence == request.Sequence);

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                existingRecord.ChargeBaseCode = request.ChargeBaseCode;
                existingRecord.Rate = request.Rate;
                existingRecord.RateType = request.RateType;
                existingRecord.FreqCode = request.FreqCode;
                existingRecord.NextExecDate = request.NextExecDate;
                existingRecord.LastExecDate = DateTime.Now;
                existingRecord.Upfront = request.Upfront;
                existingRecord.LastModifiedBy = request.AddedBy;
                existingRecord.DateLastModified = DateTime.Now;
                existingRecord.TimeLastModified = DateTime.Now.TimeOfDay;

                db.Update<CreditCharge>(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> EditCreditSecurity(List<CreateCreditSecuritiesDto> request)
        {
            ViewAPIResponse<string> response = null;
            List<CreditSecurity> creditSecurity = new List<CreditSecurity>();
            try
            {
                if(request.Count > 0)
                {
                    foreach(var record in request)
                    {
                        var existingRecord = await db.CreditSecurities.FirstOrDefaultAsync(x => x.CreditId.Equals(record.CreditId) && x.TransID.Equals(record.TransId) && x.SeqNo == record.SeqNo);

                        if (existingRecord != null)
                        {
                            existingRecord.SecurityType = record.SecurityType;
                            existingRecord.SecurityRefNo = record.SecurityRefNo;
                            existingRecord.SecurityValue = record.SecurityValue;
                            existingRecord.ValuerType = record.ValuerType;
                            existingRecord.LastModifiedBy = record.AddedBy;
                            existingRecord.DateLastModified = DateTime.Now;
                            existingRecord.TimeLastModified = DateTime.Now.TimeOfDay;
                            existingRecord.Description = record.Description;
                            existingRecord.ForcedSaleValue = record.ForcedSaleValue;
                            existingRecord.MaturityDate = record.MaturityDate;
                            existingRecord.SecurityAddress = record.SecurityAddress;
                            existingRecord.SecurityLocationCode = record.SecurityLocationCode;
                            existingRecord.SecurityTitleCode = record.SecurityTitleCode;

                            creditSecurity.Add(existingRecord);
                        }

                    }

                    db.UpdateRange(creditSecurity);
                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                    {

                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "00",
                            ResponseMessage = "success"
                        };
                    }
                    else
                    {
                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "01",
                            ResponseMessage = "errorUpdating"
                        };
                    }
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "invalidinput"
                    };
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> EditCreditGuarantor(List<CreateCreditGuarantorsDto> request)
        {
            ViewAPIResponse<string> response = null;
            List<CreditGuarantor> creditGuarantors = new List<CreditGuarantor>();
            try
            {

                if (request != null)
                {
                    foreach (var record in request)
                    {
                        var existingRecord = await db.CreditGuarantors.FirstOrDefaultAsync(x => x.CreditId.Equals(record.CreditId) && x.TransID.Equals(record.TransId) && x.SeqNo == record.SeqNo);

                        if(existingRecord != null)
                        {
                            existingRecord.GuarantorType = record.GuarantorType;
                            existingRecord.GuarantorFullNames = record.GuarantorFullNames;
                            existingRecord.Nationality = record.Nationality;
                            existingRecord.MainBusiness = record.MainBusiness;
                            existingRecord.BusinessRegNo = record.BusinessRegNo;
                            existingRecord.CertIncorp = record.CertIncorp;
                            existingRecord.BusinessTaxNo = record.BusinessTaxNo;
                            existingRecord.BirthDate = record.BirthDate;
                            existingRecord.Gender = record.Gender;
                            existingRecord.MaritalStatus = record.MaritalStatus;
                            existingRecord.LegalConstitution = record.LegalConstitution;
                            existingRecord.LastModifiedBy = record.AddedBy;
                            existingRecord.DateLastModified = DateTime.Now;
                            existingRecord.Address = record.Address;
                            existingRecord.TelNo = record.TelNo;
                            existingRecord.EmailAddr = record.EmailAddr;
                            existingRecord.Liability = record.Liability;
                            existingRecord.BvnNo = record.BvnNo;
                            existingRecord.ChequeNo = record.ChequeNo;
                            existingRecord.BankName = record.BankName;
                            existingRecord.Pep = record.Pep;

                            creditGuarantors.Add(existingRecord);
                        }

                    }

                    db.UpdateRange(creditGuarantors);
                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                    {

                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "00",
                            ResponseMessage = "success"
                        };
                    }
                    else
                    {
                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "01",
                            ResponseMessage = "errorUpdating"
                        };
                    }

                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "invalideinput"
                    };
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> EditCreditCharge(List<CreateCreditChargeDto> request)
        {
            ViewAPIResponse<string> response = null;
            List<CreditCharge> creditCharges = new List<CreditCharge>();

            try
            {

                if (request != null)
                {
                    foreach (var record in request)
                    {
                        var existingRecord = await db.CreditCharges.FirstOrDefaultAsync(x => x.CreditId.Equals(record.CreditId) && x.TransID.Equals(record.TransId) && x.Sequence == record.Sequence);

                        if(existingRecord != null)
                        {
                            existingRecord.ChargeBaseCode = record.ChargeBaseCode;
                            existingRecord.Rate = record.Rate;
                            existingRecord.RateType = record.RateType;
                            existingRecord.FreqCode = record.FreqCode;
                            existingRecord.NextExecDate = record.NextExecDate;
                            existingRecord.LastExecDate = DateTime.Now;
                            existingRecord.Upfront = record.Upfront;
                            existingRecord.LastModifiedBy = record.AddedBy;
                            existingRecord.DateLastModified = DateTime.Now;
                            existingRecord.TimeLastModified = DateTime.Now.TimeOfDay;

                            creditCharges.Add(existingRecord);
                        }
                    }

                    db.UpdateRange(creditCharges);
                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                    {

                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "00",
                            ResponseMessage = "success"
                        };
                    }
                    else
                    {
                        return response = new ViewAPIResponse<string>()
                        {
                            ResponseCode = "01",
                            ResponseMessage = "errorUpdating"
                        };
                    }

                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "invalidinput"
                    };
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditSecurity(long seqNo)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditSecurities.FirstOrDefaultAsync(x => x.SeqNo == seqNo);

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.Remove<CreditSecurity>(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditSecurity(List<long> request)
        {
            ViewAPIResponse<string> response = null;
            List<CreditSecurity> securityList = new List<CreditSecurity>();


            try
            {
                if(request.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "invalidinput"
                    };
                }

                foreach(var seqNo in  request)
                {
                    var existingRecord = await db.CreditSecurities.FirstOrDefaultAsync(x => x.SeqNo == seqNo);

                    if (existingRecord != null)
                    {
                        securityList.Add(existingRecord);
                    }

                }

                if (securityList.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.RemoveRange(securityList);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditSecurityByCreditId(string creditId)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditSecurities.Where(x => x.CreditId == creditId).ToListAsync();

                if (existingRecord.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.RemoveRange(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditGuarantor(long seqNo)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditGuarantors.FirstOrDefaultAsync(x => x.SeqNo == seqNo);

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.Remove<CreditGuarantor>(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditGuarantor(List<long> request)
        {
            ViewAPIResponse<string> response = null;
            List<CreditGuarantor> guarantorList = new List<CreditGuarantor>();


            try
            {
                if (request.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "invalidinput"
                    };
                }

                foreach (var seqNo in request)
                {
                    var existingRecord = await db.CreditGuarantors.FirstOrDefaultAsync(x => x.SeqNo == seqNo);

                    if (existingRecord != null)
                    {
                        guarantorList.Add(existingRecord);
                    }

                }

                if (guarantorList.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.RemoveRange(guarantorList);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditGuarantorByCreditId(string creditId)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditGuarantors.Where(x => x.CreditId == creditId).ToListAsync();

                if (existingRecord.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.RemoveRange(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditCharge(long sequence)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditCharges.FirstOrDefaultAsync(x => x.Sequence == sequence);

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.Remove<CreditCharge>(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditCharge(List<long> request)
        {
            ViewAPIResponse<string> response = null;
            List<CreditCharge> chargesList = new List<CreditCharge>();


            try
            {
                if (request.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "invalidinput"
                    };
                }

                foreach (var seqNo in request)
                {
                    var existingRecord = await db.CreditCharges.FirstOrDefaultAsync(x => x.Sequence == seqNo);

                    if (existingRecord != null)
                    {
                        chargesList.Add(existingRecord);
                    }

                }

                if (chargesList.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.RemoveRange(chargesList);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditChargeByCreditId(string creditId)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditCharges.Where(x => x.CreditId == creditId).ToListAsync();

                if (existingRecord.Count == 0)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.RemoveRange(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<List<CreditMaintResultDto>>> GetAllLoansWithDepenciesByUserAsync(string userId)
        {
            ViewAPIResponse<List<CreditMaintResultDto>> viewAPIResponse = null;
            List<CreditMaintResultDto> creditList = new List<CreditMaintResultDto>();

            var response = await db.CreditMaintHists.Where(x => x.AddedBy ==  userId).ToListAsync();

            if (response.Count == 0)
            {
                return new ViewAPIResponse<List<CreditMaintResultDto>>
                {
                    ResponseCode = "01",
                    ResponseMessage = "notfound"
                };
            }

            foreach(var record in response)
            {
                var guarantors = await FindCreditGuarantorsByCreditId(record.CreditId);
                var securities = await FindCreditSecuritiesByCreditId(record.CreditId);
                var charges = await FindCreditChargesByCreditId(record.CreditId);
                var scheduleParam = await FindCreditScheduleHeaderByCreditId(record.CreditId);
                var scheduleResponse = await eazyCoreFun.CreditSchedulesLinesSelectSummary(record.CreditId, "3");

                creditList.Add(new CreditMaintResultDto
                {
                    TransId = record.TransID,
                    CreditId = record.CreditId,
                    OperativeAccount = record.OperativeAccountNo,
                    AccountName = record.AccountDesc,                    
                    FacilityDescription = record.Description,
                    CreditType = record.CreditType,
                    CreditTypeDesc = LookupDescription(record.CreditType, 1003),
                    LoanProduct = record.ProdCode,
                    ProductName = LookupProdName(record.ProdCode),
                    PurposeType = record.PurposeType,
                    PurposeTypeDesc = LookupDescription(record.PurposeType, 1004),
                    FacilityAmount = record.AmountGranted,
                    BeneficiaryEquityContribution = record.EquityContribution,
                    DateOfApproval = record.DateAddedApproved.Value,
                    EffectiveDate = record.EffectiveDate,
                    Tenor = record.Tenor,
                    TenorType = record.TenorType,
                    TenorTypeDesc = LookupDescription(record.TenorType, 48),
                    InterestRate = record.Rate,
                    RepaymentType = record.RepaymentType,
                    RepaymentTypeDesc = LookupDescription(record.RepaymentType, 1053),
                    ScheduleParameters = scheduleParam,
                    ScheduleLines = scheduleResponse,
                    CreditGuarantors = guarantors,
                    CreditCharges = charges,
                    CreditSecurities = securities

                });
            }


            return new ViewAPIResponse<List<CreditMaintResultDto>>
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = creditList
            };

        }

        public async Task<ViewAPIResponse<List<CreditMaintResultDto>>> GetAllLoansByUserAsync(string userId)
        {
            ViewAPIResponse<List<CreditMaintResultDto>> viewAPIResponse = null;
            List<CreditMaintResultDto> creditList = new List<CreditMaintResultDto>();

            var response = await db.CreditMaintHists.Where(x => x.AddedBy == userId).ToListAsync();

            if (response.Count == 0)
            {
                return new ViewAPIResponse<List<CreditMaintResultDto>>
                {
                    ResponseCode = "01",
                    ResponseMessage = "notfound"
                };
            }

            foreach (var record in response)
            {
                //var guarantors = await GetCreditGuarantorsByCreditId(record.CreditId);
                //var securities = await GetCreditSecuritiesByCreditId(record.CreditId);
                //var charges = await GetCreditChargesByCreditId(record.CreditId);
                //var scheduleParam = await GetCreditScheduleHeaderByCreditId(record.CreditId);
                //var scheduleResponse = await eazyCoreFun.CreditSchedulesLinesSelectSummary(record.CreditId, "3");

                creditList.Add(new CreditMaintResultDto
                {
                    TransId = record.TransID,
                    CreditId = record.CreditId,
                    OperativeAccount = record.OperativeAccountNo,
                    AccountName = record.AccountDesc,
                    FacilityDescription = record.Description,
                    CreditType = record.CreditType,
                    CreditTypeDesc = LookupDescription(record.CreditType, 1003),
                    LoanProduct = record.ProdCode,
                    ProductName = LookupProdName(record.ProdCode),
                    PurposeType = record.PurposeType,
                    PurposeTypeDesc = LookupDescription(record.PurposeType, 1004),
                    FacilityAmount = record.AmountGranted,
                    BeneficiaryEquityContribution = record.EquityContribution,
                    DateOfApproval = record.DateAddedApproved.Value,
                    EffectiveDate = record.EffectiveDate,
                    Tenor = record.Tenor,
                    TenorType = record.TenorType,
                    TenorTypeDesc = LookupDescription(record.TenorType, 48),
                    InterestRate = record.Rate,
                    RepaymentType = record.RepaymentType,
                    RepaymentTypeDesc = LookupDescription(record.RepaymentType, 1053)
                    //ScheduleParameters = scheduleParam,
                    //ScheduleLines = scheduleResponse,
                    //CreditGuarantors = guarantors,
                    //CreditCharges = charges,
                    //CreditSecurities = securities

                });
            }


            return new ViewAPIResponse<List<CreditMaintResultDto>>
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = creditList
            };

        }

        public async Task<ViewAPIResponse<CreditMaintResultDto>> GetLoansByCreditIdAsync(string creditId)
        {
            ViewAPIResponse<List<CreditMaintResultDto>> viewAPIResponse = null;
            //List<CreditMaintResultDto> creditList = new List<CreditMaintResultDto>();

            var response = await db.CreditMaintHists.FirstOrDefaultAsync(x => x.CreditId == creditId);

            if (response == null)
            {
                return new ViewAPIResponse<CreditMaintResultDto>
                {
                    ResponseCode = "01",
                    ResponseMessage = "notfound"
                };
            }

            //var guarantors = await GetCreditGuarantorsByCreditId(record.CreditId);
            //var securities = await GetCreditSecuritiesByCreditId(record.CreditId);
            //var charges = await GetCreditChargesByCreditId(record.CreditId);
            //var scheduleParam = await GetCreditScheduleHeaderByCreditId(record.CreditId);
            //var scheduleResponse = await eazyCoreFun.CreditSchedulesLinesSelectSummary(record.CreditId, "3");

            var creditList = new CreditMaintResultDto
            {
                TransId = response.TransID,
                CreditId = response.CreditId,
                OperativeAccount = response.OperativeAccountNo,
                AccountName = response.AccountDesc,
                FacilityDescription = response.Description,
                CreditType = response.CreditType,
                CreditTypeDesc = LookupDescription(response.CreditType, 1003),
                LoanProduct = response.ProdCode,
                ProductName = LookupProdName(response.ProdCode),
                PurposeType = response.PurposeType,
                PurposeTypeDesc = LookupDescription(response.PurposeType, 1004),
                FacilityAmount = response.AmountGranted,
                BeneficiaryEquityContribution = response.EquityContribution,
                DateOfApproval = response.DateAddedApproved.Value,
                EffectiveDate = response.EffectiveDate,
                Tenor = response.Tenor,
                TenorType = response.TenorType,
                TenorTypeDesc = LookupDescription(response.TenorType, 48),
                InterestRate = response.Rate,
                RepaymentType = response.RepaymentType,
                RepaymentTypeDesc = LookupDescription(response.RepaymentType, 1053)
                //ScheduleParameters = scheduleParam,
                //ScheduleLines = scheduleResponse,
                //CreditGuarantors = guarantors,
                //CreditCharges = charges,
                //CreditSecurities = securities

            };


            return new ViewAPIResponse<CreditMaintResultDto>
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = creditList
            };

        }

        public async Task<ViewAPIResponse<CreditMaintResultDto>> GetLoanWithDepenciesByCreditIdAsync(string creditId)
        {
            ViewAPIResponse<List<CreditMaintResultDto>> viewAPIResponse = null;
            //List<CreditMaintResultDto> creditList = new List<CreditMaintResultDto>();

            var response = await db.CreditMaintHists.FirstOrDefaultAsync(x => x.CreditId == creditId);

            if (response == null)
            {
                return new ViewAPIResponse<CreditMaintResultDto>
                {
                    ResponseCode = "01",
                    ResponseMessage = "notfound"
                };
            }

            var guarantors = await FindCreditGuarantorsByCreditId(response.CreditId);
            var securities = await FindCreditSecuritiesByCreditId(response.CreditId);
            var charges = await FindCreditChargesByCreditId(response.CreditId);
            var scheduleParam = await FindCreditScheduleHeaderByCreditId(response.CreditId);
            var scheduleResponse = await eazyCoreFun.CreditSchedulesLinesSelectSummary(response.CreditId, "3");

            var creditList = new CreditMaintResultDto
            {
                TransId = response.TransID,
                CreditId = response.CreditId,
                OperativeAccount = response.OperativeAccountNo,
                AccountName = response.AccountDesc,
                FacilityDescription = response.Description,
                CreditType = response.CreditType,
                CreditTypeDesc = LookupDescription(response.CreditType, 1003),
                LoanProduct = response.ProdCode,
                ProductName = LookupProdName(response.ProdCode),
                PurposeType = response.PurposeType,
                PurposeTypeDesc = LookupDescription(response.PurposeType, 1004),
                FacilityAmount = response.AmountGranted,
                BeneficiaryEquityContribution = response.EquityContribution,
                DateOfApproval = response.DateAddedApproved.Value,
                EffectiveDate = response.EffectiveDate,
                Tenor = response.Tenor,
                TenorType = response.TenorType,
                TenorTypeDesc = LookupDescription(response.TenorType, 48),
                InterestRate = response.Rate,
                RepaymentType = response.RepaymentType,
                RepaymentTypeDesc = LookupDescription(response.RepaymentType, 1053),
                ScheduleParameters = scheduleParam,
                ScheduleLines = scheduleResponse,
                CreditGuarantors = guarantors,
                CreditCharges = charges,
                CreditSecurities = securities

             };



            return new ViewAPIResponse<CreditMaintResultDto>
            {
                ResponseCode = "00",
                ResponseMessage = "success",
                ResponseResult = creditList
            };

        }

        public string LookupDescription(string code, short typeCode)
        {

            try
            {
                var result =  db.PrmTypesDetails.FirstOrDefault(x => x.TypeCode == typeCode && x.Code == code); //credit facility types                                    

                return result.Description;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LookupProdName(string code)
        {

            try
            {
                var result =  db.ProdMasters.FirstOrDefault(x => x.ProdCode == code); //credit facility types                                    

                return result.ProdName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<List<CreditGuarantorsResultDto>> FindCreditGuarantorsByCreditId(string creditId)
        {
            List<CreditGuarantorsResultDto> guarantorsList = new List<CreditGuarantorsResultDto>();

            var response = await db.CreditGuarantors.Where(x => x.CreditId == creditId).ToListAsync();

            if(response.Count > 0)
            {
                foreach (var record in response)
                {
                    guarantorsList.Add(new CreditGuarantorsResultDto
                    {
                        SeqNo = record.SeqNo,
                        CreditId = record.CreditId,
                        TransId = record.TransID,
                        GuarantorType = record.GuarantorType,
                        GuarantorTypeDesc = "",
                        GuarantorFullNames = record.GuarantorFullNames,
                        Nationality = record.Nationality,
                        MainBusiness = record.MainBusiness,
                        BusinessRegNo = record.BusinessRegNo,
                        CertIncorp = record.CertIncorp,
                        BusinessTaxNo   = record.BusinessTaxNo,
                        BirthDate = record.BirthDate,
                        Gender = record.Gender, 
                        GenderDesc = "",
                        MaritalStatus = record.MaritalStatus,
                        MaritalStatusDesc = "",
                        LegalConstitution = record.LegalConstitution,
                        Address = record.Address,
                        EmailAddr = record.EmailAddr,
                        TelNo = record.TelNo,
                        BvnNo = record.BvnNo,
                        ChequeNo = record.ChequeNo,
                        BankName = record.BankName,
                        Liability = record.Liability,
                        Pep = record.Pep
                    });
                }

                return guarantorsList;
            }
            else { 
                return guarantorsList; 
            }
        }

        protected async Task<List<CreditChargeResultDto>> FindCreditChargesByCreditId(string creditId)
        {
            List<CreditChargeResultDto> chargesList = new List<CreditChargeResultDto>();

            var response = await db.CreditCharges.Where(x => x.CreditId == creditId).ToListAsync();

            if (response.Count > 0)
            {
                foreach (var record in response)
                {
                    chargesList.Add(new CreditChargeResultDto
                    {
                        Sequence = record.Sequence,
                        CreditId = record.CreditId,
                        TransId = record.TransID,
                        ChargeBaseCode = record.ChargeBaseCode,
                        ChargeBaseDesc = "",
                        Rate = record.Rate,
                        RateType = record.RateType,
                        RateDesc = "",
                        FreqCode = record.FreqCode,
                        NextExecDate = record.NextExecDate,
                        Upfront = record.Upfront
                    });
                }

                return chargesList;
            }
            else
            {
                return chargesList;
            }
        }

        protected async Task<List<CreditSecuritiesResultDto>> FindCreditSecuritiesByCreditId(string creditId)
        {
            List<CreditSecuritiesResultDto> securityList = new List<CreditSecuritiesResultDto>();

            var response = await db.CreditSecurities.Where(x => x.CreditId == creditId).ToListAsync();

            if (response.Count > 0)
            {
                foreach (var record in response)
                {
                    securityList.Add(new CreditSecuritiesResultDto
                    {
                        SeqNo = record.SeqNo,
                        CreditId = record.CreditId,
                        TransId = record.TransID,
                        SecurityType = record.SecurityType,
                        SecurityDesc = "",
                        SecurityRefNo = record.SecurityRefNo,
                        SecurityValue = record.SecurityValue,
                        CurrCode = record.CurrCode,
                        ValuerType = record.ValuerType,
                        ValuerDesc = "",
                        Description = record.Description,
                        ForcedSaleValue = record.ForcedSaleValue,
                        MaturityDate = record.MaturityDate,
                        SecurityAddress = record.SecurityAddress,
                        SecurityLocationCode = record.SecurityLocationCode,
                        SecurityLocation = "",
                        SecurityTitleCode = record.SecurityTitleCode,
                        SecurityTitle = ""
                    });
                }

                return securityList;
            }
            else
            {
                return securityList;
            }
        }

        protected async Task<CreditScheduleParametersResultDto> FindCreditScheduleHeaderByCreditId(string creditId)
        {
            //List<CreditScheduleParametersResultDto> securityList = new List<CreditScheduleParametersResultDto>();

            var response = await db.CreditSchedulesHeaders.FirstOrDefaultAsync(x => x.CreditId == creditId);

            if (response != null)
            {
                var sheduleHeader = new CreditScheduleParametersResultDto
                {
                    CreditId = response.CreditId,
                    TransId = response.TransID,
                    PrincipalRepaymentStartDate = response.PrincipalRepayNextDate,
                    PrincipalRepaymentFreq = response.PrincipalRepayFreq,
                    InterestRepaymentStartDate = response.InterestRepayNextDate,                    
                    InterestRepaymentFreq = response.InterestRepayFreq,
                    PrincipalMorat = response.PrincipalMorat,
                    PrincipalMoratFreq = response.PrincipalMoratFreq,
                    InterestMorat = response.InterestMorat,
                    InterestMoratFreq = response.InterestMoratFreq
                };

                return sheduleHeader;
            }
            else
            {
                return null;
            }
        }


        public async Task<ViewAPIResponse<List<CreditGuarantorsResultDto>>> GetCreditGuarantorsByCreditId(string creditId)
        {
            ViewAPIResponse<List<CreditGuarantorsResultDto>> viewAPIResponse = null;
            List<CreditGuarantorsResultDto> guarantorsList = new List<CreditGuarantorsResultDto>();

            var response = await db.CreditGuarantors.Where(x => x.CreditId == creditId).ToListAsync();

            if (response.Count > 0)
            {
                foreach (var record in response)
                {
                    guarantorsList.Add(new CreditGuarantorsResultDto
                    {
                        SeqNo = record.SeqNo,
                        CreditId = record.CreditId,
                        TransId = record.TransID,
                        GuarantorType = record.GuarantorType,
                        GuarantorTypeDesc = "",
                        GuarantorFullNames = record.GuarantorFullNames,
                        Nationality = record.Nationality,
                        MainBusiness = record.MainBusiness,
                        BusinessRegNo = record.BusinessRegNo,
                        CertIncorp = record.CertIncorp,
                        BusinessTaxNo = record.BusinessTaxNo,
                        BirthDate = record.BirthDate,
                        Gender = record.Gender,
                        GenderDesc = "",
                        MaritalStatus = record.MaritalStatus,
                        MaritalStatusDesc = "",
                        LegalConstitution = record.LegalConstitution,
                        Address = record.Address,
                        EmailAddr = record.EmailAddr,
                        TelNo = record.TelNo,
                        BvnNo = record.BvnNo,
                        ChequeNo = record.ChequeNo,
                        BankName = record.BankName,
                        Liability = record.Liability,
                        Pep = record.Pep
                    });
                }

                return new ViewAPIResponse<List<CreditGuarantorsResultDto>>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = guarantorsList
                };
            }
            else
            {
                return new ViewAPIResponse<List<CreditGuarantorsResultDto>>
                {
                    ResponseCode = "01",
                    ResponseMessage = "norecord",
                    ResponseResult = guarantorsList
                };
            }
        }

        public async Task<ViewAPIResponse<List<CreditChargeResultDto>>> GetCreditChargesByCreditId(string creditId)
        {
            ViewAPIResponse<List<CreditChargeResultDto>> viewAPIResponse = null;
            List<CreditChargeResultDto> chargesList = new List<CreditChargeResultDto>();

            var response = await db.CreditCharges.Where(x => x.CreditId == creditId).ToListAsync();

            if (response.Count > 0)
            {
                foreach (var record in response)
                {
                    chargesList.Add(new CreditChargeResultDto
                    {
                        Sequence = record.Sequence,
                        CreditId = record.CreditId,
                        TransId = record.TransID,
                        ChargeBaseCode = record.ChargeBaseCode,
                        ChargeBaseDesc = "",
                        Rate = record.Rate,
                        RateType = record.RateType,
                        RateDesc = "",
                        FreqCode = record.FreqCode,
                        NextExecDate = record.NextExecDate,
                        Upfront = record.Upfront
                    });
                }

                return new ViewAPIResponse<List<CreditChargeResultDto>> { 
                    ResponseResult = chargesList,
                    ResponseMessage = "success",
                    ResponseCode = "00"
                };
            }
            else
            {
                return new ViewAPIResponse<List<CreditChargeResultDto>>
                {
                    ResponseResult = chargesList,
                    ResponseMessage = "norecord",
                    ResponseCode = "01"
                };
            }
        }

        public async Task<ViewAPIResponse<List<CreditSecuritiesResultDto>>> GetCreditSecuritiesByCreditId(string creditId)
        {
            ViewAPIResponse<List<CreditSecuritiesResultDto>> viewAPIResponse = null;
            List<CreditSecuritiesResultDto> securityList = new List<CreditSecuritiesResultDto>();

            var response = await db.CreditSecurities.Where(x => x.CreditId == creditId).ToListAsync();

            if (response.Count > 0)
            {
                foreach (var record in response)
                {
                    securityList.Add(new CreditSecuritiesResultDto
                    {
                        SeqNo = record.SeqNo,
                        CreditId = record.CreditId,
                        TransId = record.TransID,
                        SecurityType = record.SecurityType,
                        SecurityDesc = "",
                        SecurityRefNo = record.SecurityRefNo,
                        SecurityValue = record.SecurityValue,
                        CurrCode = record.CurrCode,
                        ValuerType = record.ValuerType,
                        ValuerDesc = "",
                        Description = record.Description,
                        ForcedSaleValue = record.ForcedSaleValue,
                        MaturityDate = record.MaturityDate,
                        SecurityAddress = record.SecurityAddress,
                        SecurityLocationCode = record.SecurityLocationCode,
                        SecurityLocation = "",
                        SecurityTitleCode = record.SecurityTitleCode,
                        SecurityTitle = ""
                    });
                }

                return new ViewAPIResponse<List<CreditSecuritiesResultDto>>
                {
                    ResponseCode = "00",
                    ResponseMessage = "success",
                    ResponseResult = securityList
                };
            }
            else
            {
                return new ViewAPIResponse<List<CreditSecuritiesResultDto>>
                {
                    ResponseCode = "01",
                    ResponseMessage = "norecord",
                    ResponseResult = securityList
                };
            }
        }

        public async Task<ViewAPIResponse<CreditScheduleParametersResultDto>> GetCreditScheduleHeaderByCreditId(string creditId)
        {
            ViewAPIResponse<CreditScheduleParametersResultDto> viewAPIResponse = null;

            //List<CreditScheduleParametersResultDto> securityList = new List<CreditScheduleParametersResultDto>();

            var response = await db.CreditSchedulesHeaders.FirstOrDefaultAsync(x => x.CreditId == creditId);

            if (response != null)
            {
                var sheduleHeader = new CreditScheduleParametersResultDto
                {
                    CreditId = response.CreditId,
                    TransId = response.TransID,
                    PrincipalRepaymentStartDate = response.PrincipalRepayNextDate,
                    PrincipalRepaymentFreq = response.PrincipalRepayFreq,
                    InterestRepaymentStartDate = response.InterestRepayNextDate,
                    InterestRepaymentFreq = response.InterestRepayFreq,
                    PrincipalMorat = response.PrincipalMorat,
                    PrincipalMoratFreq = response.PrincipalMoratFreq,
                    InterestMorat = response.InterestMorat,
                    InterestMoratFreq = response.InterestMoratFreq
                };

                return new ViewAPIResponse<CreditScheduleParametersResultDto> { 
                    ResponseResult = sheduleHeader,
                    ResponseMessage = "success",
                    ResponseCode = "00"
                };
            }
            else
            {
                return new ViewAPIResponse<CreditScheduleParametersResultDto>
                {
                    ResponseMessage = "norecord",
                    ResponseCode = "01"
                };
            }
        }

        public async Task<ViewAPIResponse<string>> EditCreditMaintHist(CreditUpdateRequestDto request)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditMaintHists.FirstOrDefaultAsync(x => x.CreditId.Equals(request.CreditId) && x.TransID.Equals(request.TransId));

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                existingRecord.Description = request.FacilityDescription;
                existingRecord.CreditType = request.CreditType;
                existingRecord.AmountGranted = request.FacilityAmount;
                existingRecord.EquityContribution = request.BeneficiaryEquityContribution;
                existingRecord.LastModifiedBy = request.AddedBy;
                existingRecord.DateLastModified = DateTime.Now;
                existingRecord.EffectiveDate = request.EffectiveDate;
                existingRecord.Tenor = request.Tenor;
                existingRecord.TenorType = request.TenorType;
                existingRecord.DateApproved = request.DateOfApproval;

                db.Update<CreditMaintHist>(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ViewAPIResponse<string>> RemoveCreditMaintHistByCreditId(string creditId)
        {
            ViewAPIResponse<string> response = null;

            try
            {
                var existingRecord = await db.CreditMaintHists.FirstOrDefaultAsync(x => x.CreditId == creditId);

                if (existingRecord == null)
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Norecord"
                    };
                }

                db.RemoveRange(existingRecord);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {

                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "00",
                        ResponseMessage = "success"
                    };
                }
                else
                {
                    return response = new ViewAPIResponse<string>()
                    {
                        ResponseCode = "01",
                        ResponseMessage = "errorUpdating"
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public enum SearchFlag
    {
        AccountNo = 0,
        Name = 1,
        Bvn = 2
    }
}
