using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateBank.ViewModels
{
    public partial  class AdminChartViewModel
    {
        public int Key { get; set; }

        public int Key1 { get; set; }

        public int Key2 { get; set; }

        public int Key3 { get; set; }


        public string Value { get; set; }
    }

    public partial class DailyChatAdmin
    {
        public string Data { get; set; }

    }
}