using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("Movability")]
    public class MovabilityModel
    {
        [Key]
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string MovCode { get; set; }

        [StringLength(550)]
        public string Movability { get; set; }

        [Display(Name ="Consideration Amount")]
        public string ConField { get; set; }

        [Display(Name = "Property Area")]
        public string propArea { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        [ForeignKey("ArticleId")]
        public virtual ArticleModel Articlesss { get; set; }
    }
}