using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("vw_Rolemapping")]

    public class ViewModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        //public int RoleId { get; set; }
        public string RoleName { get; set; }
       
        public string UserName { get; set; }
        public bool? Status { get; set; }

    }
}