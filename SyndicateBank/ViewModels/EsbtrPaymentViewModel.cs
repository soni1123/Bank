using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.ViewModels
{
    public class EsbtrPaymentViewModel
    {

        public EsbtrPaymentViewModel()
        {
            AvailableDistrict = new List<SelectListItem>();
            AvailableOffice = new List<SelectListItem>();
            AvailablePayerIds = new List<SelectListItem>();
            ArticleList = new List<SelectListItem>();
            MovabilityList = new List<SelectListItem>();
            StateList = new List<SelectListItem>();
        }

        # region paymentdetails

        public string RECEIPT_TYPE { get; set; }

        public string PAYMENT_TYPE { get; set; }

        public string TRANSACTION_ID { get; set; }

        public string TREA_CODE { get; set; }

        public string OFFICE_CODE { get; set; }

        public string HOA1 { get; set; }

        public decimal AMOUNT1 { get; set; }

        public string HOA2 { get; set; }

        public decimal AMOUNT2 { get; set; }

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

        #endregion

        #region  OtherPartyDetail

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

        #endregion

        #region DutyPayerDetails

        public string PayerModeNames { get; set; }

        public string StampPurchaserFname { get; set; }

        public string StampPurchaserMname { get; set; }

        public string StampPurchaserLname { get; set; }

        public string StampPurchaserName { get; set; }

        public string PartyId { get; set; }

        public string StampPurchaserMobileNo { get; set; }

        public string EmailId { get; set; }

        public string PayerNoId { get; set; }

        #endregion

        #region PropertyDetails

        public string Article_Code { get; set; }

        public string Prop_Movability { get; set; }

        public string Prop_Addr_Line1 { get; set; }


        public string Prop_Addr_Line2 { get; set; }


        public string Prop_Addr_Line3 { get; set; }


        public string Prop_Addr_Line4 { get; set; }


        public string Road { get; set; }


        public string Town_Village { get; set; }


        public string District { get; set; }


        public string State { get; set; }

        public decimal Prop_Pincode { get; set; }

        public double Consideration_Amount { get; set; }

        public decimal Prop_Area_Units { get; set; }

        public string ESBTR_PA_UOM { get; set; }

        #endregion

        public IList<SelectListItem> AvailableDistrict { get; set; }
        public IList<SelectListItem> AvailableOffice { get; set; }
        public IList<SelectListItem> AvailablePayerIds { get; set; }
        public IList<SelectListItem> ArticleList { get; set; }
        public IList<SelectListItem> MovabilityList { get; set; }
        public IList<SelectListItem> StateList { get; set; }
    }
}