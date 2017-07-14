using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SyndicateBank.AlertMessages;
using SyndicateBank.Areas.Admin.Models;
using SyndicateBank.Areas.SuperAdmin.Models;
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
    public class BranchScrollStationeryController : Controller
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

      
        public ActionResult _SuperStockTranDetails()
        {
            var getuser = User.Identity.Name;
            var getuserdeatils = def.UserDetails.Where(r => r.UserName == getuser);
            var getdata = def.SupMainTransactionStationerys.Where(r => r.BranchCode == getuserdeatils.FirstOrDefault().BranchId.ToString());
            var mod = def.SupMainTransactionStationerys.ToList().Where(r => r.Deleted == false && r.MainScrollNumber==getdata.FirstOrDefault().MainScrollNumber);

            return View(mod);
        }

        //private void PrepareList(SupMainTransactionStationeryModel mod, SupMainTransactionStationeryViewModel model)
        //{
        //    model.BankCode = mod.BankCode;
        //    model.BranchCode = mod.BranchCode;
        //    model.
        //}

        public ActionResult List(string query = "")
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            ViewBag.qur = query;
            var deletelist = def.branchscrollstationery.ToList().Where(r => r.Deleted == false);
            return View(deletelist.Where(r => r.BankCode.Contains(query) || r.MainScrollNumber.Contains(query) || r.StationeryPrinted.ToString().Contains(query) || r.StationeryDamaged.ToString().Contains(query) || r.TotalAmount.ToString().Contains(query)).OrderByDescending(r => r.Id).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var model = new BranchScrollStationeryModel();
            model.RecordId = "MSCR";
            model.BankCode = "SYN";
            model.MainScrollDate = System.DateTime.Today;
            return View(model);


        }
        [HttpPost]
        public ActionResult Create(BranchScrollStationeryModel stockmodel)
        {
              var getuser = User.Identity.Name;
            var getuserdeatils = def.UserDetails.Where(r => r.UserName == getuser);
            var getdata = def.SupMainTransactionStationerys.Where(r => r.BranchCode == getuserdeatils.FirstOrDefault().BranchId.ToString());
            if (getdata.FirstOrDefault().MainScrollNumber == stockmodel.MainScrollNumber)
            {
                //var branch = new BranchScrollStationeryModel();
                if (ModelState.IsValid)
                {
                    if (def.branchscrollstationery.Where(r => r.MainScrollNumber.Contains(stockmodel.MainScrollNumber)) == null)
                    {
                        stockmodel.CreatedOn = System.DateTime.Today;
                        stockmodel.UpdatedOn = System.DateTime.Today;
                        stockmodel.CreatedBy = 1;
                        stockmodel.UpdatedBy = 1;
                        stockmodel.RecordId = "MSCR";
                        stockmodel.MainScrollDate = stockmodel.MainScrollDate;
                        def.branchscrollstationery.Add(stockmodel);
                        def.SaveChanges();
                        ViewBag.Result = AM.messagesuccess("Added Branch ScrollStationery Sucessfully");
                        TempData["tempalert"] = ViewBag.Result;
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ViewBag.Error = AM.messagesuccess("Main Scroll Number Alredy there");
                        stockmodel.MainScrollNumber = "";
                        return View(stockmodel);
                    }
                }
            }
            else
            {
                ViewBag.Error = AM.messagesuccess("Please enter Correct MAinScroll Number");
                stockmodel.MainScrollNumber = "";
                return View(stockmodel);
            }
            return View(stockmodel);
        }

        public ActionResult Edit(int id)
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var model = new BranchScrollStationeryModel();
            var branchscrollEdit = def.branchscrollstationery.Where(a => a.Id == id);
            model.Id = branchscrollEdit.FirstOrDefault().Id;
            model.RecordId = "MSCR";
            model.BankCode = branchscrollEdit.FirstOrDefault().BankCode;
            model.MainScrollDate = branchscrollEdit.FirstOrDefault().MainScrollDate;
            model.MainScrollNumber = branchscrollEdit.FirstOrDefault().MainScrollNumber;
            model.StationeryPrinted = branchscrollEdit.FirstOrDefault().StationeryPrinted;
            model.StationeryDamaged = branchscrollEdit.FirstOrDefault().StationeryDamaged;
            model.TotalStationeryUsed = branchscrollEdit.FirstOrDefault().TotalStationeryUsed;
            model.TotalAmount = branchscrollEdit.FirstOrDefault().TotalAmount;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(BranchScrollStationeryModel model)
        {
               var getuser = User.Identity.Name;
            var getuserdeatils = def.UserDetails.Where(r => r.UserName == getuser);
            var getdata = def.SupMainTransactionStationerys.Where(r => r.BranchCode == getuserdeatils.FirstOrDefault().BranchId.ToString());
            if (getdata.FirstOrDefault().MainScrollNumber == model.MainScrollNumber)
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = 1;
                    model.UpdatedOn =  System.DateTime.Today;
                    model.CreatedOn =  System.DateTime.Today;
                    model.UpdatedBy = 1;
                    model.RecordId = "MSCR";
                    def.Entry(model).State = EntityState.Modified;
                    def.SaveChanges();
                    ViewBag.Result = AM.messagesuccess("Edit Branch ScrollStationery Sucessfully");
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
            var branchdelete = def.branchscrollstationery.Where(r => r.Id == Id).ToList().FirstOrDefault();
            if (ModelState.IsValid)
            {
                branchdelete.Deleted = true;
                def.Entry(branchdelete).State = EntityState.Modified;
                def.SaveChanges();
            }
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