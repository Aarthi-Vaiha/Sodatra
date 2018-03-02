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
                    {
                        bool addListValue = true;
                        i++;
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