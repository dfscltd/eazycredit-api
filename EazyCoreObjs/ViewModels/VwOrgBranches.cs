﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwOrgBranches
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
		public DateTime DateAdded { get; set; }
		public DateTime DateLastModified { get; set; }
		public TimeSpan TimeAdded { get; set; }
		public TimeSpan TimeLastModified { get; set; }
		public string BankName { get; set; }
		public string CsuName { get; set; }
		public string HeadOpsName { get; set; }
	}
}
