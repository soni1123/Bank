using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("Branch")]
    public partial class BranchModel
    {
        public int Id { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        [StringLength(100)]
        public string BranchCode { get; set; }

        [StringLength(100)]
        public string BranchName { get; set; }

        public string Address { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [Required]
        [StringLength(150)]
        public string IFSCCode { get; set; }

        [StringLength(150)]
        public string MICRCode { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        //public IList<Branchs> AvailableBranchs { get; set; }
        //public IList<States> AvailableStates { get; set; }
        //public IList<Districts> AvailableDistricts { get; set; }
        //public IList<Cities> AvailableCity { get; set; }




        public BranchModel()
        {
            //AvailableBranchs = new List<Branchs>();
            //AvailableStates = new List<States>();
            //AvailableDistricts = new List<Districts>();
            //AvailableCity = new List<Cities>();



        }

        //public partial class Branchs
        //{

        //    public int Id { get; set; }

        //    public int? StateId { get; set; }

        //    public int? DistrictId { get; set; }

        //    public int? CityId { get; set; }

        //    public string BranchCode { get; set; }

        //    public string BranchName { get; set; }          

        //    public string Address { get; set; }

        //    public string Contact { get; set; }

        //    public string IFSCCode { get; set; }

        //    public string MICRCode { get; set; }

        //    public bool? Status { get; set; }


        //}

        
    }
}