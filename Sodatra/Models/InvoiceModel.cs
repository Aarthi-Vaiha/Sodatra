using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sodatra.Models
{
    public class InvoiceModel
    {

        public List<InvoiceList> InvoiceList { get; set; }
    }

    public class InvoiceList
    {
      public int No { get; set; }
      public string Description { get; set; }
      public string CodeSH { get; set; }
      public string Usagé { get; set; }
      public string Quantité { get; set; }
      public string Unite { get; set; }
      public string FOBattestéendevise { get; set; }
      public string ValeurderéférenceenFCFA { get; set; }
      

    }
}