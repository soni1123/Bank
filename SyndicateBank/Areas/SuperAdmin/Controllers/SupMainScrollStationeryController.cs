using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SyndicateBank.AlertMessages;
using SyndicateBank.Areas.SuperAdmin.Models;
using SyndicateBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.Areas.SuperAdmin.Controllers
{
     [Authorize]
    public class SupMainScrollStationeryController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();

        public ActionResult Index()
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            return RedirectToAction("List");

        }

        //[HttpGet]
        public ActionResult List(string query = "")
        {

            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            ViewBag.qur = query;

            //var model = new SupMainScrollStationeryViewModel();

            //var alllist = def.SupMainScrollStationary.ToList();

            //foreach (var item in collection)
            //{
		 
            //}
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;

            return View(def.SupMainScrollStationary.ToList().Where(r => r.branchmaster.BranchCode.Contains(query) || r.MainScrollNumber.Contains(query) || r.StationeryPrinted.ToString().Contains(query) || r.StationeryDamaged.ToString().Contains(query) || r.TotalAmount.ToString().Contains(query) || r.MainScrollDate.ToString().Contains(query) ).OrderByDescending(r => r.Id).ToList());           
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var model = new SupMainScrollStationeryViewModel();
            DropDownList(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var recept = def.SupMainScrollStationary.Where(r => r.Id == Id).ToList().FirstOrDefault();
            if (ModelState.IsValid)
            {
                recept.Deleted = true;
                def.Entry(recept).State = EntityState.Modified;
                def.SaveChanges();
            }
            return Json(recept.Deleted, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int Id)
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var model = new SupMainScrollStationeryViewModel();
            var recept = def.SupMainScrollStationary.Where(r => r.Id == Id).ToList();

            var recepts = def.BranchMasters.Where(r => r.Deleted == false && r.Status == true).ToList();
            model.AvailableBranch.Add(new SelectListItem { Text = "--SELECT--", Value = "0" });
            foreach (var item in recepts)
            {
                model.AvailableBranch.Add(new SelectListItem { Text = item.BranchCode + "-" + item.BranchName, Value = item.Id.ToString(), Selected = (recepts != null) ? (recept.ElementAt(0).BranchId == item.Id) : (false) });
            }

            model.BankCode = recept.ElementAt(0).BankCode;
            model.BranchId = recept.ElementAt(0).BranchId;
            model.MainScrollDate = recept.ElementAt(0).MainScrollDate;
            model.MainScrollNumber = recept.ElementAt(0).MainScrollNumber;
            model.RecordId = recept.ElementAt(0).RecordId;
            model.StationeryDamaged = recept.ElementAt(0).StationeryDamaged;
            model.StationeryPrinted = recept.ElementAt(0).StationeryPrinted;
            model.TotalStationeryUsed = recept.ElementAt(0).TotalStationeryUsed;
            model.TotalAmount = recept.ElementAt(0).TotalAmount;


            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SupMainScrollStationeryViewModel model)
        {
            var supmainscroll =   def.SupMainScrollStationary.Find(model.Id);
            if (model != null)
            {
                supmainscroll.BankCode = model.BankCode;
                supmainscroll.BranchId = model.BranchId;
                supmainscroll.MainScrollDate = model.MainScrollDate;
                supmainscroll.MainScrollNumber = model.MainScrollNumber;
                supmainscroll.RecordId = model.RecordId;
                supmainscroll.StationeryDamaged = model.StationeryDamaged;
                supmainscroll.StationeryPrinted = model.StationeryPrinted;
                supmainscroll.TotalStationeryUsed = model.TotalStationeryUsed;
                supmainscroll.TotalAmount = model.TotalAmount;

                def.Entry(supmainscroll).State = EntityState.Modified;
                //defcon.Countries.u(model);
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Main scroll Updated Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
              return RedirectToAction("List");
            }
           
            return RedirectToAction("List");
        }

        private void DropDownList(SupMainScrollStationeryViewModel model)
        {
            var recept = def.BranchMasters.Where(r => r.Deleted == false && r.Status == true).ToList();
            model.AvailableBranch.Add(new SelectListItem { Text = "--SELECT--", Value = "0" });
            foreach (var item in recept)
            {
                model.AvailableBranch.Add(new SelectListItem { Text = item.BranchCode + "-" + item.BranchName, Value = item.Id.ToString() });
            }

            model.BankCode = "SYN";
            model.RecordId = "SMSCR";
            model.MainScrollDate = DateTime.UtcNow;

        }

        //[NonAction]
        //private void GenerateList(SupMainScrollStationeryModel model)
        //{
        //    var stockList = def.SupMainScrollStationary.ToList();
        //    foreach (var items in stockList)
        //    {
        //        var Liststocks = new SupMainScrollStationeryModel.SupMainScrollStationery();
        //        Liststocks.Id = items.Id;
        //        Liststocks.StampName = items.StampName;
        //        Liststocks.Quantity = items.Quantity;
        //        Liststocks.PriceValue = items.PriceValue;
        //        Liststocks.MinItemQty = items.MinItemQty;
        //        Liststocks.StampRange = items.StampRange;
        //        Liststocks.Status = items.Status;
        //        model.Availablestock.Add(Liststocks);
        //    }
        //}

        [HttpPost]
        public ActionResult Create(SupMainScrollStationeryModel stockmodel)
        {
            if (ModelState.IsValid)
            {
                stockmodel.CreatedOn = System.DateTime.UtcNow;
                stockmodel.UpdatedOn = System.DateTime.UtcNow;
                stockmodel.UpdatedBy = 1;
                stockmodel.CreatedBy = 1;
                def.SupMainScrollStationary.Add(stockmodel);
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Main Scroll Details Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
                return RedirectToAction("List");
            }
            
            return RedirectToAction("List");
        }

        [NonAction]
        private Boolean IsAdmin()
        {
            var user = User.Identity;

            var usrRoles = def.ViewModels.Where(m => m.UserName == user.Name).ToList();
            var usrnR = (usrRoles.Count > 0) ? usrRoles.FirstOrDefault() : new ViewModel();

            string Role = "";

            if (usrRoles.Count > 0)
                Role = usrnR.RoleName;


            if (Role.ToLower() == "admin")
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