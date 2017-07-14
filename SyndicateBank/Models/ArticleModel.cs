using SyndicateBank.Areas.SuperAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateBank.Models
{
    [Table("Article")]
    public class ArticleModel
    {
        [Key]
        public int Id { get; set; }

        public int ReceiptId { get; set; }

        [StringLength(150)]
        public string ArticleCode { get; set; }

        [StringLength(550)]
        public string ArticleName { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        [ForeignKey("ReceiptId")]
        public virtual ReceiptTypeModel Receipt { get; set; }
    }
}