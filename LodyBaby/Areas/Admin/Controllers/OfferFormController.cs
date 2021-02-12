
using Areas.Admin.Utils;
using DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Areas.Admin.Controllers
{
    public class OfferFormController : BaseAdminController
    {

        public ActionResult Index()
        {
            return View(manager.repo_offer.List());
        }

        public ActionResult SendEmail(string Id)
        {
        
            return Redirect("/offerform-list");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Detail(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = manager.repo_offer.Find(m => m.Guid == Id);
            if (offer == null)
            {
                return HttpNotFound();
            }
         
            return View();

        }

        public String Complete(FormModel formModel, Offer offer)
        {
            try
            {

                offer.CreateDate = DateTime.Now;
                offer.Createby = AdminName;
                offer.Guid = Guid.NewGuid().ToString();
                offer.PersonName = formModel.personName;
                offer.CompanyName = formModel.companyName;
                offer.Email = formModel.personEmail;
                offer.Gsm = formModel.gsm;


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
                offer.Height = boy;
                offer.Property = ozellik;
                offer.Number = adet;
                offer.Price = fiyat;
                manager.repo_offer.Insert(offer);
                return "True";

            }
            catch (Exception)
            {
                return "Hata";
            }
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FormModel form = (from actionplan in manager.repo_offer.List().AsEnumerable()
                              where (actionplan.Id == Id)
                              select new FormModel
                              {
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
            return View(form);

        }

        public String FormEdit(FormModel formModel, Offer offer)
        {
            Offer offerEdit = manager.repo_offer.Find(m => m.Id == formModel.Id);

            try
            {

                offerEdit.UpdateDate = DateTime.Now;
                offerEdit.Updateby = AdminName;
                offerEdit.PersonName = formModel.personName;
                offerEdit.CompanyName = formModel.companyName;
                offerEdit.Email = formModel.personEmail;
                offerEdit.Gsm = formModel.gsm;


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
                offerEdit.Height = boy;
                offerEdit.Property = ozellik;
                offerEdit.Number = adet;
                offerEdit.Price = fiyat;
                manager.repo_offer.Update(offerEdit);
                return "True";

            }
            catch (Exception)
            {
                return "Hata";
            }
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = manager.repo_offer.GetById(Id);
            manager.repo_offer.Delete(offer);
            return Redirect("/offerform-list");
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
                    sb.Append("<tr><td colspan = '3'></td></tr>");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Fherboylogo.png&mode=redirect&view=true' />");
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td align='center'  colspan = '1'><h2><b>TEKLİF FORMU </b></h2></td></tr>");
                    sb.Append("<tr><td td colspan = '1'><img src='http://ebys.sumergida.com/Filemanager/connectors/ashx/filemanager.ashx?path=C%3A%2Finetpub%2Fwwwroot%2FEbys%2Ffiles%2FImages%2Flogo.png&mode=redirect&view=true' />");
                    sb.Append(" </td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    sb.Append("<table  width='100%' align='left'  border = '1'> ");
                    sb.Append("<tr><td colspan = '5'></td></tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan = '1'><b> Firma Adı </b> </td>");
                    sb.Append("<td colspan = '2'>" + companyName + "</td>");
                    sb.Append("<td colspan = '1'><b> Tarih </b></td>");
                    sb.Append("<td colspan = '1'>" + date + "</td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td colspan = '1'><b> Görüşülen Kişi</b> ");
                    sb.Append("<td colspan = '2'>" + Name + "</td>");
                    sb.Append("<td colspan = '1'><b> Sıra No </b>");
                    sb.Append("<td colspan = '1'>" + Id + "</td>");
                    sb.Append("</tr>");

                    //sb.Append("<tr>");
                    //sb.Append("<tr><td colspan = '1'><b> Tel </b> ");
                    //sb.Append("<td colspan = '1'>" + Tel + "</td>");
                    //sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<tr><td colspan = '1'><b> Email </b>");
                    sb.Append("<tr><td colspan = '4'>"+ Email + "</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<tr><td colspan = '1'><b> GSM </b> ");
                    sb.Append("<td colspan = '4'>" +Tel+ "</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr><td colspan = '3'><b> Konu :</b> ");
                    //sb.Append("Fiyat Teklifi" + "</td></tr>");
                    sb.Append("</table>");

                    sb.Append("<br />");
                    sb.Append("<table  width='100%' border = '1' cellspacing='0' cellpadding='3'>");
                    sb.Append("<tr><td colspan = '3'></td></tr>");

                    sb.Append("<tr><td colspan = '3'><b> Ödeme Şekli:</b> ");
                    sb.Append("NAKİT (%50 SİPARİŞTE, %50 TESLİMATTA)");
                    sb.Append("<br />");
                    sb.Append("İlgili ürün/ürünlere ait fiyat teklifimiz ve satış şartlarımız görüş ve bilgilerinize sunulmuştur." +
                        " Siparişlerinizi bekler, çalışmalarınızda başarılar dileriz.      <br /> <b>NOT:</b>   ADETLERDE  + / -  % 10 " +
                        " FARKLILIK OLABİLİR. KOLİ ÖLÇÜLERİ DIŞTAN DIŞA NETTİR VE CM OLARAK HESAPLANMIŞTIR.");
                    sb.Append(" </td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    sb.Append("<table border = '1'>");
                    sb.Append("<tr>");
                    sb.Append("<th><b> EBAT </b></th>");
                    sb.Append("<th colspan = '3'><b> ÖZELLİK </b></th>");
                    sb.Append("<th><b> ADET </b></th>");
                    sb.Append("<th><b> BİRİM FİYAT </b></th>");
                    sb.Append("<th><b> TOPLAM </b></th>");
                    sb.Append("</tr>");
                    for (int i = 0; i < dt.boy.Count(); i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>");
                        sb.Append(dt.boy[i]);
                        sb.Append("</td>");
                        sb.Append("<td colspan = '3'>");
                        sb.Append(dt.ozellik[i]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(dt.adet[i]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(dt.fiyat[i]);
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append(Convert.ToDecimal(dt.fiyat[i]) * Convert.ToDecimal(dt.adet[i]));
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }

                    sb.Append("</table>");
                    sb.Append("<br />");
                    sb.Append("<br />");
                    //
                    sb.Append("<table  width='100%' cellspacing='0' cellpadding='3'>");
                    sb.Append("<tr><td colspan = '3'>");
                    sb.Append("<b> a) </b> KDV fiyatlara dahil edilmemiştir.Faturada tarafınıza bildirilecektir. ");
                    sb.Append("<br />");
                    sb.Append("<b> b) </b> Üretimimizin özelliği sebebiyle adetlerimizde (+ / -) % 10 farklılık olabilir.");
                    sb.Append("<br />");
                    sb.Append("<b> c) </b> Siparişin teslimatından önce girdi fiyatlarımızın " +
                        "değişmesi durumunda oluşacak artışlar yeni fiyatların geçerli olacağı tarihten itibaren arttırılacaktır");

                    sb.Append("<br />");
                    sb.Append("<b> d) </b> Hazırlandığı tarih itibari ile teklifimiz 10 gün süre ile geçerlidir.");
                    sb.Append("<br />");
                    sb.Append("<b> e) </b>  Kamyon bazında siparişlerde sevkiyat ücreti tarafımıza aittir. Teslimat İstanbul için geçerlidir." +
                        " İstanbul dışı ambar teslimidir. Ambar ücerti alıcıya aittir.");
                    sb.Append("<br />");
                    sb.Append("<b> f) </b> Karşılıksız çekler 5 iş günü içersinde NAKDEN olarak tahsil edilir.");
                    sb.Append("<br />");
                    sb.Append("<b> g) </b> Baskılı işlerde klişe, film ücretleri size aittir.");
                    sb.Append("<br />");
                    sb.Append("<b> h) </b> Fiyatlar fatura tarihi itibari ile 7 gün içerisinde, satış vadesine hazırlanmış evrakla tahsil esasına  " +
                        "göre tanzim edilmiştir. Vadesini geçen ödemelerde % 5 vade farkı uygulanacaktır.");
                    sb.Append("<br />");
                    sb.Append("<b> ı) </b> Paletli sevkıyatlarda palet iadesi alınacaktır. İadesi olmayacak  siparişlerde fiyatlara % 3 ilave edilecektir.");
                    sb.Append("<br />");
                    sb.Append("<b> j) </b> Ölçülerimiz dıştan dışa nettir.");
                    sb.Append("<br />");
                    sb.Append("<b> j) </b> 2 yıl kullanılmayan klişeler arşivde imha edilir . Yeni siparişte tekrar üretilir.");

                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");
                    sb.Append("<br />");
                    //
                    sb.Append("<table  width='100%' border = '1' cellspacing='0' cellpadding='3'>");
                    //sb.Append("<tr><td align='center'  colspan = '3'><h2><b></b></h2></td></tr>");
                    sb.Append("<tr><td colspan = '3'></td></tr>");
                    sb.Append("<tr><td colspan = '1'><b> Adres :</b> ");
                    sb.Append("<br />");
                    sb.Append("Sakarya Cad. Anadolum Sk. No:4 <br /> Ataşehir / İstanbul");
                    sb.Append("<tr><td colspan = '1'><b> Tel :</b> ");
                    sb.Append("<br />");
                    sb.Append("Avrupa : 0212 501 00 61 <br/> Anadolu: 0216 661 66 66");
                    sb.Append("<br />");
                    sb.Append("<tr><td colspan = '1'><b> Email :</b> ");
                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("ulusmatbaa@gmail.com");
                    sb.Append("<br />");

                    //sb.Append("<tr><td colspan = '2'><b> Genel Müdür Onay :</b> <br /><br />");
                    //sb.Append("<tr><td colspan = '1'><b> Müşteri Temsilcisi Onay:</b> <br /><br />");

                    sb.Append("<tr><td colspan = '3'><b> Banka Hesap No : Garanti Bankası</b> <br />");
                    sb.Append("Hamza AYYILDIZ  Hesap No : 769-6297769  KüçükBakkalKöy | IBAN No: TR18 0006 2000 7690 0006 2977 69 ");
                    sb.Append(" </td></tr>");
                    sb.Append("</table>");
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    StyleSheet styles = new StyleSheet();
                    FontFactory.Register("c:\\windows\\fonts\\arial.ttf", "ArialFont");
                    styles.LoadTagStyle("body", "face", "ArialFont");
                    styles.LoadTagStyle("body", "encoding", "Identity-H");
                    styles.LoadTagStyle("body", "size", "8pt");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        using (var htmlWorker = new HTMLWorker(pdfDoc, null, styles))
                        {
                            htmlWorker.Parse(sr);
                        }
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();
                        MailMessage mesaj = new MailMessage();
                        mesaj.From = new MailAddress("satis@herboykoli.com", "HerboyKoli Bildirim!");
                        mesaj.CC.Add("satis@herboykoli.com");
                        mesaj.To.Add(dt.personEmail);
                        //mesaj.To.Add("mesutkaya2000@gmail.com");
                        mesaj.Subject = "HeryBoyKoli | Bildirim";
                        mesaj.Body = "Bu mesaj Herboykoli satış temsilcisi tarafından size iletilmiştir.";
                        mesaj.Attachments.Add(new Attachment(new MemoryStream(bytes), companyName + ".pdf"));
                        //mesaj.Body = sb.ToString();
                        mesaj.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient("mail.herboykoli.com", 587);
                        client.Credentials = new NetworkCredential("satis@herboykoli.com", "Herboy123654");
                        //client.EnableSsl = false;
                        client.Send(mesaj);
                    }
                }
            }
        }
    }
}