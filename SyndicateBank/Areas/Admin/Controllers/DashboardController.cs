using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SyndicateBank.Areas.Admin.Models;
using SyndicateBank.Models;
using SyndicateBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        int i = 0;
        decimal j = 0;
        decimal k = 0;
        public ActionResult Index()
        {
            if (IsSuperAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var model = new DashboardViewModel();
           
            var item=def.branchscrollstationery.ToList();
            var itemount = def.branchscrollstationery.ToList();
            var esbtrcount = def.branchscrolltransaction.ToList();

            foreach (var totalitem in item )
            {
                i = totalitem.StationeryDamaged + i;
            }

            foreach(var totalesbtr in itemount)
            {
                j = totalesbtr.TotalAmount + j;
            }

            foreach(var esbtr in esbtrcount)
            {
                k = esbtr.EsbtrAmount + k;
            }

            var staprint = def.branchscrollstationery.FirstOrDefault().StationeryPrinted;
            var staused = def.branchscrollstationery.FirstOrDefault().TotalStationeryUsed;
            //var statdamged = def.branchscrollstationery.FirstOrDefault().StationeryDamaged;
            //var totalamount = def.branchscrollstationery.FirstOrDefault().TotalAmount;
           
            var totalcount = staprint - staused;
            model.TotalUsed = staused.ToString();
            model.TotalAvailability = totalcount.ToString();
            model.TotalDamaged = i.ToString();
            model.TotalAmount = j.ToString();
            model.TotalesbtrAmount = k.ToString();
            return View(model);
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

        public ActionResult GetDataDaily()
        {
            DateTime dt = System.DateTime.Today;
            DateTime dt1 = dt.Date;
            var getdailydata = def.branchscrolltransaction.Where(r => r.CreatedOn == dt).GroupBy(r=>r.MainScrollNumber).ToList();
            List<DailyChatAdmin> DailyLists = new List<DailyChatAdmin>();            
            for(int item =0;item<getdailydata.Count();item++)
            {
                var DailyList = new DailyChatAdmin
                {
                    Data = getdailydata[item].FirstOrDefault().MainScrollNumber
                };
                DailyLists.Add(DailyList);

            }

            List<AdminChartViewModel> Getdatas = new List<AdminChartViewModel>();
            for (int j = 0; j < DailyLists.Count(); j++)
            {
                var MSN=DailyLists[j].Data;
                var getdatastationary = def.branchscrollstationery.Where(r => r.MainScrollNumber == MSN);
                var project = new AdminChartViewModel
                {
                    Key = getdatastationary.FirstOrDefault().StationeryPrinted,
                    Key1=getdatastationary.FirstOrDefault().StationeryDamaged,
                    Key2=getdatastationary.FirstOrDefault().TotalStationeryUsed,
                    Key3=Convert.ToInt32( getdatastationary.FirstOrDefault().TotalAmount),
                    Value = Convert.ToString(dt)
                    
                };
                Getdatas.Add(project);
            }

            return Json(Getdatas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataWeekly()
        {
            DateTime dt = System.DateTime.Now;
            DateTime dt1 = System.DateTime.Now.AddDays(-7);
            var getdailydata = def.branchscrolltransaction.Where(r => r.CreatedOn <= dt && r.CreatedOn>=dt1 ).GroupBy(r => r.MainScrollNumber).ToList();
            List<DailyChatAdmin> DailyLists = new List<DailyChatAdmin>();
            for (int item = 0; item < getdailydata.Count(); item++)
            {
                var DailyList = new DailyChatAdmin
                {
                    Data = getdailydata[item].FirstOrDefault().MainScrollNumber
                };
                DailyLists.Add(DailyList);

            }

            List<AdminChartViewModel> Getdatas = new List<AdminChartViewModel>();
            for (int j = 0; j < DailyLists.Count(); j++)
            {
                var MSN = DailyLists[j].Data;
                var getdatastationary = def.branchscrollstationery.Where(r => r.MainScrollNumber == MSN);
                var project = new AdminChartViewModel
                {
                    Key = getdatastationary.FirstOrDefault().StationeryPrinted,
                    Key1 = getdatastationary.FirstOrDefault().StationeryDamaged,
                    Key2 = getdatastationary.FirstOrDefault().TotalStationeryUsed,
                    Key3 = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount),
                    Value = dt.ToString()

                };
                Getdatas.Add(project);
            }

            return Json(Getdatas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataMonthly()
        {
            DateTime dt = System.DateTime.Today;
            DateTime dt1 =System.DateTime.Now.AddDays(-7);

            var getdailydata = def.branchscrolltransaction.Where(r => r.CreatedOn == dt).GroupBy(r => r.MainScrollNumber).ToList();
            List<DailyChatAdmin> DailyLists = new List<DailyChatAdmin>();
            for (int item = 0; item < getdailydata.Count(); item++)
            {
                var DailyList = new DailyChatAdmin
                {
                    Data = getdailydata[item].FirstOrDefault().MainScrollNumber
                };
                DailyLists.Add(DailyList);

            }

            List<AdminChartViewModel> Getdatas = new List<AdminChartViewModel>();
            for (int j = 0; j < DailyLists.Count(); j++)
            {
                var MSN = DailyLists[j].Data;
                var getdatastationary = def.branchscrollstationery.Where(r => r.MainScrollNumber == MSN);
                var project = new AdminChartViewModel
                {
                    Key = getdatastationary.FirstOrDefault().StationeryPrinted,
                    Key1 = getdatastationary.FirstOrDefault().StationeryDamaged,
                    Key2 = getdatastationary.FirstOrDefault().TotalStationeryUsed,
                    Key3 = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount),
                    Value = dt.ToString()

                };
                Getdatas.Add(project);
            }

            return Json(Getdatas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Checking()
        {
            return View();
        }
    }
}