using SyndicateBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class MovabilityViewModel
    {
        public MovabilityViewModel()
        {
            AvailableArticle = new List<ArticleModel>();
            AvailableReceipt = new List<ReceiptTypeModel>();
            AvailableMovability = new List<MovabilityModel>();
        }


        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string MovCode { get; set; }

        public string Movability { get; set; }

        public string ConField { get; set; }

        public string propArea { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public string ArticleCode { get; set; }

        public string ArticleName { get; set; }

        public int ReceiptId { get; set; }

        public string ReceiptName { get; set; }

        public IList<ArticleModel> AvailableArticle { get; set; }

        public IList<ReceiptTypeModel> AvailableReceipt { get; set; }

        public IList<MovabilityModel> AvailableMovability { get; set; }
    }
}