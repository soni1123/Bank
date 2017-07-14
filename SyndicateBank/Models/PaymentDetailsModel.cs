     using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

namespace SyndicateBank.Models
{
    [Table("PaymentDetails")]
    public partial class PaymentDetailsModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(2)]
        public string RECEIPT_TYPE { get; set; }

        [Required]
        [StringLength(2)]
        public string PAYMENT_TYPE { get; set; }

        [Required]
        [StringLength(20)]
        public string TRANSACTION_ID { get; set; }

        [Required]
        [StringLength(10)]
        public string TREA_CODE { get; set; }

        [Required]
        [StringLength(6)]
        public string OFFICE_CODE { get; set; }

        [Required]
        [StringLength(15)]
        public string HOA1 { get; set; }

        public decimal AMOUNT1 { get; set; }

        [StringLength(15)]
        public string HOA2 { get; set; }

        public decimal? AMOUNT2 { get; set; }

        [StringLength(15)]
        public string HOA3 { get; set; }

        public decimal? AMOUNT3 { get; set; }

        [StringLength(15)]
        public string HOA4 { get; set; }

        public decimal? AMOUNT4 { get; set; }

        [StringLength(15)]
        public string HOA5 { get; set; }

        public decimal? AMOUNT5 { get; set; }

        [StringLength(15)]
        public string HOA6 { get; set; }

        public decimal? AMOUNT6 { get; set; }

        [StringLength(15)]
        public string HOA7 { get; set; }

        public decimal? AMOUNT7 { get; set; }

        [StringLength(15)]
        public string HOA8 { get; set; }

        public decimal? AMOUNT8 { get; set; }

        [StringLength(15)]
        public string HOA9 { get; set; }

        public decimal? AMOUNT9 { get; set; }

        public decimal CHALLAN_AMOUNT { get; set; }
    }
}

