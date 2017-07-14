using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public partial class OfficeViewModel
    {
        public OfficeViewModel()
        {
            AvailableDistrict = new List<DistrictModel>();
            AvailableOffice = new List<OfficeModel>();
        }

        public int Id { get; set; }

        public int DistrictId { get; set; }

        public string DistrictCode { get; set; }

        public string DistrictName { get; set; }

        public string OfficeCode { get; set; }

        public string OfficeName { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public IList<DistrictModel> AvailableDistrict { get; set; }

        public IList<OfficeModel> AvailableOffice { get; set; }
    }
}