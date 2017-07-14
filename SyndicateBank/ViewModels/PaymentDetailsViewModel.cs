
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SyndicateBank.ViewModels
{
    public partial class PaymentDetailsViewModel
    {
        public PaymentDetailsViewModel()
        {
            AvailableDistrict = new List<SelectListItem>();
            AvailableOffice = new List<SelectListItem>();
        }
        public int ID { get; set; }
              
        public string RECEIPT_TYPE { get; set; }

        public string PAYMENT_TYPE { get; set; }

        public string TRANSACTION_ID { get; set; }

        [Required]
        public string TREA_CODE { get; set; }

        [Required]
        public string OFFICE_CODE { get; set; }

     
        public string HOA1 { get; set; }

        [Required]
        public decimal AMOUNT1 { get; set; }
        
        public string HOA2 { get; set; }

        public decimal? AMOUNT2 { get; set; }
      
        public string HOA3 { get; set; }

        public decimal? AMOUNT3 { get; set; }
      
        public string HOA4 { get; set; }

        public decimal? AMOUNT4 { get; set; }
       
        public string HOA5 { get; set; }

        public decimal? AMOUNT5 { get; set; }

        public string HOA6 { get; set; }

        public decimal? AMOUNT6 { get; set; }

        public string HOA7 { get; set; }

        public decimal? AMOUNT7 { get; set; }

        public string HOA8 { get; set; }

        public decimal? AMOUNT8 { get; set; }

        public string HOA9 { get; set; }

        public decimal? AMOUNT9 { get; set; }

        public decimal CHALLAN_AMOUNT { get; set; }

        public IList<SelectListItem> AvailableDistrict { get; set; }
        public IList<SelectListItem> AvailableOffice { get; set; }
    }
}

