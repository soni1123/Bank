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
    public class OfficeController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();

        public ActionResult Index()
        {
            
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new OfficeViewModel();

            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;

            DropDownList(model);
            return View(model);
        }

        private void DropDownList(OfficeViewModel model)
        {
            var dist = def.DistrictMastes.Where(r => r.Status && r.Deleted == false).ToList();
            foreach (var item in dist)
            {
                model.AvailableDistrict.Add(item);
            }

            var off = def.Office.Where(r => r.Deleted == false).ToList();
            foreach (var item in off)
            {
                model.AvailableOffice.Add(item);
            }
        }

        [HttpGet]
        public ActionResult Create(int Id)
        {
            var dist = def.DistrictMastes.Where(r => r.Status && r.Id == Id).FirstOrDefault();
            var model = new OfficeViewModel();
            DropDownList(model);
            if (dist != null)
            {
                model.DistrictCode = dist.DistrictCode;
                model.DistrictName = dist.DistrictName;
                model.DistrictId = dist.Id;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(OfficeViewModel model)
        {
            var mod = new OfficeModel();
            if (ModelState.IsValid)
            {
                mod.CreatedOn = DateTime.Now;
                mod.CreatedBy = model.CreatedBy;
                mod.UpdatedOn = DateTime.Now;
                mod.UpdatedBy = model.UpdatedBy;
                mod.OfficeCode = model.OfficeCode;
                mod.OfficeName = model.OfficeName;
                mod.DistrictId = model.DistrictId;
                mod.Status = model.Status;
                mod.Deleted = false;
                def.Office.Add(mod);
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Office Details Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
           
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var off = def.Office.Where(a => a.Id == id).FirstOrDefault();

            return Json(off, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(OfficeViewModel model)
        {
            var off = def.Office.Where(a => a.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                off.DistrictId = model.DistrictId;
                off.OfficeCode = model.OfficeCode;
                off.OfficeName = model.OfficeName;
                off.UpdatedOn = DateTime.Now;
                off.UpdatedBy = model.UpdatedBy;
                off.Status = model.Status;
                def.Entry(off).State = EntityState.Modified;
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Office Details Updated Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
          
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var model = def.Office.Where(r => r.Id == Id).FirstOrDefault();
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