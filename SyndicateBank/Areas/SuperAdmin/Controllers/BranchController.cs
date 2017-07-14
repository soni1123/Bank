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
    public class BranchController : Controller
    {
        DefaultConnection defcon = new DefaultConnection();
        Notification AM = new Notification();

        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }
        private void List(BranchViewModel model,string query="")
        {
            
            var branchLists = defcon.BranchMasters.Where(r => r.Deleted == false).ToList();
            var branchList = branchLists.Where(r => r.BranchName.Contains(query) || r.BranchCode.Contains(query));

            foreach (var items in branchList)
            {
                model.AvailableBranchs.Add(items);

            }


            var country = defcon.Countries.Where(r => r.Status && r.Deleted == false).ToList();
            foreach (var item in country)
            {
                var conty = new BranchViewModel.Country();
                conty.Id = item.Id;
                conty.CountryName = item.CountryName;
                model.AvailableCountry.Add(conty);
            }
        }

        private void StateList(BranchViewModel model)
        {
            var Liststate = defcon.StateMasters.Where(r => r.Deleted == false).ToList();
            foreach (var items in Liststate)
            {
                var StateList = new BranchViewModel.States();
                StateList.Id = items.Id;
                StateList.StateName = items.StateName;
                StateList.CountryId = items.CountryId;
                model.AvailableStates.Add(StateList);


            }
        }

        private void DistrictList(BranchViewModel model)
        {
            var ListDistrict = defcon.DistrictMastes.Where(r => r.Deleted == false).ToList();
            foreach (var items in ListDistrict)
            {
                var DistrictList = new BranchViewModel.Districts();
                DistrictList.Id = items.Id;
                DistrictList.DistrictName = items.DistrictName;
                DistrictList.StateId = items.StateId;

                model.AvailableDistricts.Add(DistrictList);
            }
        }
        private void CityList(BranchViewModel model)
        {
            var Listcity = defcon.Cities.Where(r => r.Deleted == false).ToList();
            foreach (var items in Listcity)
            {
                var cityList = new BranchViewModel.Cities();
                cityList.Id = items.Id;
                cityList.CityName = items.CityName;
                cityList.DistrictId = items.DistrictId;

                model.AvailableCity.Add(cityList);
            }
        }

        public ActionResult Create(int CityId = 0, int DistrictId = 0, int StateId = 0,string query="")
        {
            var citylists = defcon.Cities.Where(r => r.Deleted == false && r.Id == CityId).FirstOrDefault();
            var Districtlists = defcon.DistrictMastes.Where(r => r.Deleted == false && r.Id == DistrictId).FirstOrDefault();
            var Statelists = defcon.StateMasters.Where(r => r.Deleted == false && r.Id == StateId).FirstOrDefault();
            var viewmodel = new BranchViewModel();
            List(viewmodel, query);
            StateList(viewmodel);
            DistrictList(viewmodel);
            CityList(viewmodel);
            if (citylists != null && Districtlists != null && Statelists != null)
            {
                viewmodel.CityId = citylists.Id;
                viewmodel.CityName = citylists.CityName;
                viewmodel.DistrictId = Districtlists.Id;
                viewmodel.DistrictName = Districtlists.DistrictName;
                viewmodel.StateId = Statelists.Id;
                viewmodel.StateName = Statelists.StateName;
                return Json(viewmodel, JsonRequestBehavior.AllowGet);

            }

            var aleredit = TempData["Edit"];
            var alertmessage = TempData["tempalert"];

            ViewBag.EditResult = aleredit;
            ViewBag.Result = alertmessage;
            return View(viewmodel);


        }

        [HttpPost]
        public ActionResult Create(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedOn = DateTime.UtcNow;
                model.CreatedBy = model.CreatedBy;
                model.UpdatedOn = DateTime.UtcNow;
                model.UpdatedBy = model.UpdatedBy;
                model.Deleted = false;

                defcon.BranchMasters.Add(model);
                defcon.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Branch Details Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
           
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            var model = new BranchViewModel();
            var branchEdit = defcon.BranchMasters.Where(a => a.Id == id);
            model.BranchCode = branchEdit.FirstOrDefault().BranchCode;
            model.BranchName = branchEdit.FirstOrDefault().BranchName;
            model.Address = branchEdit.FirstOrDefault().Address;
            model.Id = branchEdit.FirstOrDefault().Id;
            model.StateId = branchEdit.FirstOrDefault().StateId;
            var stateEdit = defcon.StateMasters.Where(a => a.Id == model.StateId);
            model.StateName = stateEdit.FirstOrDefault().StateName;
            model.DistrictId = branchEdit.FirstOrDefault().DistrictId;
            var DistrictEdit = defcon.DistrictMastes.Where(a => a.Id == model.DistrictId);
            model.DistrictName = DistrictEdit.FirstOrDefault().DistrictName;
            model.CityId = branchEdit.FirstOrDefault().CityId;
            var CityEdit = defcon.Cities.Where(a => a.Id == model.CityId);
            model.CityName = CityEdit.FirstOrDefault().CityName;
            model.Contact = branchEdit.FirstOrDefault().Contact;
            model.IFSCCode = branchEdit.FirstOrDefault().IFSCCode;
            model.MICRCode = branchEdit.FirstOrDefault().MICRCode;
            model.Status = branchEdit.FirstOrDefault().Status;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(BranchModel model)
        {
            if (ModelState.IsValid)
            {

                model.CreatedBy = model.CreatedBy;
                model.UpdatedOn = DateTime.UtcNow;
                model.CreatedOn = DateTime.UtcNow;
                model.UpdatedBy = model.UpdatedBy;
                model.Deleted = false;

                model.UpdatedBy = model.UpdatedBy;
                defcon.Entry(model).State = EntityState.Modified;
                defcon.SaveChanges();
                ViewBag.EditResult = AM.messagesuccess("Branch Details updated Sucessfully...");
                TempData["Edit"] = ViewBag.EditResult;
            }

             
           
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var branchEdit = defcon.BranchMasters.Where(r => r.Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                branchEdit.Deleted = true;
                defcon.Entry(branchEdit).State = EntityState.Modified;
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