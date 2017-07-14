using SyndicateBank.Models;
using SyndicateBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.Controllers
{
    public class HomeController : Controller
    {
        DefaultConnection def = new DefaultConnection();

        public ActionResult Index()
        {
            var model = new HomeViewModel();
            DropdownList(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            TempData["paymentMode"] = model.PaymentMode;

            if (model.BranchId != null)
            {
                TempData["BranchId"] = model.BranchId;
            }

            if (model.Amount > 0)
            {
                TempData["esbtrAmount"] = model.Amount;
            }

            return RedirectToAction("Index", "EsbtrPayment");
        }

        private void DropdownList(HomeViewModel model)
        {
            var country = def.Countries.Where(r => r.Status && r.Deleted == false).ToList();
            model.CountryList.Add(new SelectListItem { Text = "---SELECT---", Value = "0" });
            foreach (var item in country)
            {
                model.CountryList.Add(new SelectListItem { Text = item.CountryName, Value = item.Id.ToString() });
            }
        }

        public ActionResult List()
        {
            return RedirectToAction("List");
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}