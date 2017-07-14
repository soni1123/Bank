using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("vw_mappingbranchwisedetails")]
    public class LocationwiseBranchMapModel
    {
        public LocationwiseBranchMapModel()
        {

        }

        public int Id { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public string CityCode { get; set; }

        public string CityName { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string CountryCurrency { get; set; }

        public string DistrictName { get; set; }

        public string StateName { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }
    }
}