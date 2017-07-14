using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("CustomerSelllog")]
    public partial class CustomerSelllogModel
    {
        public CustomerSelllogModel()
        {
            //BranchStockDetail_CustomerSell_Mapping = new HashSet<BranchStockDetail_CustomerSell_MappingModel>();
        }

        public int Id { get; set; }

        public int? StampDetailsId { get; set; }

        [StringLength(100)]
        public string CustomerName { get; set; }

        public int? BranchStockDetailId { get; set; }

        public int? BranchId { get; set; }

        [StringLength(100)]
        public string StampName { get; set; }

        public decimal? StampPrice { get; set; }

        public int? SaleQuantity { get; set; }

        public DateTime? SaleDate { get; set; }

        public int? StampNumber { get; set; }

        public int? SoldQuantity { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        //public virtual BranchStockDetailModel BranchStockDetail { get; set; }

        //public virtual ICollection<BranchStockDetail_CustomerSell_MappingModel> BranchStockDetail_CustomerSell_Mapping { get; set; }

        //public virtual StampDetailModel StampDetail { get; set; }
    }
}