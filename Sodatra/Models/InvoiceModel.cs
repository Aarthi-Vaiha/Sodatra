using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sodatra.Models
{
    public class InvoiceModel
    {
        public InvoiceModel()
        {
        }
        public InvoiceModel(Table_SodatraDetails obj)
        {
            SodatraId = obj.SodatraId;
            Vendor = obj.Vendor;
            Importer = obj.Importer;
            NumberAV = obj.NumberAV;
            Delivery = obj.Delivery;
            VendorAddress1 = obj.VendorAddress1;
            ImporterAddress1 = obj.ImporterAddress1;
            VendorAddress2 = obj.VendorAddress2;
            ImporterAddress2 = obj.ImporterAddress2;
            VendorAddress3 = obj.VendorAddress3;
            ImporterAddress3 = obj.ImporterAddress3;
            NumberDPI = obj.NumberDPI;
            DateDPI = obj.DateDPI;
            DateAV = obj.DateAV;
            VendorTelephone = obj.VendorTelephone;
            ImporterTelephone = obj.ImporterTelephone;
            VendorFax = obj.VendorFax;
            ImporterFax = obj.ImporterFax;
            VendorContact = obj.VendorContact;
            ImporterContact = obj.ImporterContact;
            WeightGross = obj.WeightGross;
            WeightNet = obj.WeightNet;
            Ninea = obj.Ninea;
            CodePPM = obj.CodePPM;
            CountryofOrgin = obj.CountryofOrgin;
            TaxPayerCode = obj.TaxPayerCode;
            Containersandleads = obj.Containersandleads;
            NatureofGoods = obj.NatureofGoods;
            NatureofPackaging = obj.NatureofPackaging;
            MarksandParcelNumbers = obj.MarksandParcelNumbers;
            PlaceofEntry = obj.PlaceofEntry;
            At = obj.At;
            The = obj.The;
            Bill = obj.Bill;
            Sure = obj.Sure;
            Incoterm = obj.Incoterm;
            BillofLading = obj.BillofLading;
            TotalBill = obj.TotalBill;
            Date = obj.Date;
            Motto = obj.Motto;


            InvoiceList = (from m in obj.Table_SodatraItemDetails.ToList()
                           select new InvoiceList
                           {
                               SodatraItemId = m.SodatraItemId,
                               SodatraId = m.SodatraId,
                               No = m.No,
                               Description = m.Description,
                               HSCode = m.HSCode,
                               Usage = m.Usage,
                               Amount = m.Amount,
                               Unit = m.Unit,
                               FOBAttestedInCurrency = m.FOBAttestedInCurrency,
                               ReferenceValueInFCFA = m.ReferenceValueInFCFA,
                           }).ToList();

            InvoiceGroupList = (from m in obj.Table_SodatraItemDetails
                                group m by m.HSCode into g
                                select new InvoiceList
                                {
                                    Description = g.FirstOrDefault().Description,
                                    HSCode = g.FirstOrDefault().HSCode,
                                    Usage = g.FirstOrDefault().Usage,
                                    AmountValue = g.Sum(x => x.Amount),
                                    //AmountValue = g.Sum(x => Convert.ToDouble(x.Amount.Replace(" ", "").Replace(",", "."))),
                                    Unit = g.FirstOrDefault().Unit,
                                    FOBAttestedInCurrency1 = g.Sum(x => Convert.ToDouble(x.FOBAttestedInCurrency)),
                                    ReferenceValueInFCFA1 = g.Sum(x => Convert.ToDouble(x.ReferenceValueInFCFA)),
                                    //FOBAttestedInCurrency1 = g.Sum(x => Convert.ToDouble(x.FOBAttestedInCurrency.Replace(" ", "").Replace(",", "."))),
                                    //ReferenceValueInFCFA1 = g.Sum(x => Convert.ToDouble(x.ReferenceValueInFCFA.Replace(" ", "").Replace(",", "."))),
                                }).ToList();

            TotalAmount = Convert.ToDouble(InvoiceGroupList.Sum(x => x.AmountValue));
            TotalFOBAttestedInCurrency = InvoiceGroupList.Sum(x => x.FOBAttestedInCurrency1);
            TotalReferenceValueInFCFA = InvoiceGroupList.Sum(x => x.ReferenceValueInFCFA1);


        }

        public long SodatraId { get; set; }
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
        public string VendorTelephone { get; set; }
        public string ImporterTelephone { get; set; }
        public string VendorFax { get; set; }
        public string ImporterFax { get; set; }
        public string VendorContact { get; set; }
        public string ImporterContact { get; set; }
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
        public string The { get; set; }
        public string Bill { get; set; }
        public string Sure { get; set; }
        public string Incoterm { get; set; }
        public string BillofLading { get; set; }
        public string TotalBill { get; set; }
        public string Date { get; set; }
        public string Motto { get; set; }


        public double? TotalAmount { get; set; }
        public double? TotalFOBAttestedInCurrency { get; set; }
        public double? TotalReferenceValueInFCFA { get; set; }

        public List<InvoiceList> InvoiceList { get; set; }
        public List<InvoiceList> InvoiceGroupList { get; set; }

    }
    public class InvoiceList
    {
        //public int No { get; set; }
        //public string Description { get; set; }
        //public string CodeSH { get; set; }
        //public string Usagé { get; set; }
        //public string Quantité { get; set; }
        //public string Unite { get; set; }
        //public string FOBattestéendevise { get; set; }
        //public string ValeurderéférenceenFCFA { get; set; }

        public long SodatraItemId { get; set; }
        public long SodatraId { get; set; }
        public int? No { get; set; }
        public string Description { get; set; }
        public string HSCode { get; set; }
        public string Usage { get; set; }
        public decimal? Amount { get; set; }
        public string Unit { get; set; }

        public string Amount2 { get; set; }
        public string FOBAttestedInCurrency2 { get; set; }
        public string ass { get; set; }
        public string ReferenceValueInFCFA2 { get; set; }

        public decimal? FOBAttestedInCurrency { get; set; }
        public decimal? ReferenceValueInFCFA { get; set; }
        public decimal? AmountValue { get; set; }

        public double? FOBAttestedInCurrency1 { get; set; }
        public double? ReferenceValueInFCFA1 { get; set; }

    }

    public class InvoiceGroupList
    {

        public long SodatraItemId { get; set; }
        public long SodatraId { get; set; }
        public int? No { get; set; }
        public string Description { get; set; }
        public string HSCode { get; set; }
        public string Usage { get; set; }
        public decimal? Amount { get; set; }
        public string Unit { get; set; }

        public string Amount2 { get; set; }
        public string FOBAttestedInCurrency2 { get; set; }
        public string ReferenceValueInFCFA2 { get; set; }
        public decimal? FOBAttestedInCurrency { get; set; }
        public decimal? ReferenceValueInFCFA { get; set; }
        public decimal? AmountValue { get; set; }
        public double? FOBAttestedInCurrency1 { get; set; }
        public double? ReferenceValueInFCFA1 { get; set; }

    }

}
