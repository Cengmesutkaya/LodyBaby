using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Package_Ecommerce.Areas.Admin.Models;
using Package_Ecommerce.Areas.Admin.Utils;
using Package_Ecommerce.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Package_Ecommerce.Areas.Admin.Controllers
{
    public class WorkOrderController : BaseAdminController
    {
        public ActionResult Index(FormModel form)
        {
            FormModel form1 = (from actionplan in manager.repo_offer.List().AsEnumerable()

                           select new FormModel
                           {
                               Id = actionplan.Id,
                               boy = actionplan.Height.Split('®'),
                               ozellik = actionplan.Property.Split('®'),
                               adet = actionplan.Number.Split('®'),
                               fiyat = actionplan.Price.Split('®'),
                               addedBy = actionplan.Createby,
                               addedDate = actionplan.CreateDate.ToShortDateString(),
                               companyName = actionplan.CompanyName,
                               personEmail = actionplan.Email,
                               gsm = actionplan.Gsm,
                               personName = actionplan.PersonName
                           }).FirstOrDefault();
            SendPDFEmail(form1);
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        public String Complete(FormModel formModel, Offer offer)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                file.SaveAs(path);
            }
            try
            {
           
                int rowCount = formModel.boy.Count();
                string boy = "";
                string ozellik = "";
                string adet = "";
                string fiyat = "";

                for (int i = 0; i < rowCount; i++)
                {
                    if (i == 0)
                    {
                        boy = formModel.boy[i];
                        ozellik = formModel.ozellik[i];
                        adet = formModel.adet[i];
                        fiyat = formModel.fiyat[i];

                    }
                    else
                    {
                        boy += "®" + formModel.boy[i];
                        ozellik += "®" + formModel.ozellik[i];
                        adet += "®" + formModel.adet[i];
                        fiyat += "®" + formModel.fiyat[i];
                    }
                }

                FormModel form = (new FormModel
                                  {
                                      Id =10,
                                      boy = boy.Split('®'),
                                      ozellik = ozellik.Split('®'),
                                      adet = adet.Split('®'),
                                      fiyat = fiyat.Split('®'),
                                      addedBy = formModel.addedBy,
                                      addedDate = DateTime.Now.ToShortDateString(),
                                      companyName = offer.CompanyName,
                                      personEmail = formModel.personEmail,
                                      gsm = formModel.gsm,
                                      personName = offer.PersonName
                                  });
                Index(form);
         
                return "True";

            }
            catch (Exception)
            {
                return "Hata";
            }
        }


        private void SendPDFEmail(FormModel dt)
        {

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    string companyName = dt.companyName;
                    string Name = dt.personName;
                    string Email = dt.personEmail;
                    int Id = dt.Id;
                    string Tel = dt.gsm;
                    DateTime goodDateHolder = DateTime.Now;
                    string date = goodDateHolder.ToString("dd/MM/yyyy");
                    StringBuilder sb = new StringBuilder();

                    sb.Append("<table  width='100%' cellspacing='0' cellpadding='3'>");
                    sb.Append("<tr><td colspan = '6'></td></tr>");
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td align='center'  colspan = '6'><h2><b> HERBOYKOLİ İŞ EMRİ </b></h2></td></tr>");
                    sb.Append(" </td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    sb.Append("<table  width='100%' align='left'  border = '1'> ");
                    sb.Append("<tr ><td colspan = '8'></td></tr>");
                    sb.Append("<tr><td align='center'  colspan = '8'> Firma Adı <h2><b> Fixed Soft </b></h2></td></tr>");
                    sb.Append("<tr><td colspan = '1'><b> İş Emri Veren </b> ");
                    sb.Append("<td colspan = '5'>" + Name + "</td>");
                    sb.Append("<td colspan = '1'><b> Sıra No </b>");
                    sb.Append("<td colspan = '1'>" + Id + "</td></tr>");

                    sb.Append("<tr><td colspan = '1'><b> Tel </b> ");
                    sb.Append("<td colspan = '5'>" + Tel + "</td></tr>");
                    sb.Append("<td colspan = '1'><b> Tarih </b>");
                    sb.Append("<td colspan = '1'>" + date + "</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    sb.Append("<table border = '1'>");
                    sb.Append("<tr style='background-color:yellow;color:white'>");
                    sb.Append("<th><b> EBAT </b></th>");
                    sb.Append("<th colspan = '3'><b> ÖZELLİK </b></th>");
                    sb.Append("<th><b> ADET </b></th>");
                    sb.Append("<th><b> ÜRETİLEN ADET </b></th>");

                    sb.Append("</tr>");
                    for (int i = 0; i < dt.boy.Count(); i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append(dt.boy[i]);
                        sb.Append("</td>");
                        sb.Append("<td  colspan = '3'>");
                        sb.Append(dt.ozellik[i]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(dt.adet[i]);
                        sb.Append("</td>");
                        sb.Append("<td></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("<br />");


                    sb.Append("<table  width='100%' border = '1' cellspacing='0' cellpadding='3'>");
                    sb.Append("<tr><td colspan = '12'></td></tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'' /> Mat Selefon</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px' />  Parlak Selefon</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Lak </td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Klişe</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Gofre</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Zımba</td>");
                    sb.Append(" </tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Serigraf</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Yeni Bıçak</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Mabsan</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Ankutsan</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Hazır Malzeme</td>");
                    sb.Append("<td colspan = '2'><img src='https://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fcheck.png&mode=redirect&view=true' width='22px'/>  Yapıştırma</td>");
                    sb.Append(" </tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    sb.Append("<table  width='100%' align='left'  border = '1'> ");
                    sb.Append("<tr style='background-color: #f2f2f2;'><td colspan = '8'></td></tr>");

                    sb.Append("<tr><td colspan = '1'></td>");
                    sb.Append("<td colspan = '5'></td>");
                    sb.Append("<td colspan = '1'><b> Personel </b> </td>");
                    sb.Append("<td colspan = '1'><b> Not </b> </td></tr>");

                    sb.Append("<tr style='color:white'><td colspan = '1'>Not: </td>");
                    sb.Append("<td colspan = '5'></td>");
                    sb.Append("<td colspan = '1'></td>");
                    sb.Append("<td colspan = '1'></td></tr>");

                    sb.Append("<tr style='color:white'><td colspan = '1'>Not: </td>");
                    sb.Append("<td colspan = '5'></td>");
                    sb.Append("<td colspan = '1'></td>");
                    sb.Append("<td colspan = '1'><b> </b> </td></tr>");

                    sb.Append("<tr style='color:white'  border = '0'><td colspan = '1'>Not: </td>");
                    sb.Append("<td colspan = '5'></td>");
                    sb.Append("<td colspan = '1'></td>");
                    sb.Append("<td colspan = '1'></td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                  
                    sb.Append("<table  width='100%' cellspacing='0'  border = '1' cellpadding='3'>");
                    sb.Append("<tr><td colspan = '3'></td></tr>");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fherboylogo.png&mode=redirect&view=true' />");
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fherboylogo.png&mode=redirect&view=true' />");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Flogo.png&mode=redirect&view=true' />");
                    sb.Append(" </td></tr>");
                    sb.Append("</table>");

                    sb.Append("<table  width='100%' cellspacing='0'  border = '1' cellpadding='3'>");
                    sb.Append("<tr><td colspan = '3'></td></tr>");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fherboylogo.png&mode=redirect&view=true' />");
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fherboylogo.png&mode=redirect&view=true' />");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Flogo.png&mode=redirect&view=true' />");
                    sb.Append(" </td></tr>");
                    sb.Append("</table>");

                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    StyleSheet styles = new StyleSheet();
                    FontFactory.Register("c:\\windows\\fonts\\arial.ttf", "ArialFont");
                    styles.LoadTagStyle("body", "face", "ArialFont");
                    styles.LoadTagStyle("body", "encoding", "Identity-H");
                    styles.LoadTagStyle("body", "size", "8pt");

                    PdfUtil objPdf = new PdfUtil();
                    Rectangle pagesize = new Rectangle(0, 0, PageSize.A4.Width, PageSize.A4.Height);
                    Document doc = new Document(pagesize, 20, 20, 20, 20);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        using (var htmlWorker = new HTMLWorker(pdfDoc, null, styles))
                        {
                            htmlWorker.Parse(sr);
                        }
                        pdfDoc.Close();
                        byte[] bytes = ms.ToArray();
                        ms.Flush();
                        ms.Close();
                        ms.Dispose();
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment; filename=test.pdf");
                        Response.AddHeader("Content-Length", bytes.Length.ToString());
                        Response.ContentType = "application/octet-stream";
                        Response.BinaryWrite(bytes);
                    }
                }
            }
        }
    }
}