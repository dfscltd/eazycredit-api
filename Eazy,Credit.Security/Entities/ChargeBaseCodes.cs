using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class ChargeBaseCodes
    {
        public string ChargeBaseCode {  get; set; }
        public string ChargeBaseDesc {  get; set; }
        public decimal FloorAmount {  get; set; }
        public decimal CeilingAmount {  get; set; }
        public bool Tier {  get; set; }
        public string ChargeDebitTrans {  get; set; }
        public decimal TaxRate {  get; set; }
        public string ChargeRateType {  get; set; }
        public string ChargeAppFreq {  get; set; }
        public string ChargeAppMonth {  get; set; }
        public string ChargeAppDay {  get; set; }
        public string ParentChargeCode {  get; set; }
        public string TransCode {  get; set; }
        public string TransCodeTax {  get; set; }
        public string ChargeContraGLAcct {  get; set; }
        public string ChargeTaxGLAcct { get; set; }
        public string AddedBy {  get; set; }
        public string LastModifiedBy {  get; set; }
        public DateTime DateAdded {  get; set; }
        public DateTime DateLastModified {  get; set; }
        public TimeSpan TimeAdded {  get; set; }
        public TimeSpan TimeLastModified {  get; set; }
        public string ChargeBaseCodeType {  get; set; }

    }
}
