using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Configuration;

namespace NET.Framework.Common.Authentication
{
    public class ADHelper
    {
        //[DllImport("advapi32.dll")]
        //private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        //public bool ValidateUserAccount(string strDomainName, string strDomainAccount, string strDomainPassword)
        //{
        //    const int LOGON32_LOGON_INTERACTIVE = 2; //通过网络验证账户合法性
        //    const int LOGON32_PROVIDER_DEFAULT = 0; //使用默认的Windows 2000/NT NTLM验证方

        //    IntPtr tokenHandle = new IntPtr(0);
        //    tokenHandle = IntPtr.Zero;

        //    string domainName = strDomainName; //域 如:officedomain
        //    string domainAccount = strDomainAccount; //域帐号 如:administrator
        //    string domainPassword = strDomainPassword;//密码
        //    bool checkok = LogonUser(domainAccount, domainName, domainPassword, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

        //    return checkok;
        //}

        string ADIPAddress = ConfigurationManager.AppSettings["ADServerIP"];
        string DomainName = ConfigurationManager.AppSettings["DomainName"];
        string strPort = ConfigurationManager.AppSettings["ADPort"];

        public bool ValidateUserAccount(string UserName, string Password, out string ErrorMsg)
        {
            ErrorMsg = "";
           
            DirectoryEntry AD = new DirectoryEntry();
            AD.Path = string.Format("LDAP://{0}", ADIPAddress);
            //AD.Username = DomainName + @"\" + UserName;
            AD.Username = UserName + "@" + DomainName;
            AD.Password = Password;
            
            AD.AuthenticationType = AuthenticationTypes.Secure;
            if(!string.IsNullOrEmpty(strPort))
            {
                AD.Options.PasswordPort = int.Parse(strPort);
            }

            try
            {
                DirectorySearcher searcher = new DirectorySearcher(AD);
                searcher.Filter = String.Format("(&(objectClass=user)(samAccountName={0}))", UserName);
                System.DirectoryServices.SearchResult result = searcher.FindOne();
                if (result != null)
                {
                    DirectoryEntry userEntry = result.GetDirectoryEntry();
                    if (userEntry != null)
                    {
                        return true;
                    }
                }
                else
                {
                    ErrorMsg = "登录失败，未知账号或密码错误！";
                }
                AD.Close();
            }
            catch (Exception ex)
            {
                ErrorMsg = "登录失败，原因：" + ex.Message.Replace("\r\n","");
            }
            return false;
        }
    }
}
