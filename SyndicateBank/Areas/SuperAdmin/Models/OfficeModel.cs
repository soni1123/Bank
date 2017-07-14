using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("OfficeMaster")]
    public partial class OfficeModel
    {
        [Key]
        public int Id { get; set; }

        public int DistrictId { get; set; }

        [Required]
        [StringLength(6)]
        public string OfficeCode { get; set; }

        public string OfficeName { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        [ForeignKey("DistrictId")]
        public virtual DistrictModel Districts { get; set; }
    }
}