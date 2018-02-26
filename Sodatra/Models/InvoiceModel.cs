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
        //public double? FOBattestéendevise { get; set; }
        //public double? ValeurderéférenceenFCFA { get; set; }
        public string TextFile { get; set; }
        public string Vendor { get; set; }
        public string Importer { get; set; }
        public string NumberAV { get; set; }
        public string Delivery { get; set; }
        public string VendorAddress1 { get; set; }
        public string ImporterAddress1 { get; set; }
        public string VendorAddress2 { get; set; }
        public string ImporterAddress2 { get; set; }
        public string VendorAddress3 { get; set; }
        public string ImporterAddress3 { get; set; }
        public string NumberDPI { get; set; }
        public string DateDPI { get; set; }
        public string DateAV { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string WeightGross { get; set; }
        public string WeightNet { get; set; }
        public string Ninea { get; set; }
        public string CodePPM { get; set; }
        public string CountryofOrgin { get; set; }
        public string TaxPayerCode { get; set; }
        public string Containersandleads { get; set; }
        public string NatureofGoods { get; set; }
        public string NatureofPackaging { get; set; }
        public string MarksandParcelNumbers { get; set; }
        public string PlaceofEntry { get; set; }
        public string At { get; set; }
        public string Bill { get; set; }
        public string Sure { get; set; }
        public string Incoterm { get; set; }
        public string BillofLading { get; set; }
        public string TotalBill { get; set; }
        public string Date { get; set; }
        public string Motto { get; set; }
        public string FOBattestéendevise { get; set; }
        public string ValeurderéférenceenFCFA { get; set; }


        
    }
}
