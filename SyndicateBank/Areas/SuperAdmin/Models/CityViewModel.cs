using SyndicateBank.Areas.SuperAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class CityViewModel
    {
        public CityViewModel()
        {
            AvailableCity = new List<CityModel>();
            AvailableCountry = new List<CountryModel>();
            AvailableState = new List<StateModel>();
            AvailableDistrict = new List<DistrictModel>();
        }

        [Key]
        public int Id { get; set; }

        public int StateId { get; set; }

        public string StateName { get; set; }

        public int DistrictId { get; set; }

        public string DistrictName { get; set; }

        public string CityCode { get; set; }

        [StringLength(100)]
        public string CityName { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        public IList<CityModel> AvailableCity { get; set; }

        public IList<CountryModel> AvailableCountry { get; set; }

        public IList<StateModel> AvailableState { get; set; }

        public IList<DistrictModel> AvailableDistrict { get; set; }
    }
}