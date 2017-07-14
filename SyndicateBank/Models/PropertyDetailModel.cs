
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


namespace SyndicateBank.Models
{
    [Table("PropertyDetail")]
    public partial class PropertyDetailModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Required]
        [StringLength(20)]
        public string TRANSACTION_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Article_Code { get; set; }

        [Required]
        [StringLength(1)]
        public string Prop_Movability { get; set; }

        [Required]
        [StringLength(100)]
        public string Prop_Addr_Line1 { get; set; }

        [StringLength(100)]
        public string Prop_Addr_Line2 { get; set; }

        [StringLength(100)]
        public string Prop_Addr_Line3 { get; set; }

        [StringLength(100)]
        public string Prop_Addr_Line4 { get; set; }

        [StringLength(100)]
        public string Road { get; set; }

        [StringLength(100)]
        public string Town_Village { get; set; }

        [StringLength(100)]
        public string District { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Prop_Pincode { get; set; }

        public double Consideration_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Prop_Area_Units { get; set; }

        [Required]
        [StringLength(10)]
        public string ESBTR_PA_UOM { get; set; }

        public bool Deleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        [StringLength(50)]
        public string BranchCode { get; set; }
    }
}
