using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    [Table("Country")]
    public partial class CountryModel
    {

        [Key]
        public int Id { get; set; }

        // [Required]
        //  [StringLength(50)]
        public string CountryCode { get; set; }

        // [Required]
        //  [StringLength(150)]
        public string CountryName { get; set; }

        //   [StringLength(50)]
       
        public string CountryCurrency { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public IList<Countries> AvailableCountriess { get; set; }
        public CountryModel()
        {
            AvailableCountriess = new List<Countries>();
        }

        public partial class Countries
        {

            public int Id { get; set; }

            public string CountryCode { get; set; }

            public string CountryName { get; set; }

            [Display(Name = "Currency")]
            public string CountryCurrency { get; set; }

            public bool Status { get; set; }


        }
    }
}