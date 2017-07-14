using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SyndicateBank.Models;
using SyndicateBank.AlertMessages;
using SyndicateBank.ViewModels;
using System.Data.Entity.Validation;

namespace SyndicateBank.Controllers
{
    public class EsbtrPaymentController : Controller
    {

        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();
        string branchName = string.Empty;
        string branchCode = string.Empty;
        int branchId = 0;
        double EsbtrAmount = 0.0;
        string paymentMode = string.Empty;
        // GET: EsbtrPayment
        public ActionResult Index()
        {
            if (TempData["BranchId"] != null)
                branchId = Convert.ToInt32(TempData["BranchId"]);

            if (TempData["esbtrAmount"] != null)
                EsbtrAmount = Convert.ToDouble(TempData["esbtrAmount"]);

            var branch = def.BranchMasters.Where(r => r.Id == branchId).FirstOrDefault();

            if (branch != null)
            {
                branchName = branch.BranchName;
                branchCode = branch.BranchCode;
            }

            paymentMode = TempData["paymentMode"].ToString();

            return RedirectToAction("EsbtrPayment");
        }

        [HttpPost]
        public dynamic GetOfficeName(string district)
        {
            var PaymentDetailsViewModel = new EsbtrPaymentViewModel();

            var OfcName = def.Office.Where(r => r.Deleted == false && r.Status == true && r.Districts.DistrictCode == district).ToList();


            PaymentDetailsViewModel.AvailableOffice.Add(new SelectListItem { Text = "-- Select --", Value = "" });
            foreach (var item in OfcName)
            {
                PaymentDetailsViewModel.AvailableOffice.Add(new SelectListItem { Text = item.OfficeCode + "-" + item.OfficeName, Value = item.OfficeCode.ToString() });
            }

            return Json(PaymentDetailsViewModel.AvailableOffice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string GetStampduty(string district)
        {
            string stampduty = "";

            if (district == "7101")
                stampduty = "0030045501-75";
            else
                stampduty = "0030046401-75";

            return stampduty;
        }

        private void DropDownList(EsbtrPaymentViewModel model)
        {

            var payerids = SyndicateBank.Models.PartyIdEnum.partyidsServices.GetAllpartyidsValue();
            foreach (var c in payerids)
                model.AvailablePayerIds.Add(new SelectListItem { Text = c.Value, Value = c.Key.ToString() });
        }

        [HttpPost]
        public ActionResult GetMovability(string articleCode)
        {
            var model = new EsbtrPaymentViewModel();
            if (articleCode != null || articleCode.Length > 0 || articleCode != " ")
            {
                var articleId = def.Article.Where(r => r.ArticleCode == articleCode).FirstOrDefault().Id;
                var mov = def.Movability.Where(r => r.ArticleId == articleId && r.Status).ToList();
                foreach (var item in mov)
                {
                    model.MovabilityList.Add(new SelectListItem { Text = item.Movability, Value = item.MovCode });
                }
            }
            return Json(model.MovabilityList);
        }


        public ActionResult EsbtrPaymentDetails()
        {
            var model = TempData["EsbtrPaymentViewModel"];

            if (model == null)
            {
                return RedirectToAction("EsbtrPayment");
            }            

            return View(model);
        }

        public ActionResult EsbtrPayment()
        {
            var PaymentDetailsViewModel = new EsbtrPaymentViewModel();

            var district = def.DistrictMastes.Where(r => r.Deleted == false && r.Status == true).ToList();

            PaymentDetailsViewModel.AvailableDistrict.Add(new SelectListItem { Text = "--SELECT--", Value = "0" });
            foreach (var item in district)
            {
                PaymentDetailsViewModel.AvailableDistrict.Add(new SelectListItem { Text = item.DistrictCode + "-" + item.DistrictName, Value = item.DistrictCode.ToString() });
            }

            var article = def.Article.Where(r => r.Status).ToList();
            PaymentDetailsViewModel.ArticleList.Add(new SelectListItem { Text = "--SELECT--", Value = "0" });
            foreach (var item in article)
            {
                PaymentDetailsViewModel.ArticleList.Add(new SelectListItem { Text = item.ArticleCode + "-" + item.ArticleName, Value = item.ArticleCode });
            }

            var state = def.StateMasters.Where(r => r.Status && r.Deleted == false).ToList();
            PaymentDetailsViewModel.StateList.Add(new SelectListItem { Text = "---SELECT---", Value = "0" });
            foreach (var item in state)
            {
                PaymentDetailsViewModel.StateList.Add(new SelectListItem { Text = item.StateName, Value = item.StateName });
            }

            PaymentDetailsViewModel.HOA1 = "0030046401-75";
            PaymentDetailsViewModel.HOA2 = "0030063301-70";


            DropDownList(PaymentDetailsViewModel);

            return View(PaymentDetailsViewModel);
        }

        [HttpPost]
        public ActionResult EsbtrPayment(EsbtrPaymentViewModel model)
        {
            var displymodel = new DisplayViewModel();

            if (ModelState.IsValid)
            {               

                using (var dbContextTransaction = def.Database.BeginTransaction())
                {
                    try
                    {
                        #region saving otherparty details

                        var model1 = new OtherPartyDetailModel();
                        var count = def.OtherPartyDetail.Count();
                        if (model.PayerModeName == "Individual")
                        {
                            var partyids = PartyIdEnum.partyidsServices.EmployeepartyidsById(Convert.ToInt32(model.ESBTROpId));
                            model1.ESBTROpId = partyids + "-" + model.PartyIdName;
                        }
                        else
                        {
                            model1.ESBTROpId = model.ESBTROpId + "-" + model.PartyIdName;
                        }
                        if (model.OtherPartyFname != null)
                            model1.OtherPartyFname = model.OtherPartyFname;
                        else
                            model1.OtherPartyFname = model.PartyName;
                        model1.OtherPartyLname = model.OtherPartyLname;
                        model1.OtherPartyMname = model.OtherPartyMname;
                        model1.PayerModeName = model.PayerModeName;
                        model1.CreatedOn = System.DateTime.Now;
                        model1.CreatedBy = 1;
                        model1.UpdatedOn = System.DateTime.Now;
                        model1.UpdatedBy = 1;
                        model1.Deleted = false;

                        model1.TransactionId = GetTransactionId();
                        def.OtherPartyDetail.Add(model1);
                        def.SaveChanges();

                        #endregion

                        #region saving payment details
                        var paymentmodel = new PaymentDetailsModel();
                        paymentmodel.AMOUNT1 = model.AMOUNT1;
                        paymentmodel.AMOUNT2 = model.AMOUNT2;
                        paymentmodel.AMOUNT3 = model.AMOUNT3;
                        paymentmodel.AMOUNT4 = model.AMOUNT4;
                        paymentmodel.AMOUNT5 = model.AMOUNT5;
                        paymentmodel.AMOUNT6 = model.AMOUNT6;
                        paymentmodel.AMOUNT7 = model.AMOUNT7;
                        paymentmodel.AMOUNT8 = model.AMOUNT8;
                        paymentmodel.AMOUNT9 = model.AMOUNT9;
                        paymentmodel.CHALLAN_AMOUNT = model.CHALLAN_AMOUNT;
                        paymentmodel.HOA1 = model.HOA1;
                        paymentmodel.HOA2 = model.HOA2;
                        paymentmodel.HOA3 = model.HOA3;
                        paymentmodel.HOA4 = model.HOA4;
                        paymentmodel.HOA5 = model.HOA5;
                        paymentmodel.HOA6 = model.HOA6;
                        paymentmodel.HOA7 = model.HOA7;
                        paymentmodel.HOA8 = model.HOA8;
                        paymentmodel.HOA9 = model.HOA9;
                        paymentmodel.OFFICE_CODE = model.OFFICE_CODE;
                        paymentmodel.PAYMENT_TYPE = "99";
                        paymentmodel.RECEIPT_TYPE = "SE";
                        paymentmodel.TREA_CODE = model.TREA_CODE;
                        paymentmodel.TRANSACTION_ID = GetTransactionId();
                        def.PaymentDetails.Add(paymentmodel);
                        def.SaveChanges();

                        #endregion

                        #region saving DutyPayerDetails

                        var dutymodel = new DutyPayerDetailModel();

                        if (model.PayerModeNames == "Individual")
                        {
                            var partyids = PartyIdEnum.partyidsServices.EmployeepartyidsById(Convert.ToInt32(model.PartyId));
                            dutymodel.PartyId = partyids.ToString() + "-" + model.PayerNoId;
                        }
                        else
                        {
                            dutymodel.PartyId = model.PartyId + "-" + model.PayerNoId;
                        }

                        dutymodel.TransactionId = GetTransactionId();


                        dutymodel.CreatedOn = System.DateTime.Now;
                        dutymodel.UpdatedOn = System.DateTime.Now;
                        dutymodel.PayerModeNames = model.PayerModeNames;

                        if (model.StampPurchaserFname != null)
                        {
                            dutymodel.StampPurchaserFname = model.StampPurchaserFname;
                        }
                        else
                        {
                            dutymodel.StampPurchaserFname = model.StampPurchaserName;
                        }
                        dutymodel.StampPurchaserLname = model.StampPurchaserLname;
                        dutymodel.StampPurchaserMname = model.StampPurchaserMname;
                        dutymodel.StampPurchaserMobileNo = model.StampPurchaserMobileNo;
                        dutymodel.EmailId = model.EmailId;
                        dutymodel.CreatedBy = 1;
                        dutymodel.UpdatedBy = 1;
                        def.DutyPayerDetail.Add(dutymodel);
                        def.SaveChanges();


                        #endregion

                        #region saving PropertyDetails

                        var propDetail = new PropertyDetailModel();
                        propDetail.Article_Code = model.Article_Code;
                        propDetail.Prop_Movability = model.Prop_Movability;
                        propDetail.Prop_Addr_Line1 = model.Prop_Addr_Line1;
                        propDetail.Prop_Addr_Line2 = model.Prop_Addr_Line2;
                        propDetail.Prop_Addr_Line3 = model.Prop_Addr_Line3;
                        propDetail.Prop_Addr_Line4 = model.Prop_Addr_Line4;
                        propDetail.Prop_Area_Units = model.Prop_Area_Units;
                        propDetail.ESBTR_PA_UOM = model.ESBTR_PA_UOM;
                        propDetail.Consideration_Amount = model.Consideration_Amount;
                        propDetail.Prop_Pincode = model.Prop_Pincode;
                        propDetail.Road = model.Road;
                        propDetail.State = model.State;
                        propDetail.Town_Village = model.Town_Village;
                        propDetail.TRANSACTION_ID = GetTransactionId();
                        propDetail.CreatedBy = 0;
                        propDetail.CreatedOn = DateTime.Now;
                        propDetail.Deleted = false;
                        propDetail.UpdatedBy = 0;
                        propDetail.UpdatedOn = DateTime.Now;


                        def.PropertyDetail.Add(propDetail);
                        try
                        {
                            def.SaveChanges();
                        }
                        catch (DbEntityValidationException dbEx)
                        {
                            Exception raise = dbEx;
                            foreach (var validationErrors in dbEx.EntityValidationErrors)
                            {
                                foreach (var validationError in validationErrors.ValidationErrors)
                                {
                                    string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                                    raise = new InvalidOperationException(message, raise);
                                }
                            }
                            throw raise;
                        }
                        #endregion

                        dbContextTransaction.Commit();

                        #region DisplayFormData

                        displymodel.DEPT_CODE = "IGR";
                        displymodel.PAYMENT_TYPE = "99-sbtr";
                        displymodel.TREASURY_CODE = model.TREA_CODE;
                        displymodel.OFFICE_CODE = model.OFFICE_CODE;
                        displymodel.REC_FIN_YEAR = "2013-2014";
                        displymodel.PERIOD = "O";
                        displymodel.FROM_DATE = System.DateTime.Now.ToString("dd/MM/yyyy");
                        displymodel.TO_DATE = "31/03/2099";
                        displymodel.MAJOR_HEAD = "0030";
                        displymodel.HOA1 = model.HOA1;
                        displymodel.HOA2 = model.HOA2;
                        displymodel.HOA3 = model.HOA3;
                        displymodel.HOA4 = model.HOA4;
                        displymodel.HOA5 = model.HOA5;
                        displymodel.HOA6 = model.HOA6;
                        displymodel.HOA7 = model.HOA7;
                        displymodel.HOA8 = model.HOA8;
                        displymodel.HOA9 = model.HOA9;
                        displymodel.AMOUNT1 = model.AMOUNT1;
                        displymodel.AMOUNT2 = model.AMOUNT2;
                        displymodel.AMOUNT3 = model.AMOUNT3;
                        displymodel.AMOUNT4 = model.AMOUNT4;
                        displymodel.AMOUNT5 = model.AMOUNT5;
                        displymodel.AMOUNT6 = model.AMOUNT6;
                        displymodel.AMOUNT7 = model.AMOUNT7;
                        displymodel.AMOUNT8 = model.AMOUNT8;
                        displymodel.AMOUNT9 = model.AMOUNT9;
                        displymodel.CHALLAN_AMOUNT = model.CHALLAN_AMOUNT;
                        displymodel.TAX_ID = "";
                        displymodel.PARTY_ID = dutymodel.PartyId;
                        displymodel.PARTY_NAME = model.StampPurchaserFname + " " + model.StampPurchaserMname + " " + model.StampPurchaserLname;
                        displymodel.ADDRESS1 = model.Prop_Addr_Line1;
                        displymodel.ADDRESS2 = model.Prop_Addr_Line2;
                        displymodel.ADDRESS3 = model.Prop_Addr_Line3;
                        displymodel.PIN_NO = model.Prop_Pincode;
                        displymodel.MOBILE_NO = model.StampPurchaserMobileNo;
                        displymodel.ARTICLE_CODE = model.Article_Code;
                        displymodel.REMARKS = "For SBTR";
                        displymodel.BANK_CODE = "SYN";
                        displymodel.BSR_CODE = branchCode;

                        if (paymentMode.ToLower() == "online")
                            displymodel.PAY_TYPE = "SE";
                        else
                            displymodel.PAY_TYPE = "SM";                             
                        
                        displymodel.BANK_REF_NO = model1.TransactionId;
                        displymodel.BANK_CIN = "63289994808123072701";
                        displymodel.BANK_TIMESTAMP = System.DateTime.Now.ToString("YYYYMMDDHHMMSS");
                        displymodel.EMAIL_ID = model.EmailId;
                        displymodel.STATUS = "Y";
                        displymodel.ESBTR_PM = model.Prop_Movability;
                        displymodel.ESBTR_CA = model.Consideration_Amount;
                        displymodel.ESBTR_PA = model.Prop_Area_Units;
                        displymodel.ESBTR_PA_UOM = model.ESBTR_PA_UOM;
                        displymodel.ESBTR_OP_ID = model1.ESBTROpId;
                        displymodel.ESBTR_OP_NAME = model.OtherPartyFname + " " + model.OtherPartyMname + " " + model.OtherPartyLname;

                      #endregion

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();

                        return View();
                    }
                }


            }

            TempData["EsbtrPaymentViewModel"] = displymodel;

            return RedirectToAction("EsbtrPaymentDetails");

        }

        [NonAction]
        private string GetTransactionId()
        {
            string transId = string.Empty;
            var totNum = def.PropertyDetail.OrderByDescending(r => r.Id).FirstOrDefault();
            int id = 0;
            if (totNum == null)
                id = 0;
            else
                id = totNum.Id;

            if (id == 0)
                id += 1;
            else
                id += 1;

            if (id < 10)
                transId = "SYN" + "0000000" + id.ToString();
            else if (id >= 10 && id < 100)
                transId = "SYN" + "000000" + id.ToString();
            else if (id >= 100 && id < 1000)
                transId = "SYN" + "00000" + id.ToString();
            else if (id >= 1000 && id < 10000)
                transId = "SYN" + "0000" + id.ToString();
            else if (id >= 10000 && id < 100000)
                transId = "SYN" + "0000" + id.ToString();
            else if (id >= 100000 && id < 1000000)
                transId = "SYN" + "000" + id.ToString();
            else if (id >= 1000000 && id < 10000000)
                transId = "SYN" + "00" + id.ToString();
            else if (id >= 10000000 && id < 100000000)
                transId = "SYN" + "0" + id.ToString();
            else if (id >= 100000000 && id < 1000000000)
                transId = "SYN" + id.ToString();

            return transId;
        }

        public ActionResult GetPayerCompany()
        {
            var model = new EsbtrPaymentViewModel();
            var payerids = PartyIdEnum.partyidsServices.EmployeepartyidsById(1);
            model.AvailablePayerIds.Add(new SelectListItem { Text = payerids, Value = payerids });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetPayerIndividual()
        {
            var model = new EsbtrPaymentViewModel();

            var payerids = SyndicateBank.Models.PartyIdEnum.partyidsServices.GetAllpartyidsValue();
            foreach (var c in payerids)
                model.AvailablePayerIds.Add(new SelectListItem { Text = c.Value, Value = c.Key.ToString() });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetPayergvt()
        {

            var model = new EsbtrPaymentViewModel();
            var payerids = PartyIdEnum.partyinfServices.EmployeepartyinfsById(6);
            model.AvailablePayerIds.Add(new SelectListItem { Text = payerids, Value = payerids });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetPayerfrgn()
        {

            var model = new EsbtrPaymentViewModel();
            var payerids = PartyIdEnum.partyinfServices.EmployeepartyinfsById(7);
            model.AvailablePayerIds.Add(new SelectListItem { Text = payerids, Value = payerids });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetDutyPayerCompany()
        {
            var model = new EsbtrPaymentViewModel();
            var payerids = PartyIdEnum.partyidsServices.EmployeepartyidsById(1);
            model.AvailablePayerIds.Add(new SelectListItem { Text = payerids, Value = payerids });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetDutyPayerIndividual()
        {
            var model = new EsbtrPaymentViewModel();

            var payerids = SyndicateBank.Models.PartyIdEnum.partyidsServices.GetAllpartyidsValue();
            foreach (var c in payerids)
                model.AvailablePayerIds.Add(new SelectListItem { Text = c.Value, Value = c.Key.ToString() });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetDutyPayergvt()
        {

            var model = new EsbtrPaymentViewModel();
            var payerids = PartyIdEnum.partyinfServices.EmployeepartyinfsById(6);
            model.AvailablePayerIds.Add(new SelectListItem { Text = payerids, Value = payerids });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetDutyPayerfrgn()
        {

            var model = new EsbtrPaymentViewModel();
            var payerids = PartyIdEnum.partyinfServices.EmployeepartyinfsById(7);
            model.AvailablePayerIds.Add(new SelectListItem { Text = payerids, Value = payerids });
            return Json(model.AvailablePayerIds, JsonRequestBehavior.AllowGet);

        }



    }
}
