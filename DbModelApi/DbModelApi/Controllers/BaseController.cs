using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DbModelApi.Model;
using NET.Framework.Common.CacheHelper;

namespace DbModelApi.Controllers
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// 获取webapi参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetPara(string name)
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"]; //获取传统context
            HttpRequestBase request = context.Request; //定义传统request对象
            string result = request.Params[name];
            if (result == null)
            {
                return "";
            }
            else
            {
                return context.Server.UrlDecode(result);
            }

        }

        //用来保存当前的用户信息
        public User LoginUser { get { return CurrentUser(); } }

        public User CurrentUser()
        {
            User user = new User();
            string sessionId = HttpContext.Current.Request["sessionId"];
            if (!string.IsNullOrEmpty(sessionId))
            {
                if (MemcachedCache.Get<User>(sessionId) != null)
                {
                    user = MemcachedCache.Get<User>(sessionId);
                }
            }
            return user;
        }
        
    }
}
