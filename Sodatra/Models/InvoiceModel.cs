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
      public double? CodeSH { get; set; }
      public string Usagé { get; set; }
      public decimal? Quantité { get; set; }
      public string Unite { get; set; }
      public double? FOBattestéendevise { get; set; }
      public double? ValeurderéférenceenFCFA { get; set; }
      

    }
}