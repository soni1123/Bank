using SyndicateBank.Areas.SuperAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.Admin.Models
{
    public class BranchScrollStationeryViewModel
    {
        public BranchScrollStationeryViewModel()
        {
            //AvailableBranchScrollStationery = new List<BranchScrollStationeryModel>();
           
        }
        public int Id { get; set; }

        public string RecordId { get; set; } 

        public string BankCode { get; set; }

        public DateTime MainScrollDate { get; set; }

        public string MainScrollNumber { get; set; }

        public int StationeryPrinted { get; set; }

        public int StationeryDamaged { get; set; }

        public int TotalStationeryUsed { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public bool Deleted { get; set; }

       // public IList<BranchScrollStationeryModel> AvailableBranchScrollStationery { get; set; }
      

    }
}