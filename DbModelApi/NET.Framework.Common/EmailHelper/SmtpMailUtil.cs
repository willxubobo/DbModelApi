using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NET.Framework.Common.EmailHelper
{
    public class SmtpMailUtil
    {
        //public static bool SendMail(MailConfig mailConfig)
        //{
        //    bool useThirdPartMailServer = bool.Parse(ConfigurationManager.AppSettings.Get("UseThirdPartMailServer"));

        //    if (useThirdPartMailServer)
        //    {
        //        return SendMailWithSLL(mailConfig);
        //    }
        //    return SendMailWithDefault(mailConfig);
        //}

        public static bool SendMailWithDefault(MailConfig mailConfig)
        {
            //if the switch: SendNoticeEmail was set to false，it will not send mail.
            bool isSendEmail = bool.Parse(ConfigurationManager.AppSettings.Get("IfSendEmail"));
            if (!isSendEmail)
            {
                return true;
            }

            try
            {
                string mailFromDisplay = ConfigurationManager.AppSettings.Get("FromDisplay");
                string mailServer = ConfigurationManager.AppSettings.Get("EmailServer");
                int emailServerPort = int.Parse(ConfigurationManager.AppSettings.Get("EmailServerPort"));

                string mailUserName = ConfigurationManager.AppSettings.Get("SMTPUserAccount");
                string mailPassword = ConfigurationManager.AppSettings.Get("SMTPUserPassword");

                mailConfig.Host = mailServer;
                mailConfig.Port = emailServerPort;
                mailConfig.FromAddress = mailUserName;
                mailConfig.FromDisplay = mailFromDisplay;

                mailConfig.UserName = mailUserName;
                mailConfig.UserPassword = mailPassword;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mailConfig.FromAddress, mailConfig.FromDisplay)
                };


                foreach (string t in mailConfig.ToAddresses)
                {
                    string address = t.Trim();
                    if (address != "")
                    {
                        string[] arrAddress = address.Split(';');
                        if (arrAddress.Length > 0)
                        {
                            foreach (string a in arrAddress)
                            {
                                mailMessage.To.Add(new MailAddress(a));
                            }
                        }
                        else
                        {
                            mailMessage.To.Add(new MailAddress(address));
                        }

                    }
                }

                foreach (string t in mailConfig.CCAddresses)
                {
                    string address = t.Trim();
                    if (address != "")
                    {
                        string[] arrAddress = address.Split(';');
                        if (arrAddress.Length > 0)
                        {
                            foreach (string a in arrAddress)
                            {
                                mailMessage.CC.Add(new MailAddress(a));
                            }
                        }
                        else
                        {
                            mailMessage.CC.Add(new MailAddress(address));
                        }

                    }
                }
                mailMessage.Subject = mailConfig.Subject;
                mailMessage.Body = mailConfig.Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                var sc = new SmtpClient(mailConfig.Host)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(mailConfig.UserName, mailConfig.UserPassword)
                };
                //sc.Credentials = CredentialCache.DefaultNetworkCredentials;// 
                sc.Send(mailMessage);
                //sc.SendMailAsync(mailMessage);
                mailMessage.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static bool SendMailWithSLL(MailConfig mailConfig)
        //{
        //    //if the switch: SendNoticeEmail was set to false，it will not send mail.
        //    bool isSendEmail = bool.Parse(ConfigurationManager.AppSettings.Get("IfSendEmail"));
        //    if (!isSendEmail)
        //    {
        //        return true;
        //    }

        //    try
        //    {
        //        string mailServer = ConfigurationManager.AppSettings.Get("SMTPServerAddress");
        //        int emailServerPortal = int.Parse(ConfigurationManager.AppSettings.Get("SMTPServerPort"));

        //        string mailUserName = ConfigurationManager.AppSettings.Get("SMTPUserAccount");
        //        string mailPassword = ConfigurationManager.AppSettings.Get("SMTPUserPassword");

        //        mailConfig.Host = mailServer;
        //        mailConfig.Port = emailServerPortal;
        //        mailConfig.UserName = mailUserName;
        //        mailConfig.UserPassword = mailPassword;
        //        mailConfig.FromAddress = mailUserName;
        //        mailConfig.FromDisplay = mailUserName;

        //        var mailMessage = new MailMessage
        //        {
        //            From = new MailAddress(mailConfig.FromAddress, mailConfig.FromDisplay)
        //        };
        //        if (mailConfig.ToAddresses != null)
        //        {
        //            foreach (string t in mailConfig.ToAddresses)
        //            {
        //                string address = t.Trim();
        //                if (address != "")
        //                {
        //                    mailMessage.To.Add(new MailAddress(address));
        //                }
        //            }
        //        }

        //        mailMessage.Subject = mailConfig.Subject;
        //        mailMessage.Body = mailConfig.Body;
        //        mailMessage.IsBodyHtml = true;
        //        mailMessage.BodyEncoding = Encoding.UTF8;

        //        var sc = new SmtpClient(mailConfig.Host, mailConfig.Port)
        //        {
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            EnableSsl = true,
        //            Credentials = new NetworkCredential(mailConfig.UserName, mailConfig.UserPassword)
        //        };

        //        //sc.Send(mailMessage);
        //        //mailMessage.Dispose();

        //        sc.SendMailAsync(mailMessage);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}