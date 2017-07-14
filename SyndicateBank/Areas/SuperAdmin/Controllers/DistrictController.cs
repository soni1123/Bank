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
    public class DistrictController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult List(string query="")       
        {
            var model = new DistrictViewModel();
            DropDownList(model,query);
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            var aleredit = TempData["Edit"];

            ViewBag.EditResult = aleredit;
            return View(model);
        }

        private void DropDownList(DistrictViewModel model, string query = "")
        {
            var dists = def.DistrictMastes.Where(r => r.Deleted == false).ToList();
            var dist = dists.Where(r => r.DistrictName.Contains(query));
            foreach (var item in dist)
            {
                model.AvailableDistrict.Add(item);
            }

            var state = def.StateMasters.Where(r => r.Deleted == false && r.Status == true).ToList();
            foreach (var item in state)
            {
                model.Availablestate.Add(item);
            }

            var country = def.Countries.Where(r => r.Deleted == false && r.Status == true).ToList();
            foreach (var item in country)
            {
                model.AvailableCountry.Add(item);
            }

        }


        [HttpGet]
        public ActionResult Create(int Id)
        {
            var state = def.StateMasters.Where(r => r.Status && r.Id == Id).FirstOrDefault();
            var model = new DistrictViewModel();
            DropDownList(model);
            if (state != null)
            {
                model.StateId = state.Id;
                model.StateName = state.StateName;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(DistrictViewModel model)
        {
            var mod = new DistrictModel();
            if (ModelState.IsValid)
            {
                mod.CreatedOn = System.DateTime.Now;
                mod.CreatedBy = model.CreatedBy;
                mod.UpdatedOn = System.DateTime.Now;
                mod.UpdatedBy = model.UpdatedBy;
                mod.DistrictCode = model.DistrictCode;
                mod.DistrictName = model.DistrictName;
                mod.StateId = model.StateId;
                mod.Status = model.Status;
                mod.Deleted = false;
                def.DistrictMastes.Add(mod);
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("District Details Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
           
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var dist = def.DistrictMastes.Where(a => a.Id == id).FirstOrDefault();
            var model = new DistrictViewModel();
            if (dist != null)
            {
                model.Id = dist.Id;
                model.StateId = dist.StateId;
                model.StateName = dist.state.StateName;
                model.DistrictCode = dist.DistrictCode;
                model.DistrictName = dist.DistrictName;
                model.Status = dist.Status;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(DistrictViewModel model)    //int Id,int stateId,string distCode,string distName,bool status
        {
            var dist = def.DistrictMastes.Where(a => a.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                dist.StateId = model.StateId;
                dist.DistrictCode = model.DistrictCode;
                dist.DistrictName = model.DistrictName;
                dist.UpdatedOn = System.DateTime.Now;
                dist.UpdatedBy = 0;
                dist.Status = model.Status;
                def.Entry(dist).State = EntityState.Modified;
                def.SaveChanges();
                ViewBag.EditResult = AM.messagesuccess("Branch Details updated Sucessfully...");
                TempData["Edit"] = ViewBag.EditResult;
            }
          
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var model = def.DistrictMastes.Where(r => r.Id == Id).FirstOrDefault();
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