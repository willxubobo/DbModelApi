using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

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
    }
}
