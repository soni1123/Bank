using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("City")]
    public partial class CityModel
    {
        [Key]
        public int Id { get; set; }

        public int StateId { get; set; }

        public int DistrictId { get; set; } 
         
        public string CityCode { get; set; }

        [StringLength(100)]
        public string CityName { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        [ForeignKey("DistrictId")]
        public virtual DistrictModel Districts { get; set; }
    }
}