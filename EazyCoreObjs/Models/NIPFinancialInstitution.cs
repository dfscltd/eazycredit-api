using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("NIPFinancialInstitution", Schema = "dbo")]
    public class NIPFinancialInstitution
    {
        public string BatchNumber { get; set; }
        public int ChannelCode { get; set; }
        public string TransactionLocation { get; set; }
        public string InstitutionCode { get; set; }
        public string InstitutionName { get; set; }
        public int Category { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
