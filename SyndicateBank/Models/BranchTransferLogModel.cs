using SyndicateBank.Areas.Admin.Models;
using SyndicateBank.Areas.SuperAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SyndicateBank.Areas.SuperAdmin.Models;
using SyndicateBank.Areas.Admin.Models;


namespace SyndicateBank.Models
{
    [Table("BranchTransferLog")]
    public partial class BranchTransferLogModel
    {
        public int Id { get; set; }

        public int? StockDetailId { get; set; }

        public int? BranchId { get; set; }

        [StringLength(100)]
        public string StampName { get; set; }

        public int? TransferQuantity { get; set; }

        [StringLength(100)]
        public string StampRange { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        public virtual SupMainScrollStationeryModel StockDetail { get; set; }
        //public virtual BranchStockDetailModel BranchStockDetail { get; set; }

        //public virtual StockDetailModel StockDetail { get; set; }
    }
}