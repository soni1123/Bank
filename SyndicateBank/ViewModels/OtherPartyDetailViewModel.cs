
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace SyndicateBank.ViewModels
{
    public partial class OtherPartyDetailViewModel
    {
        public OtherPartyDetailViewModel()
        {
            AvailablePayerIds = new List<SelectListItem>();
        }

        [Key]
        public int Id { get; set; }

        public string TransactionId { get; set; }


        public string PayerModeName { get; set; }

        public string OtherPartyFname { get; set; }

        public string OtherPartyMname { get; set; }

        public string OtherPartyLname { get; set; }

        public string ESBTROpId { get; set; }

        public bool Deleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }


        public string PartyIdName { get; set; }

        public string PartyName { get; set; }


        public IList<SelectListItem> AvailablePayerIds { get; set; }
    }
}
