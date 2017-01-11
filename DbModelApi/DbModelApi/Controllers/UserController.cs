using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using DbModelApi.CustomAttributes;
using DbModelApi.Filters;
using DbModelApi.Model;
using Newtonsoft.Json;
using RestSharp;

namespace DbModelApi.Controllers
{
    [DeflateCompression]
    [RoutePrefix("api/proxy")]
    public class UserController : BaseController
    {
        private readonly static string backendServiceEndPoint = ConfigurationManager.AppSettings["BackendServiceEndPoint"];
        /// <summary>
        /// 这个方法供登录时候验证用户名、密码使用
        /// </summary>
        /// <param name="requestBody">
        ///     @phone: 传入登录的手机号码
        ///     @password：传入密码
        /// </param>
        /// <returns>返回该用户的相关基本信息</returns>
        [HttpPost]
        [Route("auth")]
        public IHttpActionResult Authenticate(IDictionary<string, string> httpRequestBody)
        {
            if (httpRequestBody == null || httpRequestBody.Count == 0 || string.IsNullOrEmpty(httpRequestBody["username"].Trim()) || string.IsNullOrEmpty(httpRequestBody["password"].Trim()))
            {
                return HandleErrorMessage(HttpStatusCode.BadRequest, "请输入用户名或密码！");
            }
            string scan = string.Empty;
            if (httpRequestBody.Keys.Contains("scan") && !string.IsNullOrEmpty(httpRequestBody["scan"].Trim()))
            {
                scan = httpRequestBody["scan"].Trim();
            }
            string authUri = ConfigurationManager.AppSettings["AuthUri"];

            RestClient restClient = new RestClient(backendServiceEndPoint + authUri);
            restClient.AddDefaultHeader("accept", "application/json");
            restClient.AddDefaultHeader("ClientType", "PC");

            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            string encodedBody = string.Format("grant_type=password&client_id=001&username={0}&password={1}&scope={2}", httpRequestBody["username"].Trim(), httpRequestBody["password"], scan);
            request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            IRestResponse response = restClient.Execute(request);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AuthToken authToken = serializer.Deserialize<AuthToken>(response.Content);
            if (string.IsNullOrEmpty(authToken.Access_Token))
            {
                return HandleErrorMessage(HttpStatusCode.BadRequest, "用户名或密码错误！");
            }

            RequestToken = authToken.Access_Token;
            User user = new User().ConvertUser(authToken);
            CurrentUser = user;

            return Ok(user);
        }

        private IHttpActionResult HandleErrorMessage(HttpStatusCode httpStatusCode, string errorMessage)
        {
            if (errorMessage.ToLower().Contains("authorization") || httpStatusCode == HttpStatusCode.Unauthorized)
            {
                CurrentUser = null;
                RequestToken = null;
                httpStatusCode = HttpStatusCode.Unauthorized;
            }
            //如果包含 { 字符，约定已经是后台传回来的JSON格式的错误消息
            string errorContent = errorMessage.IndexOf('{') < 0 ? JsonConvert.SerializeObject(new ResponseError(httpStatusCode.GetHashCode(), errorMessage)) : errorMessage;

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(httpStatusCode);
            httpResponseMessage.Content = new StringContent(errorContent, Encoding.UTF8);

            return new System.Web.Http.Results.ResponseMessageResult(httpResponseMessage);
        }

        private User CurrentUser
        {
            set { HttpContext.Current.Session["CurrentUser"] = value; }
            get { return HttpContext.Current.Session["CurrentUser"] as User; }
        }

        private string RequestToken
        {
            set { HttpContext.Current.Session["UserRequestToken"] = value; }
            get { return HttpContext.Current.Session["UserRequestToken"] as string; }
        }
    }
}
