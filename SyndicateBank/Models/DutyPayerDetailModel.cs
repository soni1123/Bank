
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SyndicateBank.Models
{
    [Table("DutyPayerDetails")]
    public partial class DutyPayerDetailModel
    {

        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string TransactionId { get; set; }

        [Required]
        [StringLength(50)]
        public string PayerModeNames { get; set; }

        [Required]
        [StringLength(16)]
        public string StampPurchaserFname { get; set; }

        [StringLength(16)]
        public string StampPurchaserMname { get; set; }

        [StringLength(16)]
        public string StampPurchaserLname { get; set; }

        [StringLength(25)]
        public string PartyId { get; set; }

        [StringLength(15)]
        public string StampPurchaserMobileNo { get; set; }

        [StringLength(256)]
        public string EmailId { get; set; }

        //[StringLength(50)]
        //public string PayerMode_Id { get; set; }

        public bool Deleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

    }
}
