using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SyndicateBank.Models;
using SyndicateBank.Areas.SuperAdmin.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace SyndicateBank.Areas.SuperAdmin.Controllers
{
    [Authorize]
    public class StateController : Controller
    {
        DefaultConnection defcon = new DefaultConnection();
        

        public ActionResult Index()
        {
              
            return RedirectToAction("Create");            

        }



        private void StateList(Stateviewmodel model)
        {
            var statelists = defcon.StateMasters.Where(r => r.Deleted == false).ToList();

            foreach (var item in statelists)
            {
                item.CountryId = statelists.FirstOrDefault().CountryId;
                model.AvailableStates.Add(item);

            }
        }

        private void CountrtyList(Stateviewmodel model)
        {
            var Listcity = defcon.Countries.Where(r => r.Deleted == false).ToList();
            foreach (var items in Listcity)
            {
                var countryList = new Stateviewmodel.country();
                countryList.Id = items.Id;
                countryList.CountryName = items.CountryName;
                countryList.CountryId = items.Id;
                model.AvailableCountry.Add(countryList);
            }
        }


        public ActionResult Create(int CountryId=0)
        {
           

            var model = new Stateviewmodel();
            CountrtyList(model);
            StateList(model);

            var Countrtylists = defcon.Countries.Where(r => r.Deleted == false && r.Id == CountryId).FirstOrDefault();
            if (Countrtylists!=null)
            {
                model.CountryId = Countrtylists.Id;
                model.CountryName = Countrtylists.CountryName;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StateModel model)
        {

            
            if (ModelState.IsValid)
            {
                model.CreatedOn = DateTime.UtcNow;
                model.CreatedBy = model.CreatedBy;
                model.UpdatedOn = DateTime.UtcNow;
                model.UpdatedBy = model.UpdatedBy;
                model.Deleted = false;

                defcon.StateMasters.Add(model);
                defcon.SaveChanges();
            }
            return RedirectToAction("Create");

        }

        public ActionResult Edit(int id)
        {
          
            var model = new Stateviewmodel();
            var stateEdit = defcon.StateMasters.Where(a => a.Id == id);
            model.CountryId = stateEdit.FirstOrDefault().CountryId;
            var countryEdit = defcon.Countries.Where(a => a.Id == model.CountryId);
            model.StateName = stateEdit.FirstOrDefault().StateName;
            model.CountryName = countryEdit.FirstOrDefault().CountryName;
            model.Id = stateEdit.FirstOrDefault().Id;
            model.Status = stateEdit.FirstOrDefault().Status;
           
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Edit(StateModel model)
        {
            if (ModelState.IsValid)
            {
            model.CreatedOn = DateTime.UtcNow;
            model.CreatedBy = model.CreatedBy;
            model.UpdatedOn = DateTime.UtcNow;
            model.UpdatedBy = model.UpdatedBy;
            model.Deleted = false;

            model.UpdatedBy = model.UpdatedBy;
            defcon.Entry(model).State = EntityState.Modified;
            defcon.SaveChanges();
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
          

            var stateEdit = defcon.StateMasters.Where(r => r.Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                stateEdit.Deleted = true;
                defcon.Entry(stateEdit).State = EntityState.Modified;
                defcon.SaveChanges();
            }
            return RedirectToAction("Create");
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