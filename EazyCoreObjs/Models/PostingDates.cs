using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace EazyCoreObjs.Models
{
    [DataContract]
    [Table("PrmPostingDates", Schema = "dbo")]
    public class PostingDates
    {
        [Key]
        public byte Record { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime LastPostingDate { get; set; }
        public DateTime MonthStartDate { get; set; }
        public DateTime MonthEndDate { get; set; }
        public DateTime YearEndDate { get; set; }
        public DateTime GLPostingDate { get; set; }
        public bool PostingOn { get; set; }
        public DateTime LastMonthDate { get; set; }
        public DateTime LastYearDate { get; set; }
        public DateTime LastTwoYearsDate { get; set; }
        public bool EOM { get; set; }
        public DateTime AppStartDate { get; set; }
        public DateTime PenalChargeStartDate { get; set; }
    }
}
