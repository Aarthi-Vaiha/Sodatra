using Sodatra.Models;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Sodatra.Content;

namespace Sodatra.Controllers
{
    public class InvoiceController : Controller
    {
        SodatraEntities db = new SodatraEntities();
        // GET: Invoice
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            TempData["Message"] = null;
            return View(db.Table_SodatraDetails.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult View(long id)
        {
            var objValue = db.Table_SodatraDetails.FirstOrDefault(x => x.SodatraId == id);
            var model = new InvoiceModel(objValue);
            return View(model);
        }

        [HttpPost]
        // public ActionResult Create(Table_SodatraDetails model)
        public ActionResult Create(InvoiceModel model)
        {
            try
            {
                // db.Table_SodatraDetails.Add(model);
                var objValue = (new Table_SodatraDetails
                {
                    Vendor = model.Vendor,
                    Importer = model.Importer,
                    NumberAV = model.NumberAV,
                    Delivery = model.Delivery,
                    VendorAddress1 = model.VendorAddress1,
                    ImporterAddress1 = model.ImporterAddress1,
                    VendorAddress2 = model.VendorAddress2,
                    ImporterAddress2 = model.ImporterAddress2,
                    VendorAddress3 = model.VendorAddress3,
                    ImporterAddress3 = model.ImporterAddress3,
                    NumberDPI = model.NumberDPI,
                    DateDPI = model.DateDPI,
                    DateAV = model.DateAV,
                    VendorTelephone = model.VendorTelephone,
                    ImporterTelephone = model.ImporterTelephone,
                    VendorFax = model.VendorFax,
                    ImporterFax = model.ImporterFax,
                    VendorContact = model.VendorContact,
                    ImporterContact = model.ImporterContact,
                    WeightGross = model.WeightGross,
                    WeightNet = model.WeightNet,
                    Ninea = model.Ninea,
                    CodePPM = model.CodePPM,
                    CountryofOrgin = model.CountryofOrgin,
                    TaxPayerCode = model.TaxPayerCode,
                    Containersandleads = model.Containersandleads,
                    NatureofGoods = model.NatureofGoods,
                    NatureofPackaging = model.NatureofPackaging,
                    MarksandParcelNumbers = model.MarksandParcelNumbers,
                    PlaceofEntry = model.PlaceofEntry,
                    At = model.At,
                    The = model.The,
                    Bill = model.Bill,
                    Sure = model.Sure,
                    Incoterm = model.Incoterm,
                    BillofLading = model.BillofLading,
                    TotalBill = model.TotalBill,
                    Date = model.Date,
                    Motto = model.Motto,

                    Table_SodatraItemDetails = (from m in model.InvoiceList.ToList()
                                                select new Table_SodatraItemDetails
                                                {
                                                    No = m.No,
                                                    Description = m.Description,
                                                    HSCode = m.HSCode,
                                                    Usage = m.Usage,
                                                    Amount = Convert.ToDecimal(m.Amount2.Replace(" ", "").Replace(",", ".").Replace("'", "")),
                                                    Unit = m.Unit,
                                                    FOBAttestedInCurrency = Convert.ToDecimal(m.FOBAttestedInCurrency2.Replace(" ", "").Replace(",", ".").Replace("'", "")),
                                                    ReferenceValueInFCFA = Convert.ToDecimal(m.ReferenceValueInFCFA2.Replace(" ", "").Replace(",", ".").Replace("'", "")),
                                                }).ToList()
                });
                    db.Table_SodatraDetails.Add(objValue);
                db.SaveChanges();
                TempData["Message"] = "Loaded details Created successfully";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Loaded details Created error";
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetTextFilesData(string FileDirectory)
        {
            try
            {
                var fileName = FileDirectory;
                //var fileName = UploadFiles();
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);

                var line = file.ReadLine();
                var textFileList = System.IO.File.ReadAllLines(fileName);
                var model = new InvoiceModel();
                model.InvoiceList = new List<InvoiceList>();
                for (int i = 0; i < textFileList.Count(); i++)
                {
                    if (((textFileList[i].Replace(" ", "").Contains(Resource.VendorText1)) || (textFileList[i].Replace(" ", "").Contains(Resource.VendorText2)) || (textFileList[i].Replace(" ", "").Contains(Resource.VendorText3)) || (textFileList[i].Replace(" ", "").Contains(Resource.VendorText4))))
                    {
                        i++;
                        var linenumber = 1;
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            if (rowList.Count() != 0)
                            {
                                if (linenumber == 1)
                                {
                                    model.Vendor = rowList[0];
                                    model.Importer = rowList[1];
                                    model.NumberAV = rowList[2];
                                    model.Delivery = rowList[3];
                                    linenumber++;
                                }
                                else if (linenumber == 2)
                                {
                                    model.VendorAddress1 = rowList[0];
                                    model.ImporterAddress1 = rowList[1];
                                    linenumber++;
                                }
                                else if (linenumber == 3)
                                {
                                    model.VendorAddress2 = rowList[0];
                                    model.ImporterAddress2 = rowList[1];
                                    model.NumberDPI = rowList[2];
                                    model.DateDPI = rowList[3];
                                    model.DateAV = rowList[4];
                                    linenumber++;
                                }
                                else if (linenumber == 4)
                                {
                                    model.VendorAddress3 = rowList[0];
                                    model.ImporterAddress3 = rowList[1];
                                    linenumber++;
                                }
                                else if (linenumber == 5)
                                {
                                    model.VendorTelephone = rowList[1];
                                    model.ImporterTelephone = rowList[3];
                                    model.WeightGross = rowList[5];
                                    linenumber++;
                                }
                                else if (linenumber == 6)
                                {
                                    model.VendorFax = rowList[1];
                                    model.ImporterFax = rowList[3];
                                    linenumber++;
                                }
                                else if (linenumber == 7)
                                {
                                    model.WeightNet = rowList[1];
                                    linenumber++;
                                }
                                else if (linenumber == 8)
                                {
                                    model.VendorContact = rowList[1];
                                    model.ImporterContact = rowList[3];
                                    linenumber++;
                                }
                                else if (linenumber == 9)
                                {
                                    linenumber++;
                                }
                            }
                        }
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(Resource.CodePPMText1)) || (textFileList[i].Replace(" ", "").Contains(Resource.CodePPMText2)) || (textFileList[i].Replace(" ", "").Contains(Resource.CodePPMText3)) || (textFileList[i].Replace(" ", "").Contains(Resource.CodePPMText4))))
                    {
                        i++;
                        var linenumber = 1;
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            if (rowList.Count() != 0)
                            {
                                if (linenumber == 1)
                                {
                                    model.CodePPM = rowList[0];
                                    model.Ninea = rowList[1];
                                    model.TaxPayerCode = rowList[2];
                                    model.CountryofOrgin = rowList[3];
                                    linenumber++;
                                }
                                else if (linenumber == 2)
                                {
                                    linenumber++;
                                }
                            }
                        }
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(Resource.ContainersText1)) || (textFileList[i].Replace(" ", "").Contains(Resource.ContainersText2)) || (textFileList[i].Replace(" ", "").Contains(Resource.ContainersText3)) || (textFileList[i].Replace(" ", "").Contains(Resource.ContainersText4))))
                    {
                        i++;
                        var linenumber = 1;
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            if (rowList.Count() != 0)
                            {
                                if (linenumber == 1)
                                {
                                    model.Containersandleads = rowList[0] + " " + rowList[1];
                                    linenumber++;
                                }
                                else if (linenumber == 2)
                                {
                                    linenumber++;
                                }
                            }
                        }
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(Resource.NatureofGoodsText1)) || (textFileList[i].Replace(" ", "").Contains(Resource.NatureofGoodsText2)) || (textFileList[i].Replace(" ", "").Contains(Resource.NatureofGoodsText3)) || (textFileList[i].Replace(" ", "").Contains(Resource.NatureofGoodsText4))))
                    {
                        i++;
                        var linenumber = 1;
                        for (int j = i; j < textFileList.Count(); j++)
                        {

                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            if (rowList.Count() != 0)
                            {
                                if (linenumber == 1)
                                {
                                    model.NatureofGoods = rowList[0];
                                    linenumber++;
                                }
                                else if (linenumber == 2)
                                {
                                    linenumber++;
                                }
                            }
                        }
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(Resource.NatureofPackagingText1)) || (textFileList[i].Replace(" ", "").Contains(Resource.NatureofPackagingText2)) || (textFileList[i].Replace(" ", "").Contains(Resource.NatureofPackagingText3)) || (textFileList[i].Replace(" ", "").Contains(Resource.NatureofPackagingText4))))
                    {
                        i++;
                        var linenumber = 1;
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            if (rowList.Count() != 0)
                            {
                                if (linenumber == 1)
                                {
                                    model.NatureofPackaging = rowList[0];
                                    model.MarksandParcelNumbers = rowList[1];
                                    model.At = rowList[3];
                                    linenumber++;
                                }
                                else if (linenumber == 2)
                                {
                                    model.The = rowList[1];
                                    linenumber++;
                                }
                                else if (linenumber == 3)
                                {
                                    model.Sure = rowList[1];
                                    linenumber++;
                                }
                                else if (linenumber == 4)
                                {

                                    model.PlaceofEntry = rowList[1];
                                    linenumber++;
                                }
                                else if (linenumber == 5)
                                {

                                    linenumber++;
                                }
                            }
                        }
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(Resource.ConnaissementText1)) || (textFileList[i].Replace(" ", "").Contains(Resource.ConnaissementText2)) || (textFileList[i].Replace(" ", "").Contains(Resource.ConnaissementText3)) || (textFileList[i].Replace(" ", "").Contains(Resource.ConnaissementText4))))
                    {
                        i++;
                        var linenumber = 1;
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            if (rowList.Count() != 0)
                            {
                                if (linenumber == 1)
                                {

                                    model.BillofLading = rowList[0];
                                    model.Bill = rowList[1];
                                    model.Date = rowList[2];
                                    model.Incoterm = rowList[3];
                                    model.Motto = rowList[4];
                                    model.TotalBill = rowList[5];
                                    linenumber++;
                                }
                                else if (linenumber == 2)
                                {

                                    linenumber++;
                                }
                            }
                        }
                    }

                    var getString = textFileList[i].Replace(" ", "");
                    bool checkString1 = getString.Contains(Resource.setString1);
                    bool checkString2 = getString.Contains(Resource.setString2);
                    bool checkString3 = getString.Contains(Resource.setString3);
                    bool checkString2Enable = false;
                    //if (textFileList[i].Replace(" ", "") == "No.DescriptionHSCodeUnitéendeviseenFCFA" || textFileList[i].Replace(" ", "") == "No.DescriptionHSCodeUniteendeviseenFCFA")
                    if (checkString1)
                    {
                        bool addListValue = true;
                        i++;
                        var invoiceList = new InvoiceList();
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            var futureRowList = (textFileList.Count() == (j + 1)) ? textFileList[(j)].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries) : textFileList[(j + 1)].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            var usageCount = textFileList[j].Split(new[] { "      " }, StringSplitOptions.RemoveEmptyEntries); // 6 space
                            var usageCount2 = textFileList[j].Split(new[] { "     " }, StringSplitOptions.RemoveEmptyEntries); // 6 space
                            if (rowList.Count() != 0)
                            {
                                getString = textFileList[j].Replace(" ", "");
                                checkString1 = getString.Contains(Resource.setString1);
                                checkString2 = getString.Contains(Resource.setString2);
                                checkString3 = getString.Contains(Resource.setString3);

                                if (rowList.Count() == 2 && rowList[0] == "'----_ .. -" && rowList[1] == " COT€CNA ")
                                {
                                    addListValue = false;
                                }
                                else if (checkString1 || checkString2 || checkString3)
                                //else if (textFileList[j].Replace(" ", "") == "No.DescriptionHSCodeUnitéendeviseenFCFA" || textFileList[j].Replace(" ", "") == "No.DescriptionHSCodeUniteendeviseenFCFA" || textFileList[j].Replace(" ", "") == "No.DescriptionHSCodeUsageOuantiteUnitéFOBattestsValeurdereference" || textFileList[j].Replace(" ", "") == "No.DescriptionHSCodeUsageOuantiteUniteFOBattestsValeurdereference")
                                {
                                    addListValue = true;
                                    checkString2Enable = true;
                                }
                                else if (getString == Resource.description || getString == Resource.UsageQuantite || getString == Resource.UnitFOBatteste || getString == Resource.endevise || getString == Resource.valeurdereference || getString == Resource.enFCFA || getString == Resource.no)
                                {
                                    addListValue = true;
                                    checkString2Enable = true;
                                }
                                else if (addListValue)
                                {
                                    if (rowList.Count() >= 7)
                                    {
                                        invoiceList.No = Convert.ToInt32(rowList[0]);
                                        invoiceList.Description = rowList[1];
                                        invoiceList.HSCode = rowList[2];
                                        if (usageCount.Count() >= 2 || (checkString2Enable && usageCount2.Count() >= 2))
                                        {
                                            invoiceList.Usage = string.Empty;
                                            var dotSplitQtyUnite = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries)[3].Split('.');
                                            var commaSplitQtyUnite = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries)[3].Split(',');
                                            var splitQtyUnite = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                                            var quantité = "";
                                            var unite = "";
                                            if (dotSplitQtyUnite.Count() == 2)
                                            {
                                                splitQtyUnite = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split('.')[1].Split(' ');
                                                var spaceCount = splitQtyUnite;
                                                if (spaceCount.Count() == 2)
                                                {
                                                    spaceCount = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split(' ');
                                                    for (int q = 0; q < spaceCount.Count(); q++)
                                                    {
                                                        if ((q + 1) == spaceCount.Count())
                                                            unite = spaceCount[q];
                                                        else
                                                        {
                                                            if (quantité == "")
                                                                quantité = spaceCount[q];
                                                            else
                                                                quantité = quantité + " " + spaceCount[q];
                                                        }
                                                    }
                                                }
                                            }
                                            else if (commaSplitQtyUnite.Count() == 2)
                                            {
                                                splitQtyUnite = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split(',')[1].Split(' ');
                                                var spaceCount = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split(',')[1].Split(' ');
                                                if (spaceCount.Count() == 2)
                                                {
                                                    spaceCount = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split(' ');

                                                    for (int q = 0; q < spaceCount.Count(); q++)
                                                    {
                                                        if ((q + 1) == spaceCount.Count())
                                                            unite = spaceCount[q];
                                                        else
                                                        {
                                                            if (quantité == "")
                                                                quantité = spaceCount[q];
                                                            else
                                                                quantité = quantité + " " + spaceCount[q];
                                                        }
                                                    }
                                                }

                                            }
                                            if (splitQtyUnite.Count() == 2)
                                            {
                                                invoiceList.Amount2 = quantité;
                                                invoiceList.Unit = unite;
                                                //invoiceList.Amount = splitQtyUnite[0];
                                                //invoiceList.Unite = splitQtyUnite[1];
                                                invoiceList.FOBAttestedInCurrency2 = rowList[4];
                                                invoiceList.ReferenceValueInFCFA2 = rowList[5];
                                            }
                                            else
                                            {
                                                invoiceList.Amount2 = rowList[3];
                                                invoiceList.Unit = rowList[4];
                                                invoiceList.FOBAttestedInCurrency2 = rowList[5];
                                                invoiceList.ReferenceValueInFCFA2 = rowList[6];
                                            }
                                        }
                                        else
                                        {
                                            invoiceList.Usage = rowList[3];
                                            var dotSplitQtyUnite = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries)[3].Split('.');
                                            var commaSplitQtyUnite = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries)[3].Split(',');
                                            var splitQtyUnite = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                            if (dotSplitQtyUnite.Count() == 2)
                                            {
                                                splitQtyUnite = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split('.')[1].Split(' ');
                                            }
                                            else if (commaSplitQtyUnite.Count() == 2)
                                            {
                                                splitQtyUnite = (textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries))[3].Split(',')[1].Split(' ');
                                            }
                                            if (splitQtyUnite.Count() == 2)
                                            {
                                                invoiceList.Amount2 = splitQtyUnite[0];
                                                invoiceList.Unit = splitQtyUnite[1];
                                                invoiceList.FOBAttestedInCurrency2 = rowList[5];
                                                invoiceList.ReferenceValueInFCFA2 = rowList[6];
                                            }
                                            else
                                            {
                                                invoiceList.Amount2 = rowList[4];
                                                invoiceList.Unit = rowList[5];
                                                invoiceList.FOBAttestedInCurrency2 = rowList[6];
                                                invoiceList.ReferenceValueInFCFA2 = rowList[7];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        invoiceList.Description = invoiceList.Description + " " + rowList[0];
                                    }


                                    if (futureRowList.Count() >= 7 || (textFileList.Count() == (j + 1)) || (invoiceList.HSCode != "" && futureRowList.Count() == 2 && futureRowList[0] == "'----_ .. -" && futureRowList[1] == " COT€CNA "))
                                    {
                                        model.InvoiceList.Add(invoiceList);
                                        invoiceList = new InvoiceList();
                                    }
                                    else if (futureRowList.Count() == 0)
                                    {
                                        model.InvoiceList.Add(invoiceList);
                                        invoiceList = new InvoiceList();
                                        addListValue = false;
                                    }
                                    else if (futureRowList.Count() == 5 && futureRowList[1] == " DE" && futureRowList[2] == " TEXTE")
                                    {
                                        model.InvoiceList.Add(invoiceList);
                                        invoiceList = new InvoiceList();
                                        j = textFileList.Count() + 1;
                                        i = textFileList.Count() + 1;
                                    }
                                }
                            }

                        }
                    }
                }
                var DDD = model.InvoiceList;
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(string.Empty);
        }

        public ActionResult Delete(long id)
        {
            try
            {
                var objValue = db.Table_SodatraDetails.FirstOrDefault(x => x.SodatraId == id);
                db.Table_SodatraItemDetails.RemoveRange(objValue.Table_SodatraItemDetails);
                db.Table_SodatraDetails.Remove(objValue);
                db.SaveChanges();
                TempData["Message"] = "Current details delete successfully";

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Current details delete rror";
            }
            return RedirectToAction("Index");

        }



        /// <summary>
        /// UploadFiles methods
        /// </summary>
        /// <returns>file path </returns>
        // [HttpPost]
        //public ActionResult UploadFiles()
        public string UploadFiles()
        {
            string uploadpath = "~/Content/UploadFiles";
            string filepath = System.Web.HttpContext.Current.Server.MapPath(uploadpath);
            filepath += "\\";

            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it. 
                        filepath += System.IO.Path.GetFileName(file.FileName);
                        file.SaveAs(filepath);
                    }

                    return filepath;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("because it is being used by another process."))
                        return filepath;
                    else
                        return string.Empty;

                }
            }
            else
            {
                return string.Empty;
            }
        }

    }
}