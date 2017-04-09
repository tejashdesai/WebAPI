using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace InsuranceWebAPI.BusinessLayer
{
    public class Common
    {
        public static string GetHashPassword(string password, string saltKey)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(String.Concat(password, saltKey), "SHA1");
        }

        public static string GenerateSalt(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[length];
                rng.GetBytes(buffer);
                return Convert.ToBase64String(buffer);
            }
        }

        public static string CreatePasswordHash(string password, string saltKey)
        {
            string saltAndPassword = String.Concat(password, saltKey);
            var hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, "SHA1");
            return hashedPassword;
        }

        public bool sendMail(string name, string policyNumber, string date, string email, string senderEmail,
            string senderName, string CredentialEmailID, string CredentialPassword)
        {
            string EmailBody;
            string EmailError = "";

            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate") + "/Template.html"))
            {
                EmailBody = reader.ReadToEnd().ToString();
                EmailBody = EmailBody.Replace("%HolderName%", name);
                EmailBody = EmailBody.Replace("%PolicyNumber%", policyNumber);
                EmailBody = EmailBody.Replace("%Date%", date);
            }


            SendEmialWithSecure(email,
                senderEmail, senderName, senderEmail, senderName,
                System.Configuration.ConfigurationManager.AppSettings["DailySubject"].ToString(),
                EmailBody, System.Configuration.ConfigurationManager.AppSettings["SMTPIP"].ToString(),
               Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPORT"].ToString()),
               CredentialEmailID, CredentialPassword, ref EmailError);
            if (EmailError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string sendSMS(string name, string policyNumber, string date,string SMSUserName,string SMSPassword,
           string SMSSender,string SMSMessageType,string SMSRoute, string mobile)
        {
            string msg = @"Dear " + name + ", your national insurance mediclaim policy " + policyNumber + " expires on " + date + ". Please submit the renewal fees to avoid policy lapse. Contact your Agent Nayana Kapadia (9898764740).";

            string mobile_no = mobile;

            string userName = SMSUserName,
                password = SMSPassword,
                senderid = SMSSender,
                msgType = SMSMessageType,
                route = SMSRoute;

            string strAPI = "http://sms.growupitsolution.com/index.php/smsapi/httpapi/?uname=" + userName + "&password=" + password + "&sender=" + senderid + "&receiver=" + mobile_no + "&route=" + route + "&msgtype=" + msgType + "&sms=" + msg;
            var html = new WebClient().DownloadString(strAPI);
            return html;
        }

        private bool SendEmialWithSecure(string ToEmailId, string FromEmailId, string FromName, string SenderEmailId,
        string SenderName, string Subject, string MailBody, string SMTPHost, Int32 SMTPPort,
        string CredentialEmailId, string CredentialPassword, ref string EmailError)
        {
            EmailError = "";
            try
            {
                System.Net.Mail.MailMessage ResetPassMail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                ResetPassMail.To.Add(new MailAddress(ToEmailId));
                ResetPassMail.From = new MailAddress(FromEmailId, FromName);
                ResetPassMail.Sender = new MailAddress(SenderEmailId, SenderName);
                ResetPassMail.Subject = Subject;
                ResetPassMail.IsBodyHtml = true;
                ResetPassMail.Body = MailBody;
                ResetPassMail.Priority = System.Net.Mail.MailPriority.High;

                SmtpServer.Host = SMTPHost;
                SmtpServer.Port = SMTPPort;
                SmtpServer.Credentials = new NetworkCredential(CredentialEmailId, CredentialPassword);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (SMTPHost.Contains("gmail"))
                {
                    SmtpServer.EnableSsl = true;
                }

                try
                {
                    SmtpServer.Send(ResetPassMail);
                    return true;
                }
                catch (SmtpException smtpExe)
                {
                    throw new Exception("Message :" + smtpExe.Message + " Details : " + smtpExe.ToString());
                }
            }
            catch (Exception ex)
            {
                EmailError = ex.Message.ToString().Trim();
                return false;
            }
        }

        private bool SendEmialWithSecureCC(string ToEmailId, string CCEmailId, string FromEmailId, string FromName, string SenderEmailId,
        string SenderName, string Subject, string MailBody, string SMTPHost, Int32 SMTPPort,
        string CredentialEmailId, string CredentialPassword, ref string EmailError)
        {
            try
            {
                EmailError = "";
                System.Net.Mail.MailMessage ResetPassMail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                ResetPassMail.To.Add(new MailAddress(ToEmailId));
                ResetPassMail.CC.Add(new MailAddress(CCEmailId));
                ResetPassMail.From = new MailAddress(FromEmailId, FromName);
                ResetPassMail.Sender = new MailAddress(SenderEmailId, SenderName);
                ResetPassMail.Subject = Subject;
                ResetPassMail.IsBodyHtml = true;
                ResetPassMail.Body = MailBody;
                ResetPassMail.Priority = System.Net.Mail.MailPriority.High;

                SmtpServer.Host = SMTPHost;
                SmtpServer.Port = SMTPPort;
                SmtpServer.Credentials = new NetworkCredential(CredentialEmailId, CredentialPassword);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (SMTPHost.Contains("gmail"))
                {
                    SmtpServer.EnableSsl = true;
                }

                try
                {
                    SmtpServer.Send(ResetPassMail);
                    return true;
                }
                catch (SmtpException smtpExe)
                {
                    throw new Exception("Message :" + smtpExe.Message + " Details : " + smtpExe.ToString());
                }
            }
            catch (Exception ex)
            {
                EmailError = ex.Message.ToString().Trim();
                return false;
            }
        }
    }
}