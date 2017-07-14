
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


namespace SyndicateBank.ViewModels
{
    public partial class PropertyDetailViewModel
    {

        public int Id { get; set; }

        public string TRANSACTION_ID { get; set; }

        public string Article_Code { get; set; }

        public string Prop_Movability { get; set; }

        public string Prop_Addr_Line1 { get; set; }

        
        public string Prop_Addr_Line2 { get; set; }

        
        public string Prop_Addr_Line3 { get; set; }

        
        public string Prop_Addr_Line4 { get; set; }

        
        public string Road { get; set; }

        
        public string Town_Village { get; set; }

        
        public string District { get; set; }

        
        public string State { get; set; }

        public decimal Prop_Pincode { get; set; }

        public double Consideration_Amount { get; set; }

        public decimal Prop_Area_Units { get; set; }

        public string ESBTR_PA_UOM { get; set; }

        public bool Deleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string BranchCode { get; set; }
    }
}
