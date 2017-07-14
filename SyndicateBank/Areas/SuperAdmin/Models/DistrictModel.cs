using SyndicateBank.Areas.SuperAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("District")]
    public partial class DistrictModel
    {
        public int Id { get; set; }

        public int StateId { get; set; }

        public string DistrictCode { get; set; }

        public string DistrictName { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        [ForeignKey("StateId")]
        public virtual StateModel state { get; set; }
    }
}