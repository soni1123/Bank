using SyndicateBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class DistrictViewModel
    {
        public DistrictViewModel()
        {
            AvailableDistrict = new List<DistrictModel>();
            Availablestate = new List<StateModel>();
            AvailableCountry = new List<CountryModel>();
        }

        public int Id { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public string DistrictCode { get; set; }

        public string DistrictName { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedOn {  get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public bool Deleted { get; set; }
        
        public IList<StateModel> Availablestate { get; set; }
        public IList<DistrictModel> AvailableDistrict { get; set; }
        public IList<CountryModel> AvailableCountry { get; set; }
    }


}