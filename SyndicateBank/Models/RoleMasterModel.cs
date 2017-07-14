using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    [Table("RoleMaster")]
    public partial class RoleMasterModel
    {
        //public RoleMaster()
        //{
        //    User_RoleMapping = new HashSet<UserRoleMapping>();
        //}

        public int Id { get; set; }

        [StringLength(100)]
        public string RoleName { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }

        //public virtual ICollection<UserRoleMapping> User_RoleMapping { get; set; }
    }
}