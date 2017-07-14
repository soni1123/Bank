using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("UserLogin")]
    public partial class UserLoginModel
    {
        public UserLoginModel()
        {
            UserLogDetails = new HashSet<UserLogDetailModel>();
        }

        public int Id { get; set; }

        [StringLength(150)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? Status { get; set; }

        public virtual ICollection<UserLogDetailModel> UserLogDetails { get; set; }
    }
}