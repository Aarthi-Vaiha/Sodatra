using Sodatra.Models;
using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sodatra.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
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
            string optionPath = "../Content/UploadFiles";
            string filepath = System.Web.HttpContext.Current.Server.MapPath(uploadpath);
            string optionFilepath = System.Web.HttpContext.Current.Server.MapPath(optionPath);
            filepath += "\\";
            string logotostoreindb = null;
            string optionlogotostoreindb = null;

            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

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
                        logotostoreindb = uploadpath + "/" + System.IO.Path.GetFileName(file.FileName);
                        optionlogotostoreindb = optionPath + "/" + System.IO.Path.GetFileName(file.FileName);
                        file.SaveAs(filepath);
                        //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        //file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    // return Json(logotostoreindb + "$" + optionlogotostoreindb);
                    // return logotostoreindb;
                    return filepath;
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public JsonResult GetTextFilesData(string FileDirectory)
        {
            try
            {

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



                var fileName = UploadFiles();
                // fileName = "D:\\Source\\Sodatra\\files\\Scan text_org.txt";
                //System.IO.StreamReader file =  new System.IO.StreamReader(@"c:\test.txt");
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);

                var line = file.ReadLine();
                var textFileList = System.IO.File.ReadAllLines(fileName);
                var model = new InvoiceModel();
                model.InvoiceList = new List<InvoiceList>();
                for (int i = 0; i < textFileList.Count(); i++)
                {

                    if (((textFileList[i].Replace(" ", "").Contains(VendorText1)) || (textFileList[i].Replace(" ", "").Contains(VendorText2)) || (textFileList[i].Replace(" ", "").Contains(VendorText3)) || (textFileList[i].Replace(" ", "").Contains(VendorText4))))
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

                var DDD = model.InvoiceList;
                return Json(model);

                //textBox50.Text = words[0];

                //  }
                //using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName))
                //{
                //var allLines = System.IO.File.ReadAllLines(fileName);
                // }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json(string.Empty);
        }
    }
}