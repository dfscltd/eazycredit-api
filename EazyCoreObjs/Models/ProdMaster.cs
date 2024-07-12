using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace EazyCoreObjs.Models
{
    public partial class ProdMaster
    {
        /// <summary>
        /// Gets or Sets ProdCode
        /// </summary>
        [DataMember(Name = "ProdCode", EmitDefaultValue = false)]
        [Key]
        [Column("ProdCode")]
        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public string ProdCatCode { get; set; }
        public string CurrCode { get; set; }
        public string DebitIntBaseCode { get; set; }
        public string DebitIntAppFreq { get; set; }
        public byte? DebitIntAppMonth { get; set; }
        public byte? DebitIntAppDay { get; set; }
        public string DebitIntCalcBalType { get; set; }
        public string DebitIntAppMethod { get; set; }
        public string CreditIntBaseCode { get; set; }
        public string CreditIntAppFreq { get; set; }
        public byte? CreditIntAppMonth { get; set; }
        public byte? CreditIntAppDay { get; set; }
        public string CreditIntCalcBalType { get; set; }
        public string CreditIntAppMethod { get; set; }
        public string CotchargeBaseCode { get; set; }
        public string CotchargeAppFreq { get; set; }
        public byte? CotchargeAppMonth { get; set; }
        public byte? CotchargeAppDay { get; set; }
        public DateTime? LastDebitIntAppDate { get; set; }
        public DateTime? LastCreditIntAppDate { get; set; }
        public DateTime? LastCotappDate { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateLastModified { get; set; }
        public TimeSpan? TimeAdded { get; set; }
        public TimeSpan? TimeLastModified { get; set; }
        public string CreditClassSchemeCode { get; set; }
        public string ArrearsProdCode { get; set; }
        public string SegmentId { get; set; }
        public int? PriorityOrder { get; set; }

    }
}
