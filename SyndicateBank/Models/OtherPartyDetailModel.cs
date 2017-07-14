
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

namespace SyndicateBank.Models
{
     [Table("OtherPartyDetails")]
    public partial class OtherPartyDetailModel
    {
         [Key]
        public int Id { get; set; }

        //[Required]
        //[StringLength(50)]
        public string TransactionId { get; set; }

        //[Required]
        //[StringLength(64)]
        public string OtherPartyFname { get; set; }

        //[StringLength(16)]
        public string OtherPartyMname { get; set; }

        //[StringLength(16)]
        public string OtherPartyLname { get; set; }

        //[Required]
        //[StringLength(25)]
        public string ESBTROpId { get; set; }

        public bool Deleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string PayerModeName { get; set; }

    }
}
