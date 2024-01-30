using System.Net;
using AWSLambdaDuyuru;
using duyuru;
using HtmlAgilityPack;

public class Data
{
    string url = "https://bilgisayar.dpu.edu.tr/tr/index/duyurular";
    WebClient webClient;
    HtmlDocument doc;
    Mail mail;
    HtmlNodeCollection duyurular;
    string duyuruBaslik;
    string duyuruIcerik;
    string webSitesiHtml;

    public Data()
    {
        init();
    }

    public void getData()
    {
        webSitesiHtml = webClient.DownloadString(url);
        doc.LoadHtml(webSitesiHtml);
        duyurular = doc.DocumentNode.SelectNodes("//div[@class='media']");
        duyuruBaslik = "YENİ BİR DUYURU EKLENDİ! Dpü Bilgisayar";
        duyuruIcerik = duyurular[0].SelectSingleNode(".//h4").InnerText;
    }

    public void sendData(S3FileSystem s3)
    {
        if (duyurular != null && duyurular.Count > 0)
        {
            if (!Function.readedAWSFileContent.Equals(duyuruIcerik))
            {
                mail.sendMail(duyuruBaslik, duyuruIcerik);
                s3.changeDataAsync(duyuruIcerik);
                Thread.Sleep(4000);
            }
        }
        else
        {
            Console.WriteLine("Duyurular bulunamadı.");
        }
    }

    private void init()
    {
        webClient = new WebClient();
        doc = new HtmlDocument();
        mail = new Mail();
    }

}