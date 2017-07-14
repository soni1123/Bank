using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Areas.SuperAdmin.Models
{
    public class Stateviewmodel
    {
        public Stateviewmodel()
        {
            AvailableCountry = new List<country>();
            AvailableStates = new List<StateModel>();


        }
        public int Id { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }


        public string StateName { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? Deleted { get; set; }
        public IList<country> AvailableCountry { get; set; }
        public IList<StateModel> AvailableStates { get; set; }


        public partial class country
        {

            public int Id { get; set; }

            public string CountryName { get; set; }

            public int CountryId { get; set; }



        }
    }
}