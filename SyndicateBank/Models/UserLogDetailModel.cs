using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("UserLogDetail")]
    public partial class UserLogDetailModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime? LoginDate { get; set; }

        public DateTime? LogoutDate { get; set; }

        public TimeSpan? LoginTime { get; set; }

        public TimeSpan? LogoutTime { get; set; }

        [StringLength(250)]
        public string MachineName { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        [StringLength(550)]
        public string BrowserName { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        public virtual UserLoginModel UserLogin { get; set; }
    }
}