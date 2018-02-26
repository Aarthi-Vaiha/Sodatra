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
                    if (textFileList[i].Replace(" ", "") == "No.DescriptionCodeSHUnitéendeviseenFCFA" || textFileList[i].Replace(" ", "") == "No.DescriptionCodeSHUniteendeviseenFCFA")
                    {
                        bool addListValue = true;
                        i++;
                        var invoiceList = new InvoiceList();

                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            var futureRowList = (textFileList.Count() == (j + 1)) ? textFileList[(j)].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries) : textFileList[(j + 1)].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            var usageCount = textFileList[j].Split(new[] { "      " }, StringSplitOptions.RemoveEmptyEntries); // 6 space
                            if (rowList.Count() != 0)
                            {
                                if (rowList.Count() == 2 && rowList[0] == "'----_ .. -" && rowList[1] == " COT€CNA ")
                                {
                                    addListValue = false;
                                }
                                else if (textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUnitéendeviseenFCFA" || textFileList[j].Replace(" ", "") == "No.DescriptionCodeSHUniteendeviseenFCFA")
                                {
                                    addListValue = true;
                                }
                                else if (addListValue)
                                {
                                    if (rowList.Count() >= 7)
                                    {
                                        invoiceList.No = Convert.ToInt32(rowList[0]);
                                        invoiceList.Description = rowList[1];
                                        invoiceList.CodeSH = rowList[2];
                                        if (usageCount.Count() >= 2)
                                        {
                                            invoiceList.Usagé = string.Empty;
                                            invoiceList.Quantité = rowList[3];
                                            invoiceList.Unite = rowList[4];
                                            invoiceList.FOBattestéendevise = rowList[5];
                                            invoiceList.ValeurderéférenceenFCFA = rowList[6];
                                        }
                                        else
                                        {
                                            invoiceList.Usagé = rowList[3];
                                            invoiceList.Quantité = rowList[4];
                                            invoiceList.Unite = rowList[5];
                                            invoiceList.FOBattestéendevise = rowList[6];
                                            invoiceList.ValeurderéférenceenFCFA = rowList[7];
                                        }
                                    }
                                    else
                                    {
                                        invoiceList.Description = invoiceList.Description + " " + rowList[0];
                                    }
                                    if (futureRowList.Count() >= 7 || (textFileList.Count() == (j + 1)) || (invoiceList.CodeSH != "" && rowList.Count() == 2 && rowList[0] == "'----_ .. -" && rowList[1] == " COT€CNA "))
                                    {
                                        model.InvoiceList.Add(invoiceList);
                                        invoiceList = new InvoiceList();
                                    }
                                }
                            }

                        }

                        var DDD = model.InvoiceList;
                        return Json(model);

                        //textBox50.Text = words[0];
                    }
                }
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