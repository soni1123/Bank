using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    public partial class BranchStockDetail_CustomerSell_MappingModel
    {
        public int Id { get; set; }

        public int? BranchStockDetailId { get; set; }

        public int? CustomerSalelogId { get; set; }

        public int? Quantity { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        //public virtual BranchStockDetailModel BranchStockDetail { get; set; }

        //public virtual CustomerSelllogModel CustomerSelllog { get; set; }
    }
}