using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("SupMainScrollStationery")]
    public class SupMainScrollStationeryModel
    {      
        [Key]
        public int Id { get; set; }

        public string RecordId { get; set; }

        //public int BranchId { get; set; }

        public int BranchId { get; set; }

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

         [ForeignKey("BranchId")]
        public virtual BranchModel branchmaster { get; set; }

    }
}