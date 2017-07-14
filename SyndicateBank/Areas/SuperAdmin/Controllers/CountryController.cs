using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SyndicateBank.Models;
using SyndicateBank.Areas.SuperAdmin.Models;
using SyndicateBank.AlertMessages;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace SyndicateBank.Areas.SuperAdmin.Controllers
{

    [Authorize]
    public class CountryController: Controller
    {

        DefaultConnection defcon = new DefaultConnection();
        Notification AM = new Notification();


        public ActionResult Index()
        {
            return RedirectToAction("Creates");
        }
        private void List(CountryModel model, string query = "")
        {
            var countryLists = defcon.Countries.Where(r=>r.Deleted==false). ToList();
            ViewBag.qur = query;
            var countryList = countryLists.Where(r => r.CountryName.Contains(query) || r.CountryCode.Contains(query)||r.CountryCurrency.Contains(query));

            foreach(var items in countryList)
            {
                var ListCountries = new CountryModel.Countries();
                ListCountries.Id = items.Id;
                ListCountries.CountryName = items.CountryName;
                ListCountries.CountryCode = items.CountryCode;
                ListCountries.CountryCurrency = items.CountryCurrency;
                ListCountries.Status = items.Status;
                model.AvailableCountriess.Add(ListCountries);

            }

           // return View(model);
        }
        public ActionResult Creates(string query = "")
        {
            //return View("Create",new { Area = "SuperAdmin" });
            var model=new CountryModel();
            List(model,query);
           // return View(model);
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            return View(model);

        }

        [HttpPost]
        public ActionResult Creates(CountryModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedOn = DateTime.UtcNow;
                model.CreatedBy = model.CreatedBy;
                model.UpdatedOn = DateTime.UtcNow;
                model.UpdatedBy = model.UpdatedBy;
                model.AvailableCountriess = null;
                defcon.Countries.Add(model);
                defcon.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Article Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
          
            return RedirectToAction("Creates");
        }

        public ActionResult Edit(int id)
        {
            var model = new CountryModel();
            var countryEdit = defcon.Countries.Where(a=>a.Id==id);
            model.CountryCode = countryEdit.FirstOrDefault().CountryCode;
            model.CountryName = countryEdit.FirstOrDefault().CountryName;
            model.CountryCurrency = countryEdit.FirstOrDefault().CountryCurrency;
            model.Id = countryEdit.FirstOrDefault().Id;
            model.Status = countryEdit.FirstOrDefault().Status;
            model.CreatedOn = countryEdit.FirstOrDefault().CreatedOn;
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(CountryModel model)
        {
            if (ModelState.IsValid)
            {
           
                model.CreatedBy = model.CreatedBy;
                model.UpdatedOn = DateTime.UtcNow;
                model.CreatedOn = DateTime.UtcNow;
                model.Deleted = false;
                model.UpdatedBy = model.UpdatedBy;
                defcon.Entry(model).State = EntityState.Modified;
                //defcon.Countries.u(model);
                defcon.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Article Updated Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
            
            return RedirectToAction("Creates");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var countryEdit = defcon.Countries.Where(r => r.Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                countryEdit.Deleted = true;
                defcon.Entry(countryEdit).State = EntityState.Modified;
                defcon.SaveChanges();
            }
            return RedirectToAction("Creates");
        }

        [NonAction]
        private Boolean IsAdmin()
        {
            var user = User.Identity;

            var usrRoles = defcon.ViewModels.Where(m => m.UserName == user.Name).ToList();
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