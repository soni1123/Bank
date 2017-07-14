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
    public class CityController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult List()
        {
            var model = new CityViewModel();

            var alertmessage = TempData["tempalert"];

            DropDownList(model);
            ViewBag.Result = alertmessage;

            return View(model);
        }

        private void DropDownList(CityViewModel model)
        {
            var country = def.Countries.Where(r => r.Status && r.Deleted == false).ToList();
            foreach (var item in country)
            {
                model.AvailableCountry.Add(item);
            }

            var state = def.StateMasters.Where(r => r.Status && r.Deleted == false).ToList();
            foreach (var item in state)
            {
                model.AvailableState.Add(item);
            }

            var district = def.DistrictMastes.Where(r => r.Status && r.Deleted == false).ToList();
            foreach (var item in district)
            {
                model.AvailableDistrict.Add(item);
            }

            var citesList = def.Cities.Where(r => r.Deleted == false).ToList();
            foreach (var items in citesList)
            {
                model.AvailableCity.Add(items);
            }
        }

        [HttpGet]
        public ActionResult Create(int DistrictId = 0)
        {
            var model = new CityViewModel();
            DropDownList(model);
            var dist = def.DistrictMastes.Where(r => r.Status && r.Id == DistrictId).FirstOrDefault();
            if (dist != null)
            {
                model.StateId = dist.StateId;
                model.DistrictId = dist.Id;
                model.DistrictName = dist.DistrictName;
                model.StateName = dist.state.StateName;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(CityViewModel SM)
        {
            try
            {
                var model = new CityModel();
                if (ModelState.IsValid)
                {
                    model.StateId = SM.StateId;
                    model.CityCode = SM.CityCode;
                    model.CityName = SM.CityName;
                    model.DistrictId = SM.DistrictId;
                    model.CreatedOn = DateTime.Now;
                    model.UpdatedBy = 0;
                    model.UpdatedOn = DateTime.Now;
                    model.CreatedBy = 0;
                    model.Status = SM.Status;
                    model.Deleted = false;
                    def.Cities.Add(model);
                    def.SaveChanges();
                    ViewBag.Result = AM.messagesuccess("City Added Sucessfully........");

                    TempData["tempalert"] = ViewBag.Result;
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = AM.messageerror(ex.Message);
                return View(SM);

            }
            return View(SM);
        }
        public ActionResult Edit(int id)
        {
            var model = new CityViewModel();
            var cityEdit = def.Cities.Where(a => a.Id == id).FirstOrDefault();
            model.Id = cityEdit.Id;
            model.StateId = cityEdit.StateId;
            model.DistrictId = cityEdit.DistrictId;
            model.StateName = cityEdit.Districts.state.StateName;
            model.DistrictName = cityEdit.Districts.DistrictName;
            model.CityName = cityEdit.CityName;
            model.CityCode = cityEdit.CityCode;
            model.Status = cityEdit.Status;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(CityViewModel model)
        {
            var city = def.Cities.Where(r => r.Id == model.Id).ToList().FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (city != null)
                {
                    city.StateId = model.StateId;
                    city.DistrictId = model.DistrictId;
                    city.CityCode = model.CityCode;
                    city.CityName = model.CityName;
                    city.Status = model.Status;
                    city.UpdatedOn = DateTime.Now;
                    city.UpdatedBy = 0;
                    def.Entry(city).State = EntityState.Modified;
                    def.SaveChanges();
                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var model = def.Cities.Where(r => r.Id == Id).FirstOrDefault();
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