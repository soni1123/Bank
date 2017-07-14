using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("StampDetail")]
    public partial class StampDetailModel
    {
        public StampDetailModel()
        {
            CustomerSelllogs = new HashSet<CustomerSelllogModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string TransactionNo { get; set; }

        [StringLength(100)]
        public string District { get; set; }

        [StringLength(100)]
        public string StampType { get; set; }

        [StringLength(100)]
        public string OfficeName { get; set; }

        [StringLength(100)]
        public string StampName { get; set; }

        public decimal? StampAmount { get; set; }

        [StringLength(50)]
        public string RegistrationFeeType { get; set; }

        public decimal? RegistrationAmount { get; set; }

        public decimal? TotalAmount { get; set; }

        [StringLength(50)]
        public string DutyPayerMobileNumber { get; set; }

        [StringLength(50)]
        public string DutyPayerMailId { get; set; }

        [StringLength(100)]
        public string DutyPayerName { get; set; }

        [StringLength(50)]
        public string DutyPayerId { get; set; }

        [StringLength(100)]
        public string ArticleCode { get; set; }

        [StringLength(100)]
        public string Movability { get; set; }

        [StringLength(100)]
        public string Particulars { get; set; }

        public decimal? ConsidarationAmount { get; set; }

        [StringLength(100)]
        public string PropertyArea { get; set; }

        [StringLength(100)]
        public string OtherPartyName { get; set; }

        [StringLength(50)]
        public string OtherPartyId { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        public virtual ICollection<CustomerSelllogModel> CustomerSelllogs { get; set; }
    }
}