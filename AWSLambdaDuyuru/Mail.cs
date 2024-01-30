using System.Net;
using System.Net.Mail;

namespace duyuru
{
    internal class Mail
    {
        // E-posta gönderenin bilgileri
        string gondericiEmail = "";
        string gondericiSifre = "";

        // E-posta alıcının bilgileri
        string aliciEmail = "sametozalpbusiness@gmail.com";

        // E-posta sunucusunun ve portunun belirlenmesi (Google SMTP'si örneği)
        string smtpSunucu = "smtp.office365.com";
        int smtpPort = 587;
        SmtpClient smtpClient;
        MailMessage mailMessage;

        List<string> aliciEmailList = new List<string>
        {
            "sametozalpbusiness@gmail.com",
        };

        public void sendMail(string duyuruBaslik, string duyuruIcerik)
        {
            try
            {
                smtpClient = new SmtpClient(smtpSunucu);
                smtpClient.Port = smtpPort;
                smtpClient.Credentials = new NetworkCredential(gondericiEmail, gondericiSifre);
                smtpClient.EnableSsl = true;

                mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(gondericiEmail);
                mailMessage.Subject = duyuruBaslik;
                mailMessage.Body = duyuruIcerik;

                foreach (string aliciEmail in aliciEmailList)
                {
                    mailMessage.To.Add(aliciEmail);
                }

                smtpClient.Send(mailMessage);
                Console.WriteLine("E-posta başarıyla gönderildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu: " + ex.Message);
            }
        }
    }
}
