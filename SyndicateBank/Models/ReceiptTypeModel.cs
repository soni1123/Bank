using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("ReceiptType")]
    public class ReceiptTypeModel
    {
        public ReceiptTypeModel()
        {
            AvailableReceipt = new List<Receipts>();
        }
      
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string ReceiptName { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public IList<Receipts> AvailableReceipt { get; set; }


        public partial class Receipts
        {
            public int Id { get; set; }

            public string ReceiptName { get; set; }

            public bool Status { get; set; }
        }


        //public virtual List<ReceiptTypeModel> AvailableReceipts { get; set; }
    }
}