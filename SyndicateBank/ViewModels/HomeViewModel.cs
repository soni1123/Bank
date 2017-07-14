using SyndicateBank.Areas.SuperAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            AvailableBranch = new List<BranchModel>();
            BranchList = new List<SelectListItem>();
            CountryList = new List<SelectListItem>();
            StateList = new List<SelectListItem>();
            DistrictList = new List<SelectListItem>();
            CityList = new List<SelectListItem>();
        }

        public string PaymentMode { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public int DistrictId { get; set; }

        public string DistrictName { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public string BranchId { get; set; }

        public string BranchName { get; set; }

        public string IFSCCode { get; set; }

        public int Amount { get; set; }

        public IList<BranchModel> AvailableBranch { get; set; }
        public IList<SelectListItem> CountryList { get; set; }
        public IList<SelectListItem> StateList { get; set; }
        public IList<SelectListItem> DistrictList { get; set; }
        public IList<SelectListItem> CityList { get; set; }
        public IList<SelectListItem> BranchList { get; set; }
    }
}