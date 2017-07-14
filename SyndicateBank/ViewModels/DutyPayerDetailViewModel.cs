
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SyndicateBank.ViewModels
{
    public partial class DutyPayerDetailViewModel
    {
        public DutyPayerDetailViewModel()
        {
            AvailablePayerIds = new List<SelectListItem>();
        }

        public int Id { get; set; }

        public string TransactionId { get; set; }

        public string StampPurchaserFname { get; set; }

        public string StampPurchaserMname { get; set; }

        public string StampPurchaserLname { get; set; }

        public string StampPurchaserName { get; set; }

        public string PartyId { get; set; }

        public string StampPurchaserMobileNo { get; set; }

        public string EmailId { get; set; }

        public string PayerModeName { get; set; }
        public string PayerNoId { get; set; }

        public bool Deleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public IList<SelectListItem> AvailablePayerIds { get; set; }
    }
}
