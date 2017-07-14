using SyndicateBank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            AvailableArticle = new List<ArticleModel>();
            AvailableReceipts = new List<ReceiptTypeModel>();
        }

        [Key]
        public int Id { get; set; }

        public int ReceiptId { get; set; }

        public string ArticleCode { get; set; }

        [Display(Name = "Article Name")]
        public string ArticleName { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public string ReceiptName { get; set; }

        public virtual ReceiptTypeModel Receipt { get; set; }

        public IList<ReceiptTypeModel> AvailableReceipts { get; set; }

        public IList<ArticleModel> AvailableArticle { get; set; }

    }
}