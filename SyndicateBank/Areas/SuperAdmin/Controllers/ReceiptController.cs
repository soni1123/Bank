using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SyndicateBank.AlertMessages;
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
    public class ReceiptController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();

        public ActionResult Index()
        {

            return RedirectToAction("Create");
        }


        private void dropdownList(ReceiptTypeModel model)
        {
            var rec = def.ReceiptType.Where(r => r.Deleted == false).ToList();
            foreach (var item in rec)
            {
                var receipt = new ReceiptTypeModel.Receipts();
                receipt.Id = item.Id;
                receipt.Status = item.Status;
                receipt.ReceiptName = item.ReceiptName;
                model.AvailableReceipt.Add(receipt);
            }
        }



        public ActionResult Create()
        {
            var model = new ReceiptTypeModel();
            dropdownList(model);
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ReceiptTypeModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedOn = System.DateTime.Now;
                model.CreatedBy = 1;
                model.UpdatedOn = System.DateTime.Now;
                model.UpdatedBy = 1;
                model.Status = true;
                model.Deleted = false;
                def.ReceiptType.Add(model);
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Receipt Details Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
       
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            var model = new ReceiptTypeModel();
            var receipt = def.ReceiptType.Where(a => a.Id == id).FirstOrDefault();
            return Json(receipt, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(ReceiptTypeModel model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatedOn = DateTime.UtcNow;
                model.UpdatedBy = 1;
                def.Entry(model).State = EntityState.Modified;
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Receipt Details Updated Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
           
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var model = def.ReceiptType.Where(r => r.Id == Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                model.Deleted = true;
                def.Entry(model).State = EntityState.Modified;
                def.SaveChanges();
            }
            return RedirectToAction("Create");
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