using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace EazyCoreObjs.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Table("ProdAccts", Schema = "dbo")]
    public class ProdAcctsMapping
    {
        public string ProdCode { get; set; }
        public string ProdAcctTypeCode { get; set; }
        public string ProdGLAcct { get; set; }
    }
}
