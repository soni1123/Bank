using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    public class PayerDetailsEnum
    {
        public enum PayerDetails
        {
            Select = 0,
            Individual = 1,
            Company = 2 ,
            GovernmentBodies = 3,
            ForeignBodies=4,
         
        }

        public partial class PayerDetailsServices
        {
            public static Dictionary<int, string> GetAllPayerDetailssValue()
            {
                var payers = new Dictionary<int, string>();
                payers.Add((int)PayerDetails.Select, "Select.....");
                payers.Add((int)PayerDetails.Individual, "Individual");
                payers.Add((int)PayerDetails.Company, "Company/Corporate/Trust");
                payers.Add((int)PayerDetails.GovernmentBodies, "Government Bodies");
                payers.Add((int)PayerDetails.ForeignBodies, "Foreign Bodies");
                return payers;
            }
            public static string EmployeePayerDetailssById(int id)
            {
                var q = (from p in GetAllPayerDetailssValue() where p.Key == id select p).Single();
                return q.Value;
            }
        }
    }
   
}