using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace EazyCoreObjs.Models
{
    [DataContract]
    [Table("PrmCurrencies", Schema = "dbo")]
    public class PrmCurrencies
    {
        public string CurrCode { get; set; }
        public string CurrDesc { get; set; }
    }
}
