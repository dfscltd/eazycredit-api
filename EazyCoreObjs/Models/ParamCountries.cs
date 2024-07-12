using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("PrmCountries", Schema = "dbo")]
    public class ParamCountries
    {
        /// <summary>
        /// Gets or Sets CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets CountryName
        /// </summary>
        public string CountryName { get; set; }



    }
}
