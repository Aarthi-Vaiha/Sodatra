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
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTextFilesData(string FileDirectory)
        {
            try
            {
                var fileName = FileDirectory;
                //var fileName = UploadFiles();

                //  var fileName = FileDirectory;
                var VendorText1 = "Vendeur(";
                var VendorText2 = "Vcndeur(";
                var VendorText3 = "Vend~~Jr(";
                var VendorText4 = "Vendcur(";
                var CodePPMText1 = "CodePPM";
                var CodePPMText2 = "CodePPm";
                var CodePPMText3 = "NINEA";
                var CodePPMText4 = "CcdePPM";
                var ContainersText1 = "Conteneurs";
                var ContainersText2 = "Centoneurs";
                var ContainersText3 = "Conteneurs";
                var ContainersText4 = "Conteneurs";
                var NatureofGoodsText1 = "Naturedesmarchandiscs";
                var NatureofGoodsText2 = "Naturedesmarchandises";
                var NatureofGoodsText3 = "Naturedesmarchandis";
                var NatureofGoodsText4 = "Naturcdesmarchandiscs";
                var NatureofPackagingText1 = "NaturedeI'emballage";
                var NatureofPackagingText2 = "NaturedeI'emballage";
                var NatureofPackagingText3 = "NaturedeI'emballage";
                var NatureofPackagingText4 = "NaturedeI'emballage";
                var ConnaissementText1 = "ConnaissementallLTA";
                var ConnaissementText2 = "ConnaissemcntouLTA";
                var ConnaissementText3 = "Connaissemenl";
                var ConnaissementText4 = "Connaissement";
 
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);

                var line = file.ReadLine();
                var textFileList = System.IO.File.ReadAllLines(fileName);
                var model = new InvoiceModel();
                model.InvoiceList = new List<InvoiceList>();
                for (int i = 0; i < textFileList.Count(); i++)
                {

                    var getString = textFileList[i].Replace(" ", "");
                    bool checkString1 = getString.Contains(Resource.setString1);
                    bool checkString2 = getString.Contains(Resource.setString2);
                    bool checkString3 = getString.Contains(Resource.setString3);
                    bool checkString2Enable = false;
                    //if (textFileList[i].Replace(" ", "") == "No.DescriptionCodeSHUnitéendeviseenFCFA" || textFileList[i].Replace(" ", "") == "No.DescriptionCodeSHUniteendeviseenFCFA")
                    if (checkString1)


                    if (((textFileList[i].Replace(" ", "").Contains(VendorText1)) || (textFileList[i].Replace(" ", "").Contains(VendorText2)) || (textFileList[i].Replace(" ", "").Contains(VendorText3)) || (textFileList[i].Replace(" ", "").Contains(VendorText4))))
// master
                    {
                        i++;
//mahalingam
                        var invoiceList = new InvoiceList();
                        var dd = 51;
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            if (j == dd)
                            {
                                dd = j;
                            }
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
                                //else if (textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUnitéendeviseenFCFA" || textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUniteendeviseenFCFA" || textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUsageOuantiteUnitéFOBattestsValeurdereference" || textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUsageOuantiteUniteFOBattestsValeurdereference")
                                {
                                    addListValue = true;
                                    checkString2Enable = true;
                                }
                                else if (getString == Resource.description || getString == Resource.UsageQuantite || getString == Resource.uniteFOBatteste || getString == Resource.endevise || getString == Resource.valeurdereference || getString == Resource.enFCFA || getString == Resource.no)
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
                                        invoiceList.CodeSH = rowList[2];
                                        if (usageCount.Count() >= 2 || (checkString2Enable && usageCount2.Count() >= 2))
                                        {
                                            invoiceList.Usagé = string.Empty;
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
                                                invoiceList.Quantité = quantité;
                                                invoiceList.Unite = unite;
                                                //invoiceList.Quantité = splitQtyUnite[0];
                                                //invoiceList.Unite = splitQtyUnite[1];
                                                invoiceList.FOBattestéendevise = rowList[4];
                                                invoiceList.ValeurderéférenceenFCFA = rowList[5];
                                            }
                                            else
                                            {
                                                invoiceList.Quantité = rowList[3];
                                                invoiceList.Unite = rowList[4];
                                                invoiceList.FOBattestéendevise = rowList[5];
                                                invoiceList.ValeurderéférenceenFCFA = rowList[6];
                                            }
                                        }
                                        else
                                        {
                                            invoiceList.Usagé = rowList[3];
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
                                                invoiceList.Quantité = splitQtyUnite[0];
                                                invoiceList.Unite = splitQtyUnite[1];
                                                invoiceList.FOBattestéendevise = rowList[5];
                                                invoiceList.ValeurderéférenceenFCFA = rowList[6];
                                            }
                                            else
                                            {
                                                invoiceList.Quantité = rowList[4];
                                                invoiceList.Unite = rowList[5];
                                                invoiceList.FOBattestéendevise = rowList[6];
                                                invoiceList.ValeurderéférenceenFCFA = rowList[7];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        invoiceList.Description = invoiceList.Description + " " + rowList[0];
                                    }


                                    if (futureRowList.Count() >= 7 || (textFileList.Count() == (j + 1)) || (invoiceList.CodeSH != "" && futureRowList.Count() == 2 && futureRowList[0] == "'----_ .. -" && futureRowList[1] == " COT€CNA "))
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
//=======
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

                    if (((textFileList[i].Replace(" ", "").Contains(CodePPMText1)) || (textFileList[i].Replace(" ", "").Contains(CodePPMText2)) || (textFileList[i].Replace(" ", "").Contains(CodePPMText3)) || (textFileList[i].Replace(" ", "").Contains(CodePPMText4))))


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
// master
                                }
                            }
                        }
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(ContainersText1)) || (textFileList[i].Replace(" ", "").Contains(ContainersText2)) || (textFileList[i].Replace(" ", "").Contains(ContainersText3)) || (textFileList[i].Replace(" ", "").Contains(ContainersText4))))


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
//mahalingam
//=======
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(NatureofGoodsText1)) || (textFileList[i].Replace(" ", "").Contains(NatureofGoodsText2)) || (textFileList[i].Replace(" ", "").Contains(NatureofGoodsText3)) || (textFileList[i].Replace(" ", "").Contains(NatureofGoodsText4))))


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
//>>>>>>> master
                    }
                    if (((textFileList[i].Replace(" ", "").Contains(NatureofPackagingText1)) || (textFileList[i].Replace(" ", "").Contains(NatureofPackagingText2)) || (textFileList[i].Replace(" ", "").Contains(NatureofPackagingText3)) || (textFileList[i].Replace(" ", "").Contains(NatureofPackagingText4))))


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
                    if (((textFileList[i].Replace(" ", "").Contains(ConnaissementText1)) || (textFileList[i].Replace(" ", "").Contains(ConnaissementText2)) || (textFileList[i].Replace(" ", "").Contains(ConnaissementText3)) || (textFileList[i].Replace(" ", "").Contains(ConnaissementText4))))


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


                    //if (textFileList[i].Replace(" ", "") == "No.DescriptionCodeSHUnitéendeviseenFCFA" || textFileList[i].Replace(" ", "") == "No.DescriptionCodeSHUniteendeviseenFCFA")
                    //{
                    //    bool addListValue = true;
                    //    i++;
                    //    var invoiceList = new InvoiceList();

                    //    for (int j = i; j < textFileList.Count(); j++)
                    //    {
                    //        var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                    //        var futureRowList = (textFileList.Count() == (j + 1)) ? textFileList[(j)].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries) : textFileList[(j + 1)].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                    //        var usageCount = textFileList[j].Split(new[] { "      " }, StringSplitOptions.RemoveEmptyEntries); // 6 space
                    //        if (rowList.Count() != 0)
                    //        {
                    //            if (rowList.Count() == 2 && rowList[0] == "'----_ .. -" && rowList[1] == " COT€CNA ")
                    //            {
                    //                addListValue = false;
                    //            }
                    //            else if (textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUnitéendeviseenFCFA" || textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUniteendeviseenFCFA")
                    //            {
                    //                addListValue = true;
                    //            }
                    //            else if (addListValue)
                    //            {
                    //                if (rowList.Count() >= 7)
                    //                {
                    //                    invoiceList.No = Convert.ToInt32(rowList[0]);
                    //                    invoiceList.Description = rowList[1];
                    //                    invoiceList.CodeSH = rowList[2];
                    //                    if (usageCount.Count() >= 2)
                    //                    {
                    //                        invoiceList.Usagé = string.Empty;
                    //                        invoiceList.Quantité = rowList[3];
                    //                        invoiceList.Unite = rowList[4];
                    //                        invoiceList.FOBattestéendevise = rowList[5];
                    //                        invoiceList.ValeurderéférenceenFCFA = rowList[6];
                    //                    }
                    //                    else
                    //                    {
                    //                        invoiceList.Usagé = rowList[3];
                    //                        invoiceList.Quantité = rowList[4];
                    //                        invoiceList.Unite = rowList[5];
                    //                        invoiceList.FOBattestéendevise = rowList[6];
                    //                        invoiceList.ValeurderéférenceenFCFA = rowList[7];
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    invoiceList.Description = invoiceList.Description + " " + rowList[0];
                    //                }
                    //                if (futureRowList.Count() >= 7 || (textFileList.Count() == (j + 1)) || (invoiceList.CodeSH != "" && futureRowList.Count() == 2 && futureRowList[0] == "'----_ .. -" && futureRowList[1] == " COT€CNA "))
                    //                {
                    //                    model.InvoiceList.Add(invoiceList);
                    //                    invoiceList = new InvoiceList();
                    //                }
                    //            }
                    //        }

                    //    }
                    //}
                }
//<<<<<<< mahalingam
                var DDD = model.InvoiceList;
                return Json(model);
//=======

                var DDD = model.InvoiceList;
                return Json(model);

                //textBox50.Text = words[0];

                //  }
                //using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName))
                //{
                //var allLines = System.IO.File.ReadAllLines(fileName);
                // }
//>>>>>>> master
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(string.Empty);
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