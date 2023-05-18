using CarRental.Classes.Entity;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Classes
{
    internal class MessageTemplate
    {
        //Создание письма для входа в аккаунт
        public static MailMessage CreateUniqueCode(int ActionClient)
        {
            int code;
            Random rnd = new Random();
            code = rnd.Next(100000, 999999);
            User user = ConnectDB.DB.User.Where(x => x.UserID == ActionClient).FirstOrDefault();
            user.UserUniqueCode = code;
            ConnectDB.DB.SaveChanges();
            var from = new MailAddress("webcarrental24@gmail.com", "CarRental24");
            var to = new MailAddress(user.UserEmail);
            var mail = new MailMessage(from, to);
            mail.Subject = "Код подтверждения для доступа к системе";
            mail.Body = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';background-color:#edf2f7;margin:0;padding:0;width:100%\"><tbody><tr>\r\n<td align=\"center\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol'\">\r\n<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';margin:0;padding:0;width:100%\">\r\n<tbody><tr>\r\n<td style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';padding:25px 0;text-align:center\">\r\n<a style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';color:#3d4852;font-size:19px;font-weight:bold;text-decoration:none;display:inline-block\" target=\"_blank\">\r\nКод подтверждения для входа в систему\r\n</a>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';background-color:#edf2f7;border-bottom:1px solid #edf2f7;border-top:1px solid #edf2f7;margin:0;padding:0;width:100%\">\r\n<table align=\"center\" width=\"570\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';background-color:#ffffff;border-color:#e8e5ef;border-radius:2px;border-width:1px;margin:0 auto;padding:0;width:570px\">\r\n<tbody><tr>\r\n<td style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';max-width:100vw;padding:32px\">\r\n<h1 style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';color:#3d4852;font-size:18px;font-weight:bold;margin-top:0;text-align:left\">Ваш код безопасности!</h1>\r\n<p style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:16px;line-height:1.5em;margin-top:0;text-align:left\"><strong style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol'\">" + code + "</strong> </p>\r\n<p style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:16px;line-height:1.5em;margin-top:0;text-align:left\">Если вы не запрашивали этот код, возможно кто-то пытается получить доступ к вашему аккаунту. Никому не сообщайте данный код!</p>\r\n\r\n\r\n\r\n</td>\r\n</tr>\r\n</tbody></table>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol'\">\r\n<table align=\"center\" width=\"570\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';margin:0auto;padding:0;text-align:center;width:570px\"><tbody><tr>\r\n<td align=\"center\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';max-width:100vw;padding:32px\">\r\n<p style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';line-height:1.5em;margin-top:0;color:#b0adc5;font-size:12px;text-align:center\">© 2023 CarRental. All rights reserved.</p>\r\n\r\n</td>\r\n</tr></tbody></table>\r\n</td>\r\n</tr>\r\n</tbody></table>\r\n</td>\r\n</tr></tbody></table>";
            mail.IsBodyHtml = true;
            return mail;
        }

        //Отправка письма
        public static void SendMail(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("webcarrental24@gmail.com", "qnttjbetxbnzsivy");
            smtp.Send(mail);
        }

        public static MailMessage CreateMail(int ActionClient, string tema, string textmessage)
        {
            Client client = ConnectDB.DB.Client.Where(x=> x.ClientID == ActionClient).FirstOrDefault();
            var from = new MailAddress("webcarrental24@gmail.com", "CarRental24");
            var to = new MailAddress(client.ClientEmail);
            var mail = new MailMessage(from, to);
            mail.Subject = tema;
            mail.Body = "<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';background-color:#edf2f7;margin:0;padding:0;width:100%\"><tbody><tr>\r\n<td align=\"center\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol'\">\r\n<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';margin:0;padding:0;width:100%\">\r\n<tbody><tr>\r\n<td style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';padding:25px 0;text-align:center\">\r\n\r\n</td>\r\n</tr>\r\n<tr>\r\n<td width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';background-color:#edf2f7;border-bottom:1px solid #edf2f7;border-top:1px solid #edf2f7;margin:0;padding:0;width:100%\">\r\n<table align=\"center\" width=\"570\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';background-color:#ffffff;border-color:#e8e5ef;border-radius:2px;border-width:1px;margin:0 auto;padding:0;width:570px\">\r\n<tbody><tr>\r\n<td style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';max-width:100vw;padding:32px\">\r\n\r\n\r\n<p style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:16px;line-height:1.5em;margin-top:0;text-align:left\"> " + textmessage + " </p>\r\n\r\n\r\n\r\n</td>\r\n</tr>\r\n</tbody></table>\r\n</td>\r\n</tr>\r\n<tr>\r\n<td style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol'\">\r\n<table align=\"center\" width=\"570\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';margin:0auto;padding:0;text-align:center;width:570px\"><tbody><tr>\r\n<td align=\"center\" style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';max-width:100vw;padding:32px\">\r\n<p style=\"box-sizing:border-box;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';line-height:1.5em;margin-top:0;color:#b0adc5;font-size:12px;text-align:center\">© 2023 CarRental. All rights reserved.</p>\r\n\r\n</td>\r\n</tr></tbody></table>\r\n</td>\r\n</tr>\r\n</tbody></table>\r\n</td>\r\n</tr></tbody></table>";
            mail.IsBodyHtml = true;
            return mail;
        }
    }
}
