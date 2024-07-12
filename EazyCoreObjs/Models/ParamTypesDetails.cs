using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("PrmTypesDetails", Schema = "dbo")]
    public class ParamTypesDetails
    {
        /// <summary>
        /// Gets or Sets TypeCode
        /// </summary>

        public short TypeCode { get; set; }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>

        public string Code { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>

        public string Description { get; set; }

    }
}
