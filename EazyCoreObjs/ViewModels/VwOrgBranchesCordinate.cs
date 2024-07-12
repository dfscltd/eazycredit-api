using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwOrgBranchesCordinate
    {
		public string RegionCode { get; set; }
		public string RegionName { get; set; }
		public string BranchCode { get; set; }
		public string BranchName { get; set; }
		public string BranchStreet { get; set; }
		public string BranchCity { get; set; }
		public string BranchMnemonic { get; set; }
		public string CsuCode { get; set; }
		public string HeadOpsCode { get; set; }
		public string AxDataAreaId { get; set; }
		public bool AllowFcyTrans { get; set; }
		public string AddedBy { get; set; }
		public string LastModifiedBy { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }

	}
}
