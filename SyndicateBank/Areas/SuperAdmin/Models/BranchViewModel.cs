using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class BranchViewModel
    {
        [Key]
        public int Id { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        public string BranchCode { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public string IFSCCode { get; set; }

        public string MICRCode { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        public string StateName { get; set; }

        public string DistrictName { get; set; }

        public string CityName { get; set; }

        public IList<Country> AvailableCountry { get; set; }
        public IList<States> AvailableStates { get; set; }
        public IList<Districts> AvailableDistricts { get; set; }
        public IList<Cities> AvailableCity { get; set; }
        public IList<BranchModel> AvailableBranchs { get; set; }


        public BranchViewModel()
        {
            AvailableBranchs = new List<BranchModel>();
            AvailableCountry = new List<Country>();
            AvailableStates = new List<States>();
            AvailableDistricts = new List<Districts>();
            AvailableCity = new List<Cities>();
        }

        public partial class Country
        {

            public int Id { get; set; }

            public string CountryName { get; set; }

        }

        public partial class States
        {

            public int Id { get; set; }

            public string StateName { get; set; }

            public int CountryId { get; set; }
        }

        public partial class Districts
        {

            public int Id { get; set; }

            public string DistrictName { get; set; }

            public int? StateId { get; set; }



        }

        public partial class Cities
        {

            public int Id { get; set; }

            public string CityName { get; set; }

            public int? DistrictId { get; set; }


        }
    }
}