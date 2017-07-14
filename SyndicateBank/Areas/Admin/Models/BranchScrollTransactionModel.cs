using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.Admin.Models
{
    [Table("BranchScrollTransactionStationery")]
    public class BranchScrollTransactionModel
    {
        public BranchScrollTransactionModel()
        {

        }
        public int Id { get; set; }

        [StringLength(3)]
        public string RecordId { get; set; }

        [StringLength(3)]
        public string BankCode { get; set; }

        //[StringLength(8)]
        public DateTime MainScrollDate { get; set; }

        [StringLength(10)]
        public string MainScrollNumber { get; set; }

        [StringLength(18)]
        public string GRN { get; set; }

        [StringLength(25)]
        public string StationeryNumber { get; set; }

        [StringLength(1)]
        public string PrintStatus { get; set; }

        //[StringLength(14)]
        public DateTime PrintTimestamp { get; set; }

        [StringLength(50)]
        public string PrintBankBranch { get; set; }

        public decimal EsbtrAmount { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}