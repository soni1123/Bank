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
    public class SupMainTransactionStationeryController:Controller
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
        public ActionResult List(string query="")
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            //var model = new SupMainTransactionStationeryViewModel();
            //DropDownList(model);
            //return View(model);

            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            ViewBag.qur = query;
            var listscrolls = def.SupMainTransactionStationerys.ToList().Where(r => r.Deleted == false);
            return View(listscrolls.Where(r => r.BankCode.Contains(query) || r.MainScrollNumber.Contains(query) || r.StationeryNumber.ToString().Contains(query) || r.MainScrollDate.Equals(Convert.ToDateTime(query))).OrderByDescending(r => r.Id).ToList());
         //   return View(def.SupMainTransactionStationerys.ToList().Where(r=>r.Deleted==false));

        }

        private void DropDownList(SupMainTransactionStationeryViewModel model)
        {

            var branches = def.BranchMasters.Where(r => r.Deleted == false && r.Status == true).ToList();
            model.AvailableSupStationerys.Add(new SelectListItem { Text = "--SELECT--", Value = "0" });
            foreach (var item in branches)
            {
                model.AvailableSupStationerys.Add(new SelectListItem { Text = item.BranchName + " (" + item.BranchCode + " )", Value = item.Id.ToString() });
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            //var supStationerys = def.SupMainTransactionStationerys.Where(r => r.Id == Id).FirstOrDefault();
            var model = new SupMainTransactionStationeryViewModel();

           DropDownList(model);


            model.RecordId = "SSCR";
            model.BankCode = "SYN";
          //  model.AvailableSupStationerys = null;

            //}
            return View(model);
        }

         [HttpPost]
        public string GetDatatoMainScroll(int id)
        {
            var lo = new string[2];
            var getdetails = def.SupMainScrollStationary.Where(r => r.BranchId == id);
            if (getdetails.Count() != 0)
                lo[0] = getdetails.FirstOrDefault().MainScrollNumber;
            else
                lo[0] = "";
            return lo[0];

        }


        [HttpPost]
        public ActionResult Create(SupMainTransactionStationeryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MainScrollNumber != null)
                {
                    model.CreatedOn = System.DateTime.Today;
                    model.CreatedBy = 1;
                    model.UpdatedOn = System.DateTime.Now;
                    model.UpdatedBy = 1;
                    model.Deleted = false;
                    def.SupMainTransactionStationerys.Add(model);
                    def.SaveChanges();
                    ViewBag.Result = AM.messagesuccess("Main Transaction Added Sucessfully...");
                    TempData["tempalert"] = ViewBag.Result;
                }
                else
                {
                    ViewBag.Result = AM.messagesuccess("Plzz enter correct main scroll number Sucessfully...");
                    TempData["tempalert"] = ViewBag.Result;
                    var viewmodel = new SupMainTransactionStationeryViewModel();
                    viewmodel.BankCode = model.BankCode;
                    viewmodel.BranchCode = model.BranchCode;
                    viewmodel.MainScrollDate = model.MainScrollDate;
                    viewmodel.MainScrollNumber = model.MainScrollNumber;
                    viewmodel.RecordId = model.RecordId;
                    viewmodel.StationeryNumber = model.StationeryNumber;
                    DropDownList(viewmodel);
                    return View(viewmodel);
                }
            }
            return RedirectToAction("List");
        }


        public ActionResult Edit(int id)
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var model = new SupMainTransactionStationeryViewModel();

            var supStationerys = def.SupMainTransactionStationerys.Where(a => a.Id == id).FirstOrDefault();

            var recepts = def.BranchMasters.Where(r => r.Deleted == false && r.Status == true).ToList();
            model.AvailableSupStationerys.Add(new SelectListItem { Text = "--SELECT--", Value = "0" });
            foreach (var item in recepts)
            {
                model.AvailableSupStationerys.Add(new SelectListItem { Text = item.BranchName + " (" + item.BranchCode + " )", Value = item.Id.ToString(), Selected = (recepts != null) ? (supStationerys.BranchCode == item.BranchCode) : (false) });
            }
            model.BankCode = supStationerys.BankCode;
            model.BranchCode = supStationerys.BranchCode;
            model.MainScrollDate = supStationerys.MainScrollDate;
            model.MainScrollNumber = supStationerys.MainScrollNumber;
            model.RecordId = supStationerys.RecordId;
            model.StationeryNumber = supStationerys.StationeryNumber;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SupMainTransactionStationeryModel model)
        {
            var movabal = def.SupMainTransactionStationerys.Where(a => a.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                movabal.CreatedBy = 1;
                movabal.UpdatedOn = System.DateTime.Now;
                movabal.UpdatedBy =1;
                movabal.BankCode = model.BankCode;
                movabal.BranchCode = model.BranchCode;
                movabal.MainScrollDate = model.MainScrollDate;
                movabal.MainScrollNumber = model.MainScrollNumber;
                movabal.RecordId = model.RecordId;
                movabal.StationeryNumber = model.StationeryNumber;
                def.Entry(movabal).State = EntityState.Modified;
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Main Transaction Updated Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
            return RedirectToAction("List");
        }


        [HttpPost]
        public ActionResult Delete(int Id)
        {
            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var model = def.SupMainTransactionStationerys.Where(r => r.Id == Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                model.Deleted = true;
                def.Entry(model).State = EntityState.Modified;
                def.SaveChanges();
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