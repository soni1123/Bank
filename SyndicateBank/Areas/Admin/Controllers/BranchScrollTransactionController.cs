using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SyndicateBank.AlertMessages;
using SyndicateBank.Areas.Admin.Models;
using SyndicateBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.Areas.Admin.Controllers
{
    [Authorize]
    public class BranchScrollTransactionController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();
        public ActionResult Index()
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            return RedirectToAction("List");

        }


        public ActionResult List(string query = "")
        {

            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            var deletescroll= def.branchscrolltransaction.ToList().Where(r => r.Deleted == false);    
            return View(deletescroll.Where(r => r.BankCode.Contains(query) || r.MainScrollNumber.Contains(query) || r.GRN.ToString().Contains(query) || r.StationeryNumber.ToString().Contains(query) || r.PrintBankBranch.ToString().Contains(query) ||r.EsbtrAmount.ToString().Contains(query)).OrderByDescending(r => r.Id).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var model = new BranchScrollTransactionModel();
            model.RecordId = "SCR";
            model.MainScrollDate = System.DateTime.Today;
            model.PrintTimestamp = System.DateTime.Today;

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(BranchScrollTransactionModel stocktranmodel)
        {
            var getuser = User.Identity.Name;
            var getuserdeatils = def.UserDetails.Where(r => r.UserName == getuser);
            var getdata = def.SupMainTransactionStationerys.Where(r => r.BranchCode == getuserdeatils.FirstOrDefault().BranchId.ToString());
            if (getdata.FirstOrDefault().MainScrollNumber == stocktranmodel.MainScrollNumber)
            {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = def.Database.BeginTransaction())
                {
                    try
                    {
                        stocktranmodel.CreatedOn = System.DateTime.Now;
                        stocktranmodel.UpdatedOn = System.DateTime.Now;
                        stocktranmodel.CreatedBy = 1;
                        stocktranmodel.UpdatedBy = 1;
                        stocktranmodel.RecordId = "SCR";
                        stocktranmodel.MainScrollDate = System.DateTime.UtcNow;
                        stocktranmodel.PrintTimestamp = System.DateTime.UtcNow;
                        def.branchscrolltransaction.Add(stocktranmodel);
                        def.SaveChanges();
                        var getdatascroll = def.branchscrollstationery.Where(r => r.MainScrollNumber == stocktranmodel.MainScrollNumber).FirstOrDefault();
                        if (getdatascroll != null)
                        {
                            int count = getdatascroll.TotalStationeryUsed;
                            getdatascroll.TotalStationeryUsed = count + 1;
                            def.Entry(getdatascroll).State = EntityState.Modified;
                            def.SaveChanges();
                            ViewBag.Result = AM.messagesuccess("Added BranchScroll Transaction Sucessfully");
                            TempData["tempalert"] = ViewBag.Result;
                            dbContextTransaction.Commit();
                            return RedirectToAction("List");
                        }
                        else
                        {
                            ViewBag.Result = AM.messagesuccess("Added BranchScroll Transaction Failed");
                            TempData["tempalert"] = ViewBag.Result;
                            return RedirectToAction("List");
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
        }
        }
            else
            {
                ViewBag.Error = AM.messagesuccess("Please enter Correct MAinScroll Number");
                stocktranmodel.MainScrollNumber = "";
                return View(stocktranmodel);
            }

            return View(stocktranmodel);
        }

        public ActionResult Edit(int id)
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var model = new BranchScrollTransactionModel();
            var branchscrollEdit = def.branchscrolltransaction.Where(a => a.Id == id);
            model.Id = branchscrollEdit.FirstOrDefault().Id;
            model.RecordId = "SCR";
            model.BankCode = branchscrollEdit.FirstOrDefault().BankCode;
            model.MainScrollDate = branchscrollEdit.FirstOrDefault().MainScrollDate;
            model.MainScrollNumber = branchscrollEdit.FirstOrDefault().MainScrollNumber;
            model.GRN = branchscrollEdit.FirstOrDefault().GRN;
            model.StationeryNumber = branchscrollEdit.FirstOrDefault().StationeryNumber;
            model.PrintStatus = branchscrollEdit.FirstOrDefault().PrintStatus;
            model.PrintTimestamp = branchscrollEdit.FirstOrDefault().PrintTimestamp;
            model.PrintBankBranch = branchscrollEdit.FirstOrDefault().PrintBankBranch;
            model.EsbtrAmount = branchscrollEdit.FirstOrDefault().EsbtrAmount;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(BranchScrollTransactionModel model)
        {
             var getuser = User.Identity.Name;
            var getuserdeatils = def.UserDetails.Where(r => r.UserName == getuser);
            var getdata = def.SupMainTransactionStationerys.Where(r => r.BranchCode == getuserdeatils.FirstOrDefault().BranchId.ToString());
            if (getdata.FirstOrDefault().MainScrollNumber == model.MainScrollNumber)
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = 1;
                    model.UpdatedOn = System.DateTime.Now;
                    model.CreatedOn = System.DateTime.Now;
                    model.UpdatedBy = 1;
                    model.RecordId = "SCR";
                    def.Entry(model).State = EntityState.Modified;
                    def.SaveChanges();
                    ViewBag.Result = AM.messagesuccess("Edit BranchScroll Transaction Sucessfully");
                    TempData["tempalert"] = ViewBag.Result;
                    return RedirectToAction("List");
                }
            }
            else
            {
                ViewBag.Error = AM.messagesuccess("Please enter Correct MAinScroll Number");
                model.MainScrollNumber = "";
                return View(model);
            }
            return RedirectToAction("List");
            //return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var branchdelete = def.branchscrolltransaction.Where(r => r.Id == Id).ToList().FirstOrDefault();
            if (ModelState.IsValid)
            {
                branchdelete.Deleted = true;
                def.Entry(branchdelete).State = EntityState.Modified;
                def.SaveChanges();
            }
            ViewBag.Result = AM.messagesuccess("Deleted Sucessfully");

            return Json(branchdelete.Deleted, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private Boolean IsSuperAdmin()
        {
            var user = User.Identity;

            var usrRoles = def.ViewModels.Where(m => m.UserName == user.Name).ToList();
            var usrnR = (usrRoles.Count > 0) ? usrRoles.FirstOrDefault() : new ViewModel();

            string Role = "";

            if (usrRoles.Count > 0)
                Role = usrnR.RoleName;

            if (Role.ToLower() == "super admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}