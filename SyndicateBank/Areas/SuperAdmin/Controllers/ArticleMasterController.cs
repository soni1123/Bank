using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SyndicateBank.Models;
using System.Data.Entity;
using SyndicateBank.Areas.SuperAdmin.Models;
using SyndicateBank.AlertMessages;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace SyndicateBank.Areas.SuperAdmin.Controllers
{
    [Authorize]
    public class ArticleMasterController : Controller
    {
        DefaultConnection def = new DefaultConnection();
        Notification AM = new Notification();

        public ActionResult Index()
        {
           
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
         
            var model = new ArticleViewModel();
            var alertmessage = TempData["tempalert"];
            ViewBag.Result = alertmessage;
            DropDownList(model);
            return View(model);
        }


        private void DropDownList(ArticleViewModel model)
        {
            var recept = def.ReceiptType.Where(r => r.Deleted == false && r.Status == true).ToList();
            foreach (var item in recept)
            {
                model.AvailableReceipts.Add(item);
            }

            var artcle = def.Article.Where(r => r.Deleted == false).ToList();
            foreach (var item in artcle)
            {
                model.AvailableArticle.Add(item);
            }
        }

        [HttpGet]
        public ActionResult Create(int Id)
        {

            var receipt = def.ReceiptType.Where(r => r.Status && r.Id == Id).FirstOrDefault();
            var model = new ArticleViewModel();
            DropDownList(model);
            if (receipt != null)
            {
                model.ReceiptName = receipt.ReceiptName;
                model.ReceiptId = receipt.Id;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(ArticleViewModel model)
        {
            var mod = new ArticleModel();
            if (ModelState.IsValid)
            {
                mod.CreatedOn = System.DateTime.Now;
                mod.CreatedBy = model.CreatedBy;
                mod.UpdatedOn = System.DateTime.Now;
                mod.UpdatedBy = model.UpdatedBy;
                mod.ArticleCode = model.ArticleCode;
                mod.ArticleName = model.ArticleName;
                mod.ReceiptId = model.ReceiptId;
                mod.Status = model.Status;
                mod.Deleted = false;
                def.Article.Add(mod);
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Article Added Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
            
            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {

            var article = def.Article.Where(a => a.Id == id).FirstOrDefault();
            
            return Json(article, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModel model)
        {
            var article = def.Article.Where(a => a.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                article.ReceiptId = model.ReceiptId;
                article.ArticleName = model.ArticleName;
                article.ArticleCode = model.ArticleCode;
                article.CreatedBy = model.CreatedBy;
                article.UpdatedOn = System.DateTime.Now;
                article.UpdatedBy = model.UpdatedBy;
                article.Status = model.Status;
                def.Entry(article).State = EntityState.Modified;
                def.SaveChanges();
                ViewBag.Result = AM.messagesuccess("Article Updated Sucessfully...");
                TempData["tempalert"] = ViewBag.Result;
            }
           
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
          

            var model = def.Article.Where(r => r.Id == Id).FirstOrDefault();
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