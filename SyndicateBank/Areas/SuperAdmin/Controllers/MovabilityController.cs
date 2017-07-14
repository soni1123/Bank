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
    public class MovabilityController : Controller
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
            var model = new MovabilityViewModel();
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            dropdownlist(model);
            return View(model);
        }

        private void dropdownlist(MovabilityViewModel model)
        {

            var receipt = def.ReceiptType.Where(r => r.Status && r.Deleted == false).ToList();
            foreach (var item in receipt)
            {
                model.AvailableReceipt.Add(item);
            }

            var article = def.Article.Where(r => r.Status && r.Deleted == false).ToList();
            foreach (var item in article)
            {
                model.AvailableArticle.Add(item);
            }

            var mov = def.Movability.Where(r => r.Deleted == false).ToList();
            foreach (var item in mov)
            {
                model.AvailableMovability.Add(item);
            }
        }

        [HttpGet]
        public ActionResult Create(int Id)
        {
            var article = def.Article.Where(r => r.Status && r.Id == Id).FirstOrDefault();
            var model = new MovabilityViewModel();
            if (article != null)
            {
                model.ArticleId = article.Id;
                model.ArticleCode = article.ArticleCode;
                model.ArticleName = article.ArticleName;
                model.ReceiptId = article.ReceiptId;
                model.ReceiptName = article.Receipt.ReceiptName;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Create(MovabilityViewModel model)
        {
            var mod = new MovabilityModel();
            if (ModelState.IsValid)
            {
                mod.CreatedOn = System.DateTime.Now;
                mod.CreatedBy = model.CreatedBy;
                mod.UpdatedOn = System.DateTime.Now;
                mod.UpdatedBy = model.UpdatedBy;
                mod.ArticleId = model.ArticleId;
                mod.MovCode = model.MovCode;
                mod.Movability = model.Movability;
                mod.Status = model.Status;
                mod.Deleted = false;
                def.Movability.Add(mod);
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Movability Details Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
            
            return RedirectToAction("List");
        }


        public ActionResult Edit(int id)
        {
            var movability = def.Movability.Where(a => a.Id == id).FirstOrDefault();
            
            return Json(movability, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(MovabilityViewModel model)
        {
            var movabal = def.Movability.Where(a => a.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                movabal.ArticleId = model.ArticleId;
                movabal.MovCode = model.MovCode;
                movabal.Movability = model.Movability;
                movabal.Status = model.Status;
                movabal.CreatedBy = model.CreatedBy;
                movabal.UpdatedOn = System.DateTime.Now;
                movabal.UpdatedBy = model.UpdatedBy;
                def.Entry(movabal).State = EntityState.Modified;
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Movability Details Updated Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
            return Json(new { success = true });
        }


        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var model = def.Movability.Where(r => r.Id == Id).FirstOrDefault();
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