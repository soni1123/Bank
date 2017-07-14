using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.Admin.Models
{
    [Table("BranchScrollStationery")]
    public class BranchScrollStationeryModel
    {
        public int Id { get; set; }

        [StringLength(4)]
        public string RecordId { get; set; }

        [StringLength(3)]
        public string BankCode { get; set; }
      
        public DateTime MainScrollDate { get; set; }

        [StringLength(10)]
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
    }
}