using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class OrgBranch
    {
        public string BranchCode {  get; set; }
        public string RegionCode {  get; set; }
        public string BranchName {  get; set; }
        public string BranchStreet {  get; set; }
        public string BranchCity {  get; set; }
        public string CsuCode {  get; set; }
        public string HeadOpsCode {  get; set; }
        public string BranchMnemonic {  get; set; }
        public string AxDataAreaId {  get; set; }
        public bool AllowFcyTrans {  get; set; }
        public string AddedBy {  get; set; }
        public string LastModifiedBy {  get; set; }
        public DateTime DateAdded {  get; set; }
        public DateTime DateLastModified {  get; set; }
        public TimeSpan TimeAdded {  get; set; }
        public TimeSpan TimeLastModified {  get; set; }
    }
}
