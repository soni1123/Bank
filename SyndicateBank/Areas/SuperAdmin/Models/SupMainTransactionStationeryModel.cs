using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("SupMainTransactionStationery")]

    public class SupMainTransactionStationeryModel
    {
        public int Id { get; set; }

       // [StringLength(3)]
        public string RecordId { get; set; }

       // [StringLength(3)]
        public string BankCode { get; set; }

       // [StringLength(50)]
        public string BranchCode { get; set; }

        public DateTime MainScrollDate { get; set; }

       // [StringLength(10)]
        public string  MainScrollNumber { get; set; }

       // [StringLength(25)]
        public string StationeryNumber { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        //public IList<SelectListItem> AvailableSupStationerys { get; set; }


        //public SupMainTransactionStationeryModel()
        //{
        //    AvailableSupStationerys = new List<SelectListItem>();
        //}

    }
}