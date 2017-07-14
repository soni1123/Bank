using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.Models
{
    public class PartyIdEnum
    {
        public enum partyid
        {
            Select = 0,
            PAN=1,
            PPN=2,
            DLN=3,
            VID=4,
            UID=5,
            TAN=6,
            FID=7
        }

        public partial class partyidsServices
        {
            public static Dictionary<int, string> GetAllpartyidsValue()
            {
                var parties = new Dictionary<int, string>();
                parties.Add((int)partyid.Select, "Select.....");
                parties.Add((int)partyid.PAN, "PAN");
                parties.Add((int)partyid.PPN, "PPN");
                parties.Add((int)partyid.DLN, "DLN");
                parties.Add((int)partyid.VID, "VID");
                parties.Add((int)partyid.UID, "UID");
                return parties;
            }
            public static string EmployeepartyidsById(int id)
            {
                var q = (from p in GetAllpartyidsValue() where p.Key == id select p).Single();
                return q.Value;
            }

            public static int EmployeepartyidsByName(string name)
            {
                var num = (from p in GetAllpartyidsValue() where p.Value == name select p).Single();
                return num.Key;

                //PartyIdEnum foo = (PartyIdEnum)Enum.Parse(typeof(PartyIdEnum), name);
                //return foo;
                
            }
        }

        public partial class partyinfServices
        {
            public static Dictionary<int, string> GetAllpartyinfsValue()
            {
                var partiesinfs = new Dictionary<int, string>();
                //parties.Add((int)partyid.Select, "Select.....");
                partiesinfs.Add((int)partyid.TAN, "TAN");
                partiesinfs.Add((int)partyid.FID, "FID");
                return partiesinfs;
            }
            public static string EmployeepartyinfsById(int id)
            {
                var q = (from p in GetAllpartyinfsValue() where p.Key == id select p).Single();
                return q.Value;
            }
        }
    }
}