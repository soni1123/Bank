using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class SupMainScrollStationeryViewModel
    {
         public   SupMainScrollStationeryViewModel()
        {
            AvailableBranch = new List<SelectListItem>();
        }
        [Key]
        public int Id { get; set; }

        public string RecordId { get; set; }

        public int BranchId { get; set; }

        public string BankCode { get; set; }

         [DataType(DataType.DateTime)]
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

        public IList<SelectListItem> AvailableBranch { get; set; }
    }
}