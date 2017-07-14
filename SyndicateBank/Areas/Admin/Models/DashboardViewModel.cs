using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int Id { get; set; }
        public string TotalAvailability { get; set; }
        public string TotalUsed { get; set; }
        public string TotalDamaged { get; set; }
        public string TotalAmount { get; set; }
        public string TotalesbtrAmount { get; set; }

    }
}