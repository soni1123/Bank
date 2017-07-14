using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("State")]
    public partial class StateModel
    {
        public StateModel()
        {
        }

        public int Id { get; set; }

        public int CountryId { get; set; }

        [StringLength(100)]
        public string StateName { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        
        

        //public virtual ICollection<BranchMaster> BranchMasters { get; set; }

        //public virtual ICollection<DistrictMaster> DistrictMastes { get; set; }
    }
}