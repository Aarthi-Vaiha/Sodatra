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
                    return logotostoreindb;
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

        public JsonResult GetTextFilesData()
        {
            try
            {

                
                /// var fileName = UploadFiles();
                var fileName = "D:\\Source\\Sodatra\\files\\Scan text_org.txt";
                //System.IO.StreamReader file =  new System.IO.StreamReader(@"c:\test.txt");
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);

                var line = file.ReadLine();
                var textFileList = System.IO.File.ReadAllLines(fileName);
                var model = new InvoiceModel();
                model.InvoiceList = new List<InvoiceList>();
                for (int i = 0; i < textFileList.Count(); i++)
                {
                    if (textFileList[i].Replace(" ", "") == "No.DescriptionCodeSHUnitéendeviseenFCFA")
                    {
                        i++;
                        for (int j = i; j < textFileList.Count(); j++)
                        {
                            var words = textFileList[j].Split(new[] { "", " ", "     " }, StringSplitOptions.RemoveEmptyEntries);
                            var rowList = textFileList[j].Split(' ');
                            rowList = textFileList[j].Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);// 2 space
                            var usageCount = textFileList[j].Split(new[] { "      " }, StringSplitOptions.RemoveEmptyEntries); // 6 space

                            var invoiceList = new InvoiceList();
                            invoiceList.No = Convert.ToInt32(rowList[0]);
                            invoiceList.Description = rowList[1];
                            invoiceList.CodeSH =Convert.ToDouble( rowList[2]);
                            if (usageCount.Count() >= 2)
                            {
                                invoiceList.Usagé = usageCount.Count() == 2 ? string.Empty : rowList[3];
                                invoiceList.Quantité = Convert.ToDecimal(rowList[3]);
                                invoiceList.Usagé = rowList[4];
                                invoiceList.FOBattestéendevise = Convert.ToDouble(rowList[5]);
                                invoiceList.ValeurderéférenceenFCFA = Convert.ToDouble(rowList[6]);
                            }
                            else
                            {
                                invoiceList.Usagé = usageCount.Count() == 2 ? string.Empty : rowList[3];
                                invoiceList.Quantité = Convert.ToDecimal(rowList[4]);
                                invoiceList.Usagé = rowList[5];
                                invoiceList.FOBattestéendevise = Convert.ToDouble(rowList[6]);
                                invoiceList.ValeurderéférenceenFCFA = Convert.ToDouble(rowList[7]);
                            }

                            model.InvoiceList.Add(invoiceList);

                            //for (int k = 0; k < words.Count(); k++)
                            //{

                            //}
                        }
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