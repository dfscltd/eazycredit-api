﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace EazyCoreObjs.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Table("CusMaster", Schema = "dbo")]
    public class CusMaster
    {
        [Key]
        public string CusId { get; set; }
        public string NetcusId { get; set; }
        public string CusName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BranchCode { get; set; }
        public string CusType { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string LocalGovtCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string EmployStatus { get; set; }
        public string OccCode { get; set; }
        public string MotherMaidName { get; set; }

        public string IncomeBracketCode { get; set; }
        public string SectorCode { get; set; }
        public string BusinessType { get; set; }
        public string CertificateNo { get; set; }
        public DateTime? CertificateDate { get; set; }
        public string CertificateIssuer { get; set; }
        public decimal? AuthCapital { get; set; }
        public decimal? PaidUpCapital { get; set; }
        public decimal? Reserves { get; set; }
        public decimal? UnappProfitLoss { get; set; }
        public DateTime? FinYearEnd { get; set; }
        public DateTime? LastAuditDate { get; set; }
        public byte BoardSize { get; set; }
        public string BranchAdded { get; set; }
        public string CusStatus { get; set; }
        public bool Insider { get; set; }
        public string InsiderType { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? PostingDateAdded { get; set; }

        public string LastApprovedBy { get; set; }
        public string BranchLastApproved { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? PostingDateLastModified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastApproved { get; set; }
        public DateTime? PostingDateLastApproved { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public TimeSpan? TimeLastApproved { get; set; }
        public string TinNo { get; set; }
        public string BvnNo { get; set; }
        public string OwnershipCode { get; set; }
        public string CreditRiskCode { get; set; }
        public string ComplianceRiskCode { get; set; }
        public bool? Pep { get; set; }
        public string RatingAgencyID { get; set; }
        public string Rating { get; set; }
        public string RatingOrigination { get; set; }
        public string FinInstTypeCode { get; set; }
    }
}
