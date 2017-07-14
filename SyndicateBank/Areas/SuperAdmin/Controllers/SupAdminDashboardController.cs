using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SyndicateBank.Areas.SuperAdmin.Models;
using SyndicateBank.Models;
using SyndicateBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.Areas.SuperAdmin.Controllers
{
    [Authorize]
    public class SupAdminDashboardController : Controller
    {
        DefaultConnection def = new DefaultConnection();

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var getuser = User.Identity.Name;

            if (IsAdmin())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            var getuserdetails=def.AspNetUsers.Where(r=>r.UserName==getuser);
            var checkrole=def.UserRoleMapping.Where(p=>p.UserId==getuserdetails.FirstOrDefault().UId);
            if (checkrole.FirstOrDefault().RoleId!=2)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            return View();
        }
        [HttpGet]
        public ActionResult _SuperAdminStockTranDetails()
        {
            var mod = def.SupMainTransactionStationerys.ToList().Where(r => r.Deleted == false);
            return View(mod);
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

        public ActionResult ViewDetails(int id)
        {
            var getscrollnum = def.SupMainTransactionStationerys.Where(r=>r.Id==id);
            var getbranchwisedetails = def.branchscrolltransaction.Where(p => p.MainScrollNumber == getscrollnum.FirstOrDefault().MainScrollNumber);
            var getdetails = getbranchwisedetails.ToList().Where(q => q.Deleted == false);
            ViewBag.passId = id;
            return View(getdetails);
        }

        public ActionResult GetData(int id)
        {
            var getbranch = def.SupMainTransactionStationerys.Where(r => r.Id == id);
            var getdata = def.branchscrolltransaction.Where(r => r.MainScrollNumber == getbranch.FirstOrDefault().MainScrollNumber).ToList();
            List<SuperAdminChartViewModel> projects = new List<SuperAdminChartViewModel>();
            for (int i = 0; i < getdata.Count(); i++)
            {
                var project = new SuperAdminChartViewModel
                {
                    Key = getdata[i].Id,
                    Value = Convert.ToInt32(getdata[i].StationeryNumber)
                };
                projects.Add(project);
            }

            return Json(projects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDataToday()
        {
            DateTime dt = System.DateTime.Today;

            var getbranchdetails = def.BranchMasters.ToList();
             List<SuperAdminData> GetDatas = new List<SuperAdminData>();

            for(int m=0;m<getbranchdetails.Count();m++)
            {
                string ids=getbranchdetails[m].Id.ToString();
                var gettrancdetails = def.SupMainTransactionStationerys.Where(r => r.BranchCode == ids).ToList();
                if (gettrancdetails.Count() != 0)
                {
                    var getdatafromsupertransc = new SuperAdminData
                    {
                        Data = gettrancdetails.FirstOrDefault().MainScrollNumber,
                        BranchName = getbranchdetails[m].BranchName

                    };
                    GetDatas.Add(getdatafromsupertransc);
                }

                else
                {
                    var getdatafromsupertransc = new SuperAdminData
                    {
                        Data = "",
                        BranchName = getbranchdetails[m].BranchName

                    };
                    GetDatas.Add(getdatafromsupertransc);
                }

            }
            
            List<SuperAdminChartsViewModel> projects = new List<SuperAdminChartsViewModel>();
            for (int i = 0; i < GetDatas.Count(); i++)
            {
                string checkdata = GetDatas[i].Data;
                int sold = 0;
                int damg = 0;
                int remaining = 0;
                var getdataofallbranch = def.branchscrolltransaction.Where(p => p.MainScrollNumber == checkdata && p.CreatedOn == dt).ToList();
                for (int q = 0; q < getdataofallbranch.Count();q++ )
                {
                    if (getdataofallbranch[q].PrintStatus == "S")
                    {
                        sold = sold + 1;
                    }
                    else
                    {
                        damg = damg + 1;
                    }
                }
               
                if (getdataofallbranch.Count() == 0)
                {
                    var project = new SuperAdminChartsViewModel
                    {
                        Key1 = 0,
                        Key2=0,
                        Key3=0,
                        Key4=0,
                        Value = GetDatas[i].BranchName
                    };
                    projects.Add(project);
                }
            
            else
            {
                string MSN = getdataofallbranch.FirstOrDefault().MainScrollNumber;
                var getdatastationary = def.branchscrollstationery.Where(r => r.MainScrollNumber == MSN);
                remaining = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount);
                var project = new SuperAdminChartsViewModel
                {
                    Key1 = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount),
                    Key2=sold,
                    Key3=damg,
                    Key4 = remaining - sold + damg,
                    Value = GetDatas[i].BranchName
                };
                projects.Add(project);
            }
        }

            return Json(projects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDataWeek()
        {
            DateTime dt = System.DateTime.Now;

            DateTime dt1 = System.DateTime.Now.AddDays(-7);
            
            var getbranchdetails = def.BranchMasters.ToList();
             List<SuperAdminData> GetDatas = new List<SuperAdminData>();

            for(int m=0;m<getbranchdetails.Count();m++)
            {
                string ids=getbranchdetails[m].Id.ToString();
                var gettrancdetails = def.SupMainTransactionStationerys.Where(r => r.BranchCode == ids).ToList();
                if (gettrancdetails.Count() != 0)
                {
                    var getdatafromsupertransc = new SuperAdminData
                    {
                        Data = gettrancdetails.FirstOrDefault().MainScrollNumber,
                        BranchName = getbranchdetails[m].BranchName

                    };
                    GetDatas.Add(getdatafromsupertransc);
                }

                else
                {
                    var getdatafromsupertransc = new SuperAdminData
                    {
                        Data = "",
                        BranchName = getbranchdetails[m].BranchName

                    };
                    GetDatas.Add(getdatafromsupertransc);
                }

            }
            
            List<SuperAdminChartsViewModel> projects = new List<SuperAdminChartsViewModel>();
            for (int i = 0; i < GetDatas.Count(); i++)
            {
                int sold = 0;
                int damg = 0;
                int remaining = 0;
                string checkdata = GetDatas[i].Data;
                var getdataofallbranch = def.branchscrolltransaction.Where(p => p.MainScrollNumber == checkdata && p.CreatedOn <= dt && p.CreatedOn>=dt1).ToList();
                for (int q = 0; q < getdataofallbranch.Count(); q++)
                {
                    if (getdataofallbranch[q].PrintStatus == "S")
                    {
                        sold = sold + 1;
                    }
                    else
                    {
                        damg = damg + 1;
                    }
                }

                if (getdataofallbranch.Count() == 0)
                {
                    var project = new SuperAdminChartsViewModel
                    {
                        Key1 = 0,
                        Key2=0,
                        Key3 = 0,
                        Key4 = 0,
                        Value = GetDatas[i].BranchName
                    };
                    projects.Add(project);
                }
            
            else
            {
                string MSN = getdataofallbranch.FirstOrDefault().MainScrollNumber;
                var getdatastationary = def.branchscrollstationery.Where(r => r.MainScrollNumber == MSN);
                remaining = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount);
                if(getdatastationary.Count()!=0)
                {
                var project = new SuperAdminChartsViewModel
                {
                    Key1 = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount),
                    Key2 = sold,
                    Key3 = damg,
                    Key4 = remaining - sold + damg,
                    Value = GetDatas[i].BranchName
                };
                projects.Add(project);
            }
                   
            }
        }

            return Json(projects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDataMonth()
        {
            DateTime dt = System.DateTime.Now;

            DateTime dt1 = System.DateTime.Now.AddDays(-7);

            var getbranchdetails = def.BranchMasters.ToList();
            List<SuperAdminData> GetDatas = new List<SuperAdminData>();

            for (int m = 0; m < getbranchdetails.Count(); m++)
            {
                string ids = getbranchdetails[m].Id.ToString();
                var gettrancdetails = def.SupMainTransactionStationerys.Where(r => r.BranchCode == ids).ToList();
                if (gettrancdetails.Count() != 0)
                {
                    var getdatafromsupertransc = new SuperAdminData
                    {
                        Data = gettrancdetails.FirstOrDefault().MainScrollNumber,
                        BranchName = getbranchdetails[m].BranchName

                    };
                    GetDatas.Add(getdatafromsupertransc);
                }

                else
                {
                    var getdatafromsupertransc = new SuperAdminData
                    {
                        Data = "",
                        BranchName = getbranchdetails[m].BranchName

                    };
                    GetDatas.Add(getdatafromsupertransc);
                }

            }

            List<SuperAdminChartsViewModel> projects = new List<SuperAdminChartsViewModel>();
            for (int i = 0; i < GetDatas.Count(); i++)
            {
                int sold = 0;
                int damg = 0;
                int remaining = 0;
                string checkdata = GetDatas[i].Data;
              //  var getdataofallbranch = def.branchscrolltransaction.Where(p => p.MainScrollNumber == checkdata && p.CreatedOn <= dt && p.CreatedOn >= dt1).GroupBy(r => r.MainScrollNumber).ToList();
               int dt3= System.DateTime.Now.Month;
                var getmonthrecord = def.branchscrolltransaction.Where(p => p.MainScrollNumber == checkdata && p.CreatedOn.Month == System.DateTime.Now.Month).ToList();
                for (int q = 0; q < getmonthrecord.Count(); q++)
                {
                    if (getmonthrecord[q].PrintStatus == "S")
                    {
                        sold = sold + 1;
                    }
                    else
                    {
                        damg = damg + 1;
                    }
                }
                if (getmonthrecord.Count() == 0)
                {
                    var project = new SuperAdminChartsViewModel
                    {
                        Key1 = 0,
                        Key2 = 0,
                        Key3=0,
                        Key4=0,
                        Value = GetDatas[i].BranchName
                    };
                    projects.Add(project);
                }

                else
                {
                    string MSN = getmonthrecord.FirstOrDefault().MainScrollNumber;
                    var getdatastationary = def.branchscrollstationery.Where(r => r.MainScrollNumber == MSN);
                    remaining = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount);
                    if (getdatastationary.Count() != 0)
                    {
                        var project = new SuperAdminChartsViewModel
                        {
                            Key1 = Convert.ToInt32(getdatastationary.FirstOrDefault().TotalAmount),
                            Key2 = sold,
                            Key3 = damg,
                            Key4 = remaining-sold + damg,
                            Value = GetDatas[i].BranchName
                        };
                        projects.Add(project);
                    }

                }
            }

            return Json(projects, JsonRequestBehavior.AllowGet);
        }

    }
}