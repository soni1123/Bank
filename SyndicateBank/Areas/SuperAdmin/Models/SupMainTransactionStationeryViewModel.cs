using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class SupMainTransactionStationeryViewModel
    {

        public SupMainTransactionStationeryViewModel()
        {
            AvailableSupStationerys = new List<SelectListItem>();
        }


        
        public int Id { get; set; }

        //[StringLength(3)]
        [Key]
        public string RecordId { get; set; }

        //[StringLength(3)]
        public string BankCode { get; set; }

      //  [StringLength(50)]
        public string BranchCode { get; set; }

        public DateTime MainScrollDate { get; set; }

       // [StringLength(10)]
        public string MainScrollNumber { get; set; }

       // [StringLength(25)]
        public string StationeryNumber { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        public IList<SelectListItem> AvailableSupStationerys { get; set; }

        

    }
}