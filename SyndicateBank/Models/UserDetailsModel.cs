using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
        [Table("UserDetails")]
    public class UserDetailsModel
   {
       public int Id { get; set; }

       [Required]
       [StringLength(128)]
       public string AspNetUsersId { get; set; }

       [StringLength(150)]
       public string FullName { get; set; }

       public int? Gender { get; set; }

       public int StateId { get; set; }

       public int DistrictId { get; set; }

       public int BranchId { get; set; }

       [StringLength(15)]
       public string BranchCode { get; set; }

       [StringLength(15)]
       public string IFSC { get; set; }

       public string UserName { get; set; }

       public string Password { get; set; }

     //  public virtual AspNetUser AspNetUser { get; set; }
    }
}